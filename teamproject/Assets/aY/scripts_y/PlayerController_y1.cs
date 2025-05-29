using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController_y1 : MonoBehaviour
{
    public static PlayerController_y1 instance;

    private Rigidbody rb;
    //private CapsuleCollider collider;

    //�X�e�[�^�X�ϐ�
    public string State = "";
    public float MoveSpeed = 5.0f;  //�ړ����x
    public float JumpPower = 10.0f;  //�W�����v��
    public float DoubleJumpPower = 8.0f;  //�󒆃W�����v��
    public float DodgeSpeed = 20.0f;

    //����
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction attackAction;
    private InputAction dodgeAction;

    private Vector2 moveValue;

    [SerializeField] private bool onGround = false;  //�ڒn����
    private bool canJump = true;  //
    [SerializeField] private int JumpTime = 0;
    private int LongJumpLimit = 20;
    private bool DoubleJump = false;     //�󒆃W�����v���c���Ă��邩
    private int AirTime = 0;        //�؋󎞊�
    public int JumpLimit = 10;      //�W�����v�ł���ő�؋󎞊�

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

        //���͎擾
        moveValue = moveAction.ReadValue<Vector2>();

        //�n�ʔ���擾
        if(Physics.SphereCast(rb.position, transform.localScale.y/2-0.1f, Vector3.down, out RaycastHit h, transform.localScale.y/2+0.15f, layerMask))
        {//�n�ʂɂ��Ă���
            //�ڒn��Ԃɂ���
            onGround = true;
            ////�󒆃W�����v���񕜂�����
            DoubleJump = true;
            //�G�A�^�C��������
            AirTime = 0;
            //�󒆉����
            AirDodgeTimeCount = 120;
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

        if (DodgeTimeCount < DodgeCoolTime)
            DodgeTimeCount++;

        if (AirDodgeTimeCount < AirDodgeCoolTime)
            AirDodgeTimeCount++;
    }

    private IEnumerator Jump()
    {
        if (onGround)
        {   //�ڒn���Ă���Ȃ�

            while (JumpTime < LongJumpLimit && jumpAction.IsPressed() && canJump)
            {
                //�ڒn������Ȃ���
                onGround = false;

                //�W�����v����
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, JumpPower, rb.linearVelocity.z);

                JumpTime++;

                yield return null;
            }

                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 3.0f, rb.linearVelocity.z);
                JumpTime = 0;


            
        }
        else if (DoubleJump)
        {   //�󒆃W�����v���c���Ă���Ȃ�
            //�W�����v����
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, DoubleJumpPower, rb.linearVelocity.z);

            AirDodgeTimeCount = AirDodgeCoolTime;

            DoubleJump = false;
        }
    }

    private void Attack()
    {
        Debug.Log("�U��");
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
                Debug.Log("�n����");
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
                Debug.Log("�󒆉��");
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
                    Debug.Log("�R���{��t");
                    combo = true;
                }

                ATK = false;

                yield return null;
            }
            Debug.Log("�R���{��t�I��");
        }

        if (combo)
        {
            Debug.Log("�R���{�h��");
            Attack();
        }
        else
        {
            Debug.Log("�R���{���Z�b�g");
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