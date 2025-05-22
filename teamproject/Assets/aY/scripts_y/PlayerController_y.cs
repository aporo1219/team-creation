using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController_y : MonoBehaviour
{
    public static PlayerController_y instance;

    private Rigidbody rb;
    //private CapsuleCollider collider;

    //�X�e�[�^�X�ϐ�
    public string State = ("");
    public float MoveSpeed = 5.0f;  //�ړ����x
    public float JumpPower = 5.0f;  //�W�����v��
    public float DodgeSpeed = 20.0f;

    //����
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction attackAction;
    private InputAction dodgeAction;

    private Vector2 moveValue;

    private bool onGround = false;  //�ڒn����
    private bool isJump = false;
    private bool DoubleJump = false;     //�󒆃W�����v���c���Ă��邩
    private int AirTime = 0;        //�؋󎞊�
    public int JumpLimit = 10;      //�W�����v�ł���ő�؋󎞊�

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



        //���͎擾
        moveValue = moveAction.ReadValue<Vector2>();

        //�n�ʔ���擾
        if(Physics.SphereCast(rb.position, transform.localScale.y/2-0.1f, Vector3.down, out RaycastHit h, transform.localScale.y/2+0.2f, layerMask))
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

        //Physics.Raycast(rb.position, Vector3.down, transform.localScale.y, layerMask)
        //Physics.SphereCast(rb.position, 1.0f, Vector3.down, out RaycastHit h, transform.localScale.y, layerMask)

        if(AirTime > JumpLimit)
        {//��莞�ԋ󒆂ɂ���
            //�ڒn������Ȃ���
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
        //�J�����̕�������X-Z���ʂ̒P�ʃx�N�g�����擾
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        //�����L�[�̓��͒l�ƃJ�����̌�������ړ�����������
        Vector3 moveForward = cameraForward * moveValue.y + Camera.main.transform.right * moveValue.x;

        if (canMove)
        {
            //�ړ������ɃX�s�[�h���|����
            rb.linearVelocity = moveForward * MoveSpeed + new Vector3(0, rb.linearVelocity.y, 0);

        }

        if (canRotate)
        {
            //�L�����N�^�[�̌�����i�s������
            if (moveForward != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(moveForward);
            }
        }
        


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
        Debug.Log("���");
    }
}