using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using RPGCharacterAnims.Actions;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController_y1 : MonoBehaviour
{
    public static PlayerController_y1 instance;

    public PlayerStatus Status;

    [SerializeField] private GameObject Barrier;
    [SerializeField] private GameObject ActiveSword;
    [SerializeField] private GameObject DisposeSword;

    [HideInInspector] public Rigidbody rb;

    //SE
    [SerializeField] AudioSource AS;
    [SerializeField] AudioClip AvoidanceSE;
    [SerializeField] AudioClip JumpSE;

    //ステータス変数---------------------------------------
    public float MoveSpeed = 5.0f;          //移動速度
    public float DashSpeed = 10.0f;         //移動速度
    public float JumpPower = 20.0f;         //ジャンプ力
    public float DoubleJumpPower = 30.0f;   //空中ジャンプ力
    public float DodgeSpeed = 30.0f;        //地上回避速度
    public float AirDodgeSpeed = 50.0f;     //空中回避速度
    //-----------------------------------------------------

    //入力-------------------------------------------------
    private InputAction moveAction;     //左スティック
    private InputAction dashAction;     //左スティック押し込み
    private InputAction jumpAction;     //Aボタン
    [HideInInspector] public InputAction attackAction;   //Xボタン
    private InputAction dodgeAction;    //Rトリガー
    private InputAction guardAction;    //Lトリガー
    private Vector2 moveValue;          //左スティック入力量
    //-----------------------------------------------------

    [HideInInspector] public bool Dash = false;

    public bool onGround = false;     //接地判定
    [SerializeField] private bool GroundHit = false;    //地形との衝突判定
    private int JumpTime = 0;           //ジャンプ持続
    private int LongJumpLimit = 10;     //最大ジャンプ時間
    private bool DoubleJump = false;    //空中ジャンプが残っているか
    private int AirTime = 0;            //滞空時間
    private int JumpLimit = 10;         //ジャンプできる最大滞空時間
    private bool isJump = false;        //ジャンプ中か
    private int LandCount;
    private int LandTime = 0;

    //行動許可---------------------------------------------
    public bool canMove = true;     //移動
    public bool canRotate = true;   //回転
    public bool canJump = true;     //ジャンプ
    public bool canAction = true;   //アクション
    //-----------------------------------------------------

    bool ATK = false;

    private int DodgeTimeCount;         //地上回避タイムカウント
    private int DodgeCoolTime = 10;     //地上回避クールタイム

    private int AirDodgeTimeCount;      //空中回避タイムカウント
    private int AirDodgeCoolTime = 120; //空中回避クールタイム

    //アニメーション管理用変数-----------------------------
    private string NowAnime = "Idle";
    private string OldAnime = "";
    private string PlayAnime = "";
    private bool ChangeAnime = true;
    private bool Move;
    private bool isDodge = false;
    private bool isGuard = false;
    //-----------------------------------------------------

    //攻撃の種類
    [HideInInspector]
    public enum AttackType
    {
        None,
        G1, G2, G3, GF,
        A1, A2, A3, AF,
    }

    public int AttackNum = 0;      //攻撃の段階
    [SerializeField] public AttackType AttackState;        //どの攻撃か

    //地上レイヤー
    LayerMask layerMask;

    //アニメーター
    [HideInInspector] public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60;

        Status = GetComponent<PlayerStatus>();

        //Rigidbody取得
        rb = GetComponent<Rigidbody>();

        //インプットシステム取得
        moveAction = InputSystem.actions.FindAction("Move");
        dashAction = InputSystem.actions.FindAction("Dash");
        jumpAction = InputSystem.actions.FindAction("Jump");
        attackAction = InputSystem.actions.FindAction("Attack");
        dodgeAction = InputSystem.actions.FindAction("Dodge");
        guardAction = InputSystem.actions.FindAction("Guard");

        LandCount = LandTime;

        //回避クールタイム初期化
        DodgeTimeCount = DodgeCoolTime;
        AirDodgeTimeCount = AirDodgeCoolTime;

        //インスタンス設定
        instance = this;

        //レイヤーマスクにグラウンドレイヤー設定
        layerMask = LayerMask.GetMask("Ground");

        //アニメーター取得
        animator = GetComponentInChildren<Animator>();

        Barrier.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        //ゲームが動いていないなら処理を行わない
        if (Time.timeScale == 0)
        {
            return;
        }

        PlayAnime = "";

        //左スティック入力取得
        moveValue = moveAction.ReadValue<Vector2>();

        //地面判定取得
        if (Physics.SphereCast(rb.position, transform.localScale.y / 2 - 0.1f, Vector3.down, out RaycastHit h, transform.localScale.y / 2 + 0.4f, layerMask))
        {//地面についている
            if (!onGround)
            {
                //if (Dash)
                //{
                //    AnimationPlay("Dash-Land");
                //}
                //else
                //{
                //    AnimationPlay("Land");
                //}
                
            }

            //接地状態にする
            onGround = true;
            ////空中ジャンプを回復させる
            DoubleJump = true;
            //エアタイム初期化
            AirTime = 0;

            if (LandCount < LandTime)
                LandCount++;
            //空中回避回復
            AirDodgeTimeCount = 120;

            animator.SetBool("onGround", true);
        }
        else
        {//空中にいる
            //エアタイム増加
            AirTime++;

            LandCount = 0;
            //
            if (!isJump && canMove)
                animator.SetBool("onGround", false);
        }



        if (AirTime > JumpLimit)
        {//一定時間空中にいる
            //接地判定をなくす
            onGround = false;
        }

        //入力検知
        {
            //ダッシュ
            if (dashAction.WasPressedThisFrame() && onGround)
            {
                //ダッシュ切替
                if (Dash)
                {
                    Dash = false;
                }
                else
                {
                    Dash = true;
                }

            }
            //スティックの入力無し
            if (moveValue.x == 0 && moveValue.y == 0)
            {
                //ダッシュ解除
                Dash = false;
                //
                if (onGround)
                {
                    animator.SetBool("Move", false);
                }

            }
            //ジャンプ
            if (jumpAction.WasPressedThisFrame() && canAction && !isJump)
            {
                StartCoroutine("Jump");
            }
            //通常攻撃
            if (attackAction.WasPressedThisFrame() && canAction && AttackState == AttackType.None)
            {
                Attack();
            }
            //回避
            if (dodgeAction.WasPressedThisFrame() && canAction)
            {
                Dodge();
            }
            //ガード
            if (guardAction.WasPressedThisFrame() && canAction)
            {
                StartCoroutine("Guard");
            }
        }
        //ジャンプ以外で少し浮いた時に下向きに強い力を与える
        if (onGround && !isJump && !GroundHit && canMove)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, -10, rb.linearVelocity.z);
        }

    }

    private void FixedUpdate()
    {


        //カメラの方向からX-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        //方向キーの入力値とカメラの向きから移動方向を決定
        Vector3 moveForward = cameraForward * moveValue.y + Camera.main.transform.right * moveValue.x;
        //
        moveForward.Normalize();

        if (onGround && (moveValue.x != 0 || moveValue.y != 0))
        {
            Move = true;
            animator.SetBool("Move", true);
        }

        //移動処理
        if (canMove && LandCount == LandTime)
        {
            if (Dash)
            {//ダッシュ時
                //移動方向にダッシュスピードを掛ける
                rb.linearVelocity = moveForward * DashSpeed + new Vector3(0, rb.linearVelocity.y, 0);
                //
                animator.SetBool("Dash", true);

                ActiveSword.SetActive(false);
                DisposeSword.SetActive(true);

                
            }
            else
            {//通常時
                //移動方向に移動スピードを掛ける
                rb.linearVelocity = moveForward * MoveSpeed + new Vector3(0, rb.linearVelocity.y, 0);
                //
                animator.SetBool("Dash", false);

                ActiveSword.SetActive(true);
                DisposeSword.SetActive(false);

            }
                

        }

        

        if (canRotate)
        {
            //キャラクターの向きを進行方向に
            if (moveForward != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(moveForward);
            }
        }
        //重力強化
        rb.AddForce(new Vector3(0, -5, 0));
        //地上回避クールタイム
        if (DodgeTimeCount < DodgeCoolTime)
            DodgeTimeCount++;
        //空中回避クールタイム
        if (AirDodgeTimeCount < AirDodgeCoolTime)
            AirDodgeTimeCount++;


    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Bullet")
    //    {
    //        if (Status.ColliderStste == PlayerStatus.ColliderMode.Neutral)
    //        {
    //            Status.HP -= 5;
    //        }
    //        else if(Status.ColliderStste == PlayerStatus.ColliderMode.Guard)
    //        {
    //            Status.HP -= 2;
    //        }
    //        else if (Status.ColliderStste == PlayerStatus.ColliderMode.Invincible)
    //        {
    //            Status.HP -= 0;
    //        }
    //    }
    //}

    private void OnCollisionStay(Collision collision)
    {
        //地形との接触検知
        if (collision.gameObject.layer == 6)
        {
            GroundHit = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //地形から離れたのを検知
        if (collision.gameObject.layer == 6)
        {
            GroundHit = false;
        }
    }

    private IEnumerator Jump()
    {
        if (onGround)
        {   //接地しているなら
            if (LandCount == LandTime)
            {
                //ジャンプ中に
                isJump = true;
                //
               
                animator.SetBool("Jump", true);
                if (Dash)
                {
                    AnimationPlay("Dash-Jump");
                }
                else
                {
                    AnimationPlay("Jump");
                }
                   

                while (((JumpTime < LongJumpLimit && jumpAction.IsPressed()) || JumpTime < 3) && canJump)
                {
                    //接地判定をなくす
                    onGround = false;
                    //ジャンプする
                    rb.linearVelocity = new Vector3(rb.linearVelocity.x, JumpPower, rb.linearVelocity.z);
                    AS.PlayOneShot(JumpSE);

                    JumpTime++;

                    yield return null;
                }
            }
            

            
            
        }
        else if (DoubleJump)
        {   //空中で空中ジャンプが残っているなら
            //ジャンプ中に
            isJump = true;
            //
            animator.SetBool("Jump", true);
            if (Dash)
            {
                AnimationPlay("Dash-Jump-Flip");
            }
            else
            {
                AnimationPlay("Jump-Flip");
            }
                
            //空中回避のクールタイムをなくす
            AirDodgeTimeCount = AirDodgeCoolTime;
            while (JumpTime < 17 && canJump)
            {
                //ジャンプする
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, DoubleJumpPower, rb.linearVelocity.z);

                JumpTime++;

                yield return null;
            }
            
            

            DoubleJump = false;
        }

        if (canJump&&isJump)
        {
            animator.SetBool("Jump", false);

            //上昇減速
            for (int i = 0; i < 2; i++)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y / 2, rb.linearVelocity.z);

                yield return null;
            }
        }
        animator.SetBool("Jump", false);

        JumpTime = 0;
        //非ジャンプ状態に
        isJump = false;

        yield return null;
    }

    public void Attack()
    {
        Debug.Log("攻撃");

        ActiveSword.SetActive(true);
        DisposeSword.SetActive(false);
        Dash = false;

        canMove = false;
        canRotate = false;
        canAction = false;

        AttackNum++;
        switch (AttackNum)
        {
            case 1://初段

                if (onGround)
                {//地上
                    AttackState = AttackType.G1;
                }
                else
                {//空中
                    AttackState = AttackType.A1;
                }

                break;
            case 2://二段目

                if (onGround)
                {//地上
                    AttackState = AttackType.G2;
                }
                else
                {//空中
                    AttackState = AttackType.A2;
                }

                break;
            case 3://三段目

                if (onGround)
                {//地上
                    AttackState = AttackType.G3;
                }
                else
                {//空中
                    AttackState = AttackType.A3;
                }

                break;
            case 4://フィニッシュ

                if (onGround)
                {//地上
                    AttackState = AttackType.GF;
                }
                else
                {//空中
                    AttackState = AttackType.AF;
                }

                break;
        }

        PlayerAttack.instance.Attack(AttackState);

    }

    private void Dodge()
    {
        if (onGround)
        {
            if (DodgeTimeCount == DodgeCoolTime)
            {
                Debug.Log("地上回避");
                canMove = false;
                canJump = false;
                canRotate = false;
                canAction = false;

                //
                AS.PlayOneShot(AvoidanceSE);
                

                StartCoroutine("DodgeMove", 1.0f);
                DodgeTimeCount = 0;
            }
            
        }
        else
        {
            if (AirDodgeTimeCount == AirDodgeCoolTime)
            {
                Debug.Log("空中回避");
                canMove = false;
                canJump = false;
                canRotate = false;
                canAction = false;
                rb.useGravity = false;

                //rb.linearVelocity = transform.forward * DodgeSpeed;
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0.0f, rb.linearVelocity.z);

                AS.PlayOneShot(AvoidanceSE);
                StartCoroutine("AirDodgeMove", 0.3f);
                AirDodgeTimeCount = 0;
            }
                
        }
        

    }

    private IEnumerator Guard()
    {
        

        while (guardAction.IsPressed())
        {
            if(onGround)
            {
                rb.linearVelocity = new Vector3(0.0f, rb.linearVelocity.y, 0.0f);

                animator.SetBool("Guard", true);
                AnimationPlay("Guard-In");
                for (float i = 0; i < 0.3f; i += Time.deltaTime)
                {
                    yield return null;
                }

                Barrier.SetActive(true);
                Status.ColliderStste = PlayerStatus.ColliderMode.Guard;

                canMove = false;
                canJump = false;
                canRotate = false;
                canAction = false;

                yield return null;
            }
            yield return null;
        }

        animator.SetBool("Guard", false);

        Barrier.SetActive(false);
        Status.ColliderStste = PlayerStatus.ColliderMode.Neutral;

        for (float i = 0; i < 0.3f; i += Time.deltaTime)
        {
            yield return null;
        }

        
        canMove = true;
        canJump = true;
        canRotate = true;
        canAction = true;

        yield return null;
    }

    private IEnumerator DodgeMove(float actiontime)
    {
        float time = 0.0f;
        //rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0.0f, rb.linearVelocity.z);
        animator.SetBool("Dodge", true);
        if (Dash)
        {
            AnimationPlay("Dash-DodgeRoll");
        }
        else
        {
            AnimationPlay("DodgeRoll");
        }
            

        rb.linearVelocity = transform.forward * MoveSpeed;

        while (time < actiontime * 0.2f)
        {
            if(Math.Abs(rb.linearVelocity.x)<Math.Abs(transform.forward.x*DodgeSpeed))
            rb.linearVelocity = rb.linearVelocity * 1.2f;

            if (!GroundHit && rb.linearVelocity.y > 0)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            }

            rb.linearVelocity = new Vector3(rb.linearVelocity.x, -10, rb.linearVelocity.z);

            time += Time.deltaTime;
            yield return null;
        }

        Status.ColliderStste = PlayerStatus.ColliderMode.Invincible;

        while (time < actiontime * 0.6f) 
        {
            rb.linearVelocity = transform.forward * DodgeSpeed;

            if (!GroundHit && rb.linearVelocity.y > 0)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            }

            rb.linearVelocity = new Vector3(rb.linearVelocity.x, -10, rb.linearVelocity.z);

            time += Time.deltaTime; 
            yield return null; 
        }

        animator.SetBool("Dodge", false);
        Status.ColliderStste = PlayerStatus.ColliderMode.Neutral;

        while (time < actiontime)
        {
            
            if (!GroundHit && rb.linearVelocity.y > 0)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            }

            rb.linearVelocity = new Vector3(rb.linearVelocity.x*0.9f, -10, rb.linearVelocity.z*0.9f);

            time += Time.deltaTime;
            yield return null;
        }

        canMove = true;
        canJump = true;
        canRotate = true;
        canAction = true;
        rb.useGravity = true;
        
        yield return null;
    }

    private IEnumerator AirDodgeMove(float actiontime)
    {
        float time = 0.0f;

        animator.SetBool("Dodge", true);
        if(Dash)
        {
            AnimationPlay("Dash-Air-Roll");
        }
        else
        {
            AnimationPlay("Air-Roll");
        }

            Status.ColliderStste = PlayerStatus.ColliderMode.Invincible;

        while (time < actiontime * 0.6f)
        {
            rb.linearVelocity = transform.forward * AirDodgeSpeed;

            if (!GroundHit && rb.linearVelocity.y != 0)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            }

            time += Time.deltaTime;
            yield return null;
        }

        animator.SetBool("Dodge", false);
        Status.ColliderStste = PlayerStatus.ColliderMode.Neutral;

        while (time < actiontime)
        {

            if (!GroundHit && rb.linearVelocity.y > 0)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            }

            rb.linearVelocity = new Vector3(rb.linearVelocity.x * 0.9f, rb.linearVelocity.y, rb.linearVelocity.z * 0.9f);

            time += Time.deltaTime;
            yield return null;
        }

        canMove = true;
        canJump = true;
        canRotate = true;
        canAction = true;
        rb.useGravity = true;

        yield return null;
    }

    public void AnimationPlay(string animasion_name, float crosstime = 0.1f)
    {
        if(animator.runtimeAnimatorController.name != "Animation-Controller_y")
            return;

        animator.CrossFade(animasion_name, crosstime);

        //if (animasion_name == "")
        //{
        //    if(Dash)
        //    {
        //        if (!onGround)
        //        {
        //            NowAnime = "Dash-Fall";
        //        }
        //        else
        //        {
        //            if (rb.linearVelocity.x != 0 || rb.linearVelocity.z != 0)
        //            {
        //                NowAnime = "Dash";
        //            }
        //            else
        //            {
        //                NowAnime = "Idle";
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (!onGround)
        //        {
        //            NowAnime = "Fall";
        //        }
        //        else
        //        {
        //            if (rb.linearVelocity.x != 0 || rb.linearVelocity.z != 0)
        //            {
        //                NowAnime = "Run";
        //            }
        //            else
        //            {
        //                NowAnime = "Idle";
        //            }
        //        }
        //    }

        //    if (NowAnime != OldAnime && ChangeAnime)
        //    {
        //        OldAnime = NowAnime;
        //        animator.CrossFade(NowAnime, crosstime);
        //    }

        //}
        //else
        //{
        //    NowAnime = animasion_name;

        //    if (NowAnime != OldAnime)
        //    {
        //        OldAnime = NowAnime;
        //        animator.CrossFade(NowAnime, crosstime);
        //    }

        //    ChangeAnime = false;
        //}




    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(new Vector3(rb.position.x, rb.position.y - transform.localScale.y / 2-0.4f, rb.position.z), transform.localScale.y / 2 - 0.0f);
    //}
}