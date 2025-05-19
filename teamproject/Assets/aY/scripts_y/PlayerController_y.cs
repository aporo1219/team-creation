using UnityEngine;

public class PlayerController_y : MonoBehaviour
{
    public static PlayerController_y instance;

    private Rigidbody rb;
    //private CapsuleCollider collider;

    //ステータス変数
    public float MoveSpeed = 5.0f;  //移動速度
    public float JumpPower = 5.0f;  //ジャンプ力

    //入力
    private float InputH = 0.0f;    //横
    private float InputV = 0.0f;    //縦

    private bool onGround = false;  //接地判定
    private bool DJump = false; //空中ジャンプが残っているか

    LayerMask layerMask;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //collider = GetComponent<CapsuleCollider>();

        instance = this;

        layerMask = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        //入力取得
        InputH = Input.GetAxisRaw("Horizontal");
        InputV = Input.GetAxisRaw("Vertical");

        //接地判定取得
        onGround = Physics.Raycast(rb.position, Vector3.down, 1.0f, layerMask);

        if(onGround)
        {   //地面についたら
            //空中ジャンプを回復させる
            DJump = true;
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        //if (Input.GetButtonDown("Attack"))
        //{
        //    Attack();
        //}
    }

    private void FixedUpdate()
    {
        //カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        //方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * InputV + Camera.main.transform.right * InputH;
        //移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
        rb.linearVelocity = moveForward * MoveSpeed + new Vector3(0, rb.linearVelocity.y, 0);
        //キャラクターの向きを進行方向に
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
    }

    private void Jump()
    {
        if (onGround)
        {   //接地しているなら
            //ジャンプする
            rb.AddForce(0.0f, JumpPower, 0.0f, ForceMode.Impulse);
        }
        else if (DJump)
        {   //空中ジャンプが残っているなら
            //上下の移動力をリセットする
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0.0f, rb.linearVelocity.z);
            //ジャンプする
            rb.AddForce(0.0f, JumpPower, 0.0f, ForceMode.Impulse);

            DJump = false;
        }
    }

    private void Attack()
    {
        Debug.Log("攻撃");
    }
}