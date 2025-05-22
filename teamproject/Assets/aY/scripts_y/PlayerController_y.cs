using UnityEngine;

public class PlayerController_y : MonoBehaviour
{
    public static PlayerController_y instance;

    private Rigidbody rb;
    //private CapsuleCollider collider;

    //�X�e�[�^�X�ϐ�
    public string State = ("");
    public float MoveSpeed = 5.0f;  //�ړ����x
    public float JumpPower = 5.0f;  //�W�����v��
    public float DodgeSpeed = 10.0f;

    //����
    private float InputH = 0.0f;    //��
    private float InputV = 0.0f;    //�c

    private bool onGround = false;  //�ڒn����
    private bool isJump = false;
    private bool DoubleJump = false;     //�󒆃W�����v���c���Ă��邩
    private int AirTime = 0;        //�؋󎞊�
    public int JumpLimit = 10;      //�W�����v�ł���ő�؋󎞊�

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
        

        //���͎擾
        InputH = Input.GetAxisRaw("Horizontal");
        InputV = Input.GetAxisRaw("Vertical");

        //�n�ʔ���擾
        if(Physics.Raycast(rb.position, Vector3.down, transform.localScale.y, layerMask))
        {//�n�ʂɂ��Ă���
            //�ڒn��Ԃɂ���
            onGround = true;
            ////�󒆃W�����v���񕜂�����
            DoubleJump = true;
            //�G�A�^�C��������
            AirTime = 0;
        }
        else
        {//�󒆂ɂ���
            //�G�A�^�C������
            AirTime++;
        }

        if(AirTime > JumpLimit)
        {//��莞�ԋ󒆂ɂ���
            //�ڒn������Ȃ���
            onGround = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        //if (Input.GetButtonDown("Attack"))
        //{
        //    Attack();
        //}

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dodge();
        }
    }

    private void FixedUpdate()
    {
        //�J�����̕�������AX-Z���ʂ̒P�ʃx�N�g�����擾
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        //�����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
        Vector3 moveForward = cameraForward * InputV + Camera.main.transform.right * InputH;

        //�ړ������ɃX�s�[�h���|����B
        rb.linearVelocity = moveForward * MoveSpeed + new Vector3(0, rb.linearVelocity.y, 0);

        //�L�����N�^�[�̌�����i�s������
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }



        if (DodgeTimeCount == DodgeCoolTime)
            isDodge = true;
        if (AirDodgeTimeCount == AirDodgeCoolTime)
            isAirDodge = true;
        if (!isDodge)
            DodgeTimeCount++;
        if (!isAirDodge)
            AirDodgeTimeCount++;

    }

    private void Jump()
    {
        if (onGround)
        {   //�ڒn���Ă���Ȃ�
            //�W�����v����
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, JumpPower, rb.linearVelocity.z);
            //�ڒn������Ȃ���
            onGround = false;
        }
        else if (DoubleJump)
        {   //�󒆃W�����v���c���Ă���Ȃ�
            //�W�����v����
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, JumpPower, rb.linearVelocity.z);

            DoubleJump = false;
        }
    }

    private void Attack()
    {
        Debug.Log("�U��");
    }

    private void Dodge()
    {
        if (onGround && isDodge)
        {
            nowDodge = true;
            rb.AddForce(transform.forward * DodgeSpeed);


            isDodge = false;
            DodgeTimeCount = 0;
        }

        if (!onGround && isAirDodge)
        {

        }
    }
}