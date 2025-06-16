using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController_y1 : MonoBehaviour
{
    public static PlayerController_y1 instance;

    private Rigidbody rb;

    //ステータス変数
    public string State = "";
    public float MoveSpeed = 5.0f;  //移動速度
    public float DashSpeed = 10.0f;  //移動速度
    public float JumpPower = 20.0f;  //ジャンプ力
    public float DoubleJumpPower = 30.0f;  //空中ジャンプ力
    public float DodgeSpeed = 30.0f;
    public float AirDodgeSpeed = 50.0f;

    //入力
    private InputAction moveAction;
    private InputAction dashAction;
    private InputAction jumpAction;
    private InputAction attackAction;
    private InputAction dodgeAction;
    private InputAction guardAction;

    private Vector2 moveValue;

    private bool Dash = false;

    [SerializeField] private bool onGround = false;  //接地判定
    [SerializeField] private bool GroundHit = false;
    private int JumpTime = 0;
    private int LongJumpLimit = 10;
    private bool DoubleJump = false;     //空中ジャンプが残っているか
    private int AirTime = 0;        //滞空時間
    private int JumpLimit = 10;      //ジャンプできる最大滞空時間
    private bool isJump = false;

    public bool canMove = true;
    public bool canRotate = true;
    public bool canJump = true;  //
    public bool canAction = true;

    bool ATK = false;

    private int DodgeTimeCount;
    private int DodgeCoolTime = 10;

    private int AirDodgeTimeCount;
    private int AirDodgeCoolTime = 120;

    private bool nowDodge = false;

    private int AttackNum = 0;
    [SerializeField] public string AttackState = "";

    LayerMask layerMask;

    Animator animator;

    private string NowAnime = "";
    private string OldAnime = "";

    public string JumpAnime;
    public string DoubleJumpAnime;
    public string FallAnime;
    public string RunAnime;
    public string NeutralAnime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60;

        rb = GetComponent<Rigidbody>();

        moveAction = InputSystem.actions.FindAction("Move");
        dashAction = InputSystem.actions.FindAction("Dash");
        jumpAction = InputSystem.actions.FindAction("Jump");
        attackAction = InputSystem.actions.FindAction("Attack");
        dodgeAction = InputSystem.actions.FindAction("Dodge");
        guardAction = InputSystem.actions.FindAction("Guard");

        DodgeTimeCount = DodgeCoolTime;
        AirDodgeTimeCount = AirDodgeCoolTime;

        instance = this;

        layerMask = LayerMask.GetMask("Ground");

       
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
        {
            return;
        }

        //Debug.Log(Time.deltaTime);

        //入力取得
        moveValue = moveAction.ReadValue<Vector2>();

        //地面判定取得
        if (Physics.SphereCast(rb.position, transform.localScale.y / 2 - 0.1f, Vector3.down, out RaycastHit h, transform.localScale.y / 2 + 0.4f, layerMask))
        {//地面についている
            //接地状態にする
            onGround = true;
            ////空中ジャンプを回復させる
            DoubleJump = true;
            //エアタイム初期化
            AirTime = 0;
            //空中回避回復
            AirDodgeTimeCount = 120;
        }
        else
        {//空中にいる
            //エアタイム増加
            AirTime++;
            //
            if (!isJump)
                NowAnime = FallAnime;
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
                    NowAnime = NeutralAnime;
                    //animator.SetBool("Moving", false);
                }

            }
            //ジャンプ
            if (jumpAction.WasPressedThisFrame() && canAction)
            {
                StartCoroutine("Jump");
            }
            //通常攻撃
            if (attackAction.WasPressedThisFrame() && canAction && AttackState == "")
            {
                Attack();
            }
            //回避
            if (dodgeAction.WasPressedThisFrame() && canAction)
            {
                Dodge();
            }
            //ガード
            if (guardAction.WasPressedThisFrame() && canAction && onGround)
            {
                StartCoroutine("Guard");
            }
        }

        animator.SetBool("Moving", true);

        Debug.Log("Anime = " + NowAnime);
        if (NowAnime != OldAnime)
        {
            animator.Play(NowAnime);

            OldAnime = NowAnime;
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

        if (canMove)
        {
            if (Dash)
            {//ダッシュ時
                //移動方向にダッシュスピードを掛ける
                rb.linearVelocity = moveForward * DashSpeed + new Vector3(0, rb.linearVelocity.y, 0);
                //
                if (onGround)
                    NowAnime = RunAnime;
                    //animator.SetBool("Moving", true);
            }
            else
            {//通常時
                //移動方向に移動スピードを掛ける
                rb.linearVelocity = moveForward * MoveSpeed + new Vector3(0, rb.linearVelocity.y, 0);
                //
                if (onGround)
                    NowAnime = RunAnime;
                    //animator.SetBool("Moving", true);
            }
                

        }
        

        //ジャンプ以外で少し浮いた時に下向きに強い力を与える
        if (onGround && !isJump && !GroundHit && canMove)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, -10, rb.linearVelocity.z);
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
            //ジャンプ中に
            isJump = true;
            //
            NowAnime = JumpAnime;

            while (((JumpTime < LongJumpLimit && jumpAction.IsPressed()) || JumpTime < 3) && canJump)
            {
                //接地判定をなくす
                onGround = false;
                //ジャンプする
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, JumpPower, rb.linearVelocity.z);

                JumpTime++;

                yield return null;
            }

            
            
        }
        else if (DoubleJump)
        {   //空中で空中ジャンプが残っているなら
            //ジャンプ中に
            isJump = true;
            //
            NowAnime = DoubleJumpAnime;
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

        if (canJump)
            //上昇減速
            for(int i = 0; i < 2; i++)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y / 2, rb.linearVelocity.z);

                yield return null;
            }
        
        JumpTime = 0;
        //非ジャンプ状態に
        isJump = false;

        yield return null;
    }

    private void Attack()
    {
        Debug.Log("攻撃");
        ATK = true;
        AttackNum++;
        switch (AttackNum)
        {
            case 1://初段

                if (onGround)
                {//地上
                    AttackState = "GroundFirst";
                    StartCoroutine("AttackCombo", 0.5f);
                }
                else
                {//空中
                    AttackState = "AirFirst";
                    StartCoroutine("AttackCombo", 0.5f);
                }

                break;
            case 2://二段目

                if (onGround)
                {//地上
                    AttackState = "GroundSecond";
                    StartCoroutine("AttackCombo", 0.5f);
                }
                else
                {//空中
                    AttackState = "AirSecond";
                    StartCoroutine("AttackCombo", 0.5f);
                }

                break;
            case 3://三段目

                if (onGround)
                {//地上
                    AttackState = "GroundThird";
                    StartCoroutine("AttackCombo", 0.5f);
                }
                else
                {//空中
                    AttackState = "AirThird";
                    StartCoroutine("AttackCombo", 0.5f);
                }

                break;
            case 4://フィニッシュ

                if (onGround)
                {//地上
                    AttackState = "GroundFinish";
                    StartCoroutine("AttackCombo", 0.5f);
                }
                else
                {//空中
                    AttackState = "AirFinish";
                    StartCoroutine("AttackCombo", 0.5f);
                }

                break;
        }

        
        
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
                

                StartCoroutine("DodgeMove", 0.2f);
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

                StartCoroutine("AirDodgeMove", 0.3f);
                AirDodgeTimeCount = 0;
            }
                
        }
        

    }

    private IEnumerator AttackCombo(float inputlimit)
    {
        float time = 0.0f;
        bool combo = false;

        if (AttackNum > 3)
        {
            while (time <inputlimit)
            {
                time += Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            while (time < inputlimit)
            {
                time += Time.deltaTime;

                if (attackAction.WasPressedThisFrame() && !ATK)
                {
                    Debug.Log("コンボ受付");
                    combo = true;
                }

                ATK = false;

                yield return null;
            }
            Debug.Log("コンボ受付終了");
        }

        if (combo)
        {
            Debug.Log("コンボ派生");
            Attack();
        }
        else
        {
            Debug.Log("コンボリセット");
            AttackNum = 0;
            AttackState = "";
        }


        yield return null;
    }

    private IEnumerator Guard()
    {
        

        while (guardAction.IsPressed())
        {
            rb.linearVelocity = new Vector3(0.0f, rb.linearVelocity.y, 0.0f);

            canMove = false;
            canJump = false;
            canRotate = false;
            canAction = false;

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

        while (time < actiontime) 
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

        while (time < actiontime)
        {
            rb.linearVelocity = transform.forward * AirDodgeSpeed;

            if (!GroundHit && rb.linearVelocity.y != 0)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            }

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

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(new Vector3(rb.position.x, rb.position.y - transform.localScale.y / 2-0.4f, rb.position.z), transform.localScale.y / 2 - 0.0f);
    //}
}