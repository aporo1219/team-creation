using UnityEngine;

public class PlayerController_y : MonoBehaviour
{
    public static PlayerController_y instance;

    private Rigidbody rb;
    //private CapsuleCollider collider;

    //�X�e�[�^�X�ϐ�
    public float MoveSpeed = 5.0f;  //�ړ����x
    public float JumpPower = 5.0f;  //�W�����v��

    //����
    private float InputH = 0.0f;    //��
    private float InputV = 0.0f;    //�c

    private bool onGround = false;  //�ڒn����
    private bool DJump = false; //�󒆃W�����v���c���Ă��邩

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
        //���͎擾
        InputH = Input.GetAxisRaw("Horizontal");
        InputV = Input.GetAxisRaw("Vertical");

        //�ڒn����擾
        onGround = Physics.Raycast(rb.position, Vector3.down, 1.0f, layerMask);

        if(onGround)
        {   //�n�ʂɂ�����
            //�󒆃W�����v���񕜂�����
            DJump = true;
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        //�ړ����f
        rb.linearVelocity = new Vector3(InputH * MoveSpeed, rb.linearVelocity.y, InputV * MoveSpeed);
    }

    private void Jump()
    {
        if (onGround)
        {   //�ڒn���Ă���Ȃ�
            //�W�����v����
            rb.AddForce(0.0f, JumpPower, 0.0f, ForceMode.Impulse);
        }
        else if (DJump)
        {   //�󒆃W�����v���c���Ă���Ȃ�
            //�㉺�̈ړ��͂����Z�b�g����
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0.0f, rb.linearVelocity.z);
            //�W�����v����
            rb.AddForce(0.0f, JumpPower, 0.0f, ForceMode.Impulse);

            DJump = false;
        }
    }

}