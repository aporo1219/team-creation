using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController_y1 : MonoBehaviour
{
    public static PlayerController_y1 instance;

    private Rigidbody rb;
    //private CapsuleCollider collider;

    //ステータス変数
    public string State = "";
    public float MoveSpeed = 5.0f;  //移動速度
    public float JumpPower = 10.0f;  //ジャンプ力
    public float DoubleJumpPower = 8.0f;  //空中ジャンプ力
    public float DodgeSpeed = 20.0f;

    //入力
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction attackAction;
    private InputAction dodgeAction;

    private Vector2 moveValue;

    [SerializeField] private bool onGround = false;  //接地判定
    private bool canJump = true;  //
    [SerializeField] private int JumpTime = 0;
    private int LongJumpLimit = 20;
    private bool DoubleJump = false;     //空中ジャンプが残っているか
    private int AirTime = 0;        //滞空時間
    public int JumpLimit = 10;      //ジャンプできる最大滞空時間

    private bool canMove = true;
    private bool canRotate = true;
    private bool canAction = true;

    bool ATK = false;

    private int DodgeTimeCount;
    private int DodgeCoolTime = 10;

    private int AirDodgeTimeCount;
    private int AirDodgeCoolTime = 120;

    private bool nowDodge = false;

    private int AttackNum = 0;
    [SerializeField] private string AttackState = "";

    LayerMask layerMask;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60;

        rb = GetComponent<Rigidbody>();
        //collider = GetComponent<CapsuleCollider>();

        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        attackAction = InputSystem.actions.FindAction("Attack");
        dodgeAction = InputSystem.actions.FindAction("Dodge");

        DodgeTimeCount = DodgeCoolTime;
        AirDodgeTimeCount = AirDodgeCoolTime;

        instance = this;

        layerMask = LayerMask.GetMask("Ground");
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
        if(Physics.SphereCast(rb.position, transform.localScale.y/2-0.1f, Vector3.down, out RaycastHit h, transform.localScale.y/2+0.15f, layerMask))
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
        }

        if(AirTime > JumpLimit)
        {//一定時間空中にいる
            //接地判定をなくす
            onGround = false;
        }

        if (jumpAction.WasPressedThisFrame() && canAction)
        {
            StartCoroutine("Jump");
        }

        if (attackAction.WasPressedThisFrame() && canAction && AttackState == "")
        {
            Attack();
        }

        if(dodgeAction.WasPressedThisFrame() && canAction)
        {
            Dodge();
        }
    }

    private void FixedUpdate()
    {
        //カメラの方向からX-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        //方向キーの入力値とカメラの向きから移動方向を決定
        Vector3 moveForward = cameraForward * moveValue.y + Camera.main.transform.right * moveValue.x;

        if (canMove)
        {
            //移動方向にスピードを掛ける
            rb.linearVelocity = moveForward * MoveSpeed + new Vector3(0, rb.linearVelocity.y, 0);

        }

        if (canRotate)
        {
            //キャラクターの向きを進行方向に
            if (moveForward != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(moveForward);
            }
        }

        if (DodgeTimeCount < DodgeCoolTime)
            DodgeTimeCount++;

        if (AirDodgeTimeCount < AirDodgeCoolTime)
            AirDodgeTimeCount++;
    }

    private IEnumerator Jump()
    {
        if (onGround)
        {   //接地しているなら

            while (JumpTime < LongJumpLimit && jumpAction.IsPressed() && canJump)
            {
                //接地判定をなくす
                onGround = false;

                //ジャンプする
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, JumpPower, rb.linearVelocity.z);

                JumpTime++;

                yield return null;
            }

                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 3.0f, rb.linearVelocity.z);
                JumpTime = 0;


            
        }
        else if (DoubleJump)
        {   //空中ジャンプが残っているなら
            //ジャンプする
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, DoubleJumpPower, rb.linearVelocity.z);

            AirDodgeTimeCount = AirDodgeCoolTime;

            DoubleJump = false;
        }
    }

    private void Attack()
    {
        Debug.Log("攻撃");
        ATK = true;
        AttackNum++;
        switch (AttackNum)
        {
            case 1:

                if (onGround)
                {
                    AttackState = "GroundFirst";
                    StartCoroutine("AttackCombo", 0.5f);
                }
                else
                {
                    AttackState = "AirFirst";
                    StartCoroutine("AttackCombo", 0.5f);
                }

                break;
            case 2:

                if (onGround)
                {
                    AttackState = "GroundSecond";
                    StartCoroutine("AttackCombo", 0.5f);
                }
                else
                {
                    AttackState = "AirSecond";
                    StartCoroutine("AttackCombo", 0.5f);
                }

                break;
            case 3:

                if (onGround)
                {
                    AttackState = "GroundThird";
                    StartCoroutine("AttackCombo", 0.5f);
                }
                else
                {
                    AttackState = "AirThird";
                    StartCoroutine("AttackCombo", 0.5f);
                }

                break;
            case 4:

                if (onGround)
                {
                    AttackState = "GroundFinish";
                    StartCoroutine("AttackCombo", 0.5f);
                }
                else
                {
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

                rb.linearVelocity = transform.forward * DodgeSpeed;

                StartCoroutine("ActionEnd", 0.2f);
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

                rb.linearVelocity = transform.forward * DodgeSpeed;
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0.0f, rb.linearVelocity.z);

                StartCoroutine("ActionEnd", 0.3f);
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

    private IEnumerator ActionEnd(float actiontime)
    {
        float time = 0.0f;
        
        while (time < actiontime) 
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0.0f, rb.linearVelocity.z);
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
}