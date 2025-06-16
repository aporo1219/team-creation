using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController_y1 : MonoBehaviour
{
    public static PlayerController_y1 instance;

    private Rigidbody rb;

    //�X�e�[�^�X�ϐ�
    public string State = "";
    public float MoveSpeed = 5.0f;  //�ړ����x
    public float DashSpeed = 10.0f;  //�ړ����x
    public float JumpPower = 20.0f;  //�W�����v��
    public float DoubleJumpPower = 30.0f;  //�󒆃W�����v��
    public float DodgeSpeed = 30.0f;
    public float AirDodgeSpeed = 50.0f;

    //����
    private InputAction moveAction;
    private InputAction dashAction;
    private InputAction jumpAction;
    private InputAction attackAction;
    private InputAction dodgeAction;
    private InputAction guardAction;

    private Vector2 moveValue;

    private bool Dash = false;

    [SerializeField] private bool onGround = false;  //�ڒn����
    [SerializeField] private bool GroundHit = false;
    private int JumpTime = 0;
    private int LongJumpLimit = 10;
    private bool DoubleJump = false;     //�󒆃W�����v���c���Ă��邩
    private int AirTime = 0;        //�؋󎞊�
    private int JumpLimit = 10;      //�W�����v�ł���ő�؋󎞊�
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

        //���͎擾
        moveValue = moveAction.ReadValue<Vector2>();

        //�n�ʔ���擾
        if (Physics.SphereCast(rb.position, transform.localScale.y / 2 - 0.1f, Vector3.down, out RaycastHit h, transform.localScale.y / 2 + 0.4f, layerMask))
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
            //
            if (!isJump)
                NowAnime = FallAnime;
        }



        if (AirTime > JumpLimit)
        {//��莞�ԋ󒆂ɂ���
            //�ڒn������Ȃ���
            onGround = false;
        }

        //���͌��m
        {
            //�_�b�V��
            if (dashAction.WasPressedThisFrame() && onGround)
            {
                //�_�b�V���ؑ�
                if (Dash)
                {
                    Dash = false;
                }
                else
                {
                    Dash = true;
                }

            }
            //�X�e�B�b�N�̓��͖���
            if (moveValue.x == 0 && moveValue.y == 0)
            {
                //�_�b�V������
                Dash = false;
                //
                if (onGround)
                {
                    NowAnime = NeutralAnime;
                    //animator.SetBool("Moving", false);
                }

            }
            //�W�����v
            if (jumpAction.WasPressedThisFrame() && canAction)
            {
                StartCoroutine("Jump");
            }
            //�ʏ�U��
            if (attackAction.WasPressedThisFrame() && canAction && AttackState == "")
            {
                Attack();
            }
            //���
            if (dodgeAction.WasPressedThisFrame() && canAction)
            {
                Dodge();
            }
            //�K�[�h
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
        //�J�����̕�������X-Z���ʂ̒P�ʃx�N�g�����擾
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        //�����L�[�̓��͒l�ƃJ�����̌�������ړ�����������
        Vector3 moveForward = cameraForward * moveValue.y + Camera.main.transform.right * moveValue.x;
        //
        moveForward.Normalize();

        if (canMove)
        {
            if (Dash)
            {//�_�b�V����
                //�ړ������Ƀ_�b�V���X�s�[�h���|����
                rb.linearVelocity = moveForward * DashSpeed + new Vector3(0, rb.linearVelocity.y, 0);
                //
                if (onGround)
                    NowAnime = RunAnime;
                    //animator.SetBool("Moving", true);
            }
            else
            {//�ʏ펞
                //�ړ������Ɉړ��X�s�[�h���|����
                rb.linearVelocity = moveForward * MoveSpeed + new Vector3(0, rb.linearVelocity.y, 0);
                //
                if (onGround)
                    NowAnime = RunAnime;
                    //animator.SetBool("Moving", true);
            }
                

        }
        

        //�W�����v�ȊO�ŏ������������ɉ������ɋ����͂�^����
        if (onGround && !isJump && !GroundHit && canMove)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, -10, rb.linearVelocity.z);
        }

        if (canRotate)
        {
            //�L�����N�^�[�̌�����i�s������
            if (moveForward != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(moveForward);
            }
        }
        //�d�͋���
        rb.AddForce(new Vector3(0, -5, 0));
        //�n�����N�[���^�C��
        if (DodgeTimeCount < DodgeCoolTime)
            DodgeTimeCount++;
        //�󒆉���N�[���^�C��
        if (AirDodgeTimeCount < AirDodgeCoolTime)
            AirDodgeTimeCount++;
    }

    private void OnCollisionStay(Collision collision)
    {
        //�n�`�Ƃ̐ڐG���m
        if (collision.gameObject.layer == 6)
        {
            GroundHit = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //�n�`���痣�ꂽ�̂����m
        if (collision.gameObject.layer == 6)
        {
            GroundHit = false;
        }
    }

    private IEnumerator Jump()
    {
        if (onGround)
        {   //�ڒn���Ă���Ȃ�
            //�W�����v����
            isJump = true;
            //
            NowAnime = JumpAnime;

            while (((JumpTime < LongJumpLimit && jumpAction.IsPressed()) || JumpTime < 3) && canJump)
            {
                //�ڒn������Ȃ���
                onGround = false;
                //�W�����v����
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, JumpPower, rb.linearVelocity.z);

                JumpTime++;

                yield return null;
            }

            
            
        }
        else if (DoubleJump)
        {   //�󒆂ŋ󒆃W�����v���c���Ă���Ȃ�
            //�W�����v����
            isJump = true;
            //
            NowAnime = DoubleJumpAnime;
            //�󒆉���̃N�[���^�C�����Ȃ���
            AirDodgeTimeCount = AirDodgeCoolTime;
            while (JumpTime < 17 && canJump)
            {
                //�W�����v����
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, DoubleJumpPower, rb.linearVelocity.z);

                JumpTime++;

                yield return null;
            }
            
            

            DoubleJump = false;
        }

        if (canJump)
            //�㏸����
            for(int i = 0; i < 2; i++)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y / 2, rb.linearVelocity.z);

                yield return null;
            }
        
        JumpTime = 0;
        //��W�����v��Ԃ�
        isJump = false;

        yield return null;
    }

    private void Attack()
    {
        Debug.Log("�U��");
        ATK = true;
        AttackNum++;
        switch (AttackNum)
        {
            case 1://���i

                if (onGround)
                {//�n��
                    AttackState = "GroundFirst";
                    StartCoroutine("AttackCombo", 0.5f);
                }
                else
                {//��
                    AttackState = "AirFirst";
                    StartCoroutine("AttackCombo", 0.5f);
                }

                break;
            case 2://��i��

                if (onGround)
                {//�n��
                    AttackState = "GroundSecond";
                    StartCoroutine("AttackCombo", 0.5f);
                }
                else
                {//��
                    AttackState = "AirSecond";
                    StartCoroutine("AttackCombo", 0.5f);
                }

                break;
            case 3://�O�i��

                if (onGround)
                {//�n��
                    AttackState = "GroundThird";
                    StartCoroutine("AttackCombo", 0.5f);
                }
                else
                {//��
                    AttackState = "AirThird";
                    StartCoroutine("AttackCombo", 0.5f);
                }

                break;
            case 4://�t�B�j�b�V��

                if (onGround)
                {//�n��
                    AttackState = "GroundFinish";
                    StartCoroutine("AttackCombo", 0.5f);
                }
                else
                {//��
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

                //
                

                StartCoroutine("DodgeMove", 0.2f);
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