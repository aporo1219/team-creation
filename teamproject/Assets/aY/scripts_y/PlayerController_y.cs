using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController_y : MonoBehaviour
{
    public static PlayerController_y instance;

    private Rigidbody rb;
    //private CapsuleCollider collider;

    //ステータス変数
    public string State = ("");
    public float MoveSpeed = 5.0f;  //移動速度
    public float JumpPower = 5.0f;  //ジャンプ力
    public float DodgeSpeed = 20.0f;

    //入力
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction attackAction;
    private InputAction dodgeAction;

    private Vector2 moveValue;

    private bool onGround = false;  //接地判定
    private bool isJump = false;
    private bool DoubleJump = false;     //空中ジャンプが残っているか
    private int AirTime = 0;        //滞空時間
    public int JumpLimit = 10;      //ジャンプできる最大滞空時間

    private bool canMove = true;
    private bool canRotate = true;

    public bool isDodge = false;
    public int DodgeTimeCount = 30;
    public int DodgeCoolTime = 30;

    public bool isAirDodge = false;
    public int AirDodgeTimeCount = 120;
    public int AirDodgeCoolTime = 120;

    public bool nowDodge = false;

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



        //入力取得
        moveValue = moveAction.ReadValue<Vector2>();

        //地面判定取得
        if(Physics.SphereCast(rb.position, transform.localScale.y/2-0.1f, Vector3.down, out RaycastHit h, transform.localScale.y/2+0.2f, layerMask))
        {//地面についている
            //接地状態にする
            onGround = true;
            ////空中ジャンプを回復させる
            DoubleJump = true;
            //エアタイム初期化
            AirTime = 0;
        }
        else
        {//空中にいる
            //エアタイム増加
            AirTime++;
        }

        //Physics.Raycast(rb.position, Vector3.down, transform.localScale.y, layerMask)
        //Physics.SphereCast(rb.position, 1.0f, Vector3.down, out RaycastHit h, transform.localScale.y, layerMask)

        if(AirTime > JumpLimit)
        {//一定時間空中にいる
            //接地判定をなくす
            onGround = false;
        }

        if (jumpAction.WasPressedThisFrame())
        {
            Jump();
        }

        if (attackAction.WasPressedThisFrame())
        {
            Attack();
        }

        if(dodgeAction.WasPressedThisFrame())
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
        


    }

    private void Jump()
    {
        if (onGround)
        {   //接地しているなら
            //ジャンプする
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, JumpPower, rb.linearVelocity.z);
            //接地判定をなくす
            onGround = false;
        }
        else if (DoubleJump)
        {   //空中ジャンプが残っているなら
            //ジャンプする
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, JumpPower, rb.linearVelocity.z);

            DoubleJump = false;
        }
    }

    private void Attack()
    {
        Debug.Log("攻撃");
    }

    private void Dodge()
    {
        Debug.Log("回避");
    }
}