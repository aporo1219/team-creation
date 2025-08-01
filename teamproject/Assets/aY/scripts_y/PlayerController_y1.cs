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

    //�X�e�[�^�X�ϐ�---------------------------------------
    public float MoveSpeed = 5.0f;          //�ړ����x
    public float DashSpeed = 10.0f;         //�ړ����x
    public float JumpPower = 20.0f;         //�W�����v��
    public float DoubleJumpPower = 30.0f;   //�󒆃W�����v��
    public float DodgeSpeed = 30.0f;        //�n���𑬓x
    public float AirDodgeSpeed = 50.0f;     //�󒆉�𑬓x
    //-----------------------------------------------------

    //����-------------------------------------------------
    private InputAction moveAction;     //���X�e�B�b�N
    private InputAction dashAction;     //���X�e�B�b�N��������
    private InputAction jumpAction;     //A�{�^��
    [HideInInspector] public InputAction attackAction;   //X�{�^��
    private InputAction dodgeAction;    //R�g���K�[
    private InputAction guardAction;    //L�g���K�[
    private Vector2 moveValue;          //���X�e�B�b�N���͗�
    //-----------------------------------------------------

    [HideInInspector] public bool Dash = false;

    public bool onGround = false;     //�ڒn����
    [SerializeField] private bool GroundHit = false;    //�n�`�Ƃ̏Փ˔���
    private int JumpTime = 0;           //�W�����v����
    private int LongJumpLimit = 10;     //�ő�W�����v����
    private bool DoubleJump = false;    //�󒆃W�����v���c���Ă��邩
    private int AirTime = 0;            //�؋󎞊�
    private int JumpLimit = 10;         //�W�����v�ł���ő�؋󎞊�
    private bool isJump = false;        //�W�����v����
    private int LandCount;
    private int LandTime = 0;

    //�s������---------------------------------------------
    public bool canMove = true;     //�ړ�
    public bool canRotate = true;   //��]
    public bool canJump = true;     //�W�����v
    public bool canAction = true;   //�A�N�V����
    //-----------------------------------------------------

    bool ATK = false;

    private int DodgeTimeCount;         //�n�����^�C���J�E���g
    private int DodgeCoolTime = 10;     //�n�����N�[���^�C��

    private int AirDodgeTimeCount;      //�󒆉���^�C���J�E���g
    private int AirDodgeCoolTime = 120; //�󒆉���N�[���^�C��

    //�A�j���[�V�����Ǘ��p�ϐ�-----------------------------
    private string NowAnime = "Idle";
    private string OldAnime = "";
    private string PlayAnime = "";
    private bool ChangeAnime = true;
    private bool Move;
    private bool isDodge = false;
    private bool isGuard = false;
    //-----------------------------------------------------

    //�U���̎��
    [HideInInspector]
    public enum AttackType
    {
        None,
        G1, G2, G3, GF,
        A1, A2, A3, AF,
    }

    public int AttackNum = 0;      //�U���̒i�K
    [SerializeField] public AttackType AttackState;        //�ǂ̍U����

    //�n�ヌ�C���[
    LayerMask layerMask;

    //�A�j���[�^�[
    [HideInInspector] public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60;

        Status = GetComponent<PlayerStatus>();

        //Rigidbody�擾
        rb = GetComponent<Rigidbody>();

        //�C���v�b�g�V�X�e���擾
        moveAction = InputSystem.actions.FindAction("Move");
        dashAction = InputSystem.actions.FindAction("Dash");
        jumpAction = InputSystem.actions.FindAction("Jump");
        attackAction = InputSystem.actions.FindAction("Attack");
        dodgeAction = InputSystem.actions.FindAction("Dodge");
        guardAction = InputSystem.actions.FindAction("Guard");

        LandCount = LandTime;

        //����N�[���^�C��������
        DodgeTimeCount = DodgeCoolTime;
        AirDodgeTimeCount = AirDodgeCoolTime;

        //�C���X�^���X�ݒ�
        instance = this;

        //���C���[�}�X�N�ɃO���E���h���C���[�ݒ�
        layerMask = LayerMask.GetMask("Ground");

        //�A�j���[�^�[�擾
        animator = GetComponentInChildren<Animator>();

        Barrier.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        //�Q�[���������Ă��Ȃ��Ȃ珈�����s��Ȃ�
        if (Time.timeScale == 0)
        {
            return;
        }

        PlayAnime = "";

        //���X�e�B�b�N���͎擾
        moveValue = moveAction.ReadValue<Vector2>();

        //�n�ʔ���擾
        if (Physics.SphereCast(rb.position, transform.localScale.y / 2 - 0.1f, Vector3.down, out RaycastHit h, transform.localScale.y / 2 + 0.4f, layerMask))
        {//�n�ʂɂ��Ă���
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

            //�ڒn��Ԃɂ���
            onGround = true;
            ////�󒆃W�����v���񕜂�����
            DoubleJump = true;
            //�G�A�^�C��������
            AirTime = 0;

            if (LandCount < LandTime)
                LandCount++;
            //�󒆉����
            AirDodgeTimeCount = 120;

            animator.SetBool("onGround", true);
        }
        else
        {//�󒆂ɂ���
            //�G�A�^�C������
            AirTime++;

            LandCount = 0;
            //
            if (!isJump && canMove)
                animator.SetBool("onGround", false);
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
                    animator.SetBool("Move", false);
                }

            }
            //�W�����v
            if (jumpAction.WasPressedThisFrame() && canAction && !isJump)
            {
                StartCoroutine("Jump");
            }
            //�ʏ�U��
            if (attackAction.WasPressedThisFrame() && canAction && AttackState == AttackType.None)
            {
                Attack();
            }
            //���
            if (dodgeAction.WasPressedThisFrame() && canAction)
            {
                Dodge();
            }
            //�K�[�h
            if (guardAction.WasPressedThisFrame() && canAction)
            {
                StartCoroutine("Guard");
            }
        }
        //�W�����v�ȊO�ŏ������������ɉ������ɋ����͂�^����
        if (onGround && !isJump && !GroundHit && canMove)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, -10, rb.linearVelocity.z);
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

        if (onGround && (moveValue.x != 0 || moveValue.y != 0))
        {
            Move = true;
            animator.SetBool("Move", true);
        }

        //�ړ�����
        if (canMove && LandCount == LandTime)
        {
            if (Dash)
            {//�_�b�V����
                //�ړ������Ƀ_�b�V���X�s�[�h���|����
                rb.linearVelocity = moveForward * DashSpeed + new Vector3(0, rb.linearVelocity.y, 0);
                //
                animator.SetBool("Dash", true);

                ActiveSword.SetActive(false);
                DisposeSword.SetActive(true);

                
            }
            else
            {//�ʏ펞
                //�ړ������Ɉړ��X�s�[�h���|����
                rb.linearVelocity = moveForward * MoveSpeed + new Vector3(0, rb.linearVelocity.y, 0);
                //
                animator.SetBool("Dash", false);

                ActiveSword.SetActive(true);
                DisposeSword.SetActive(false);

            }
                

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
            if (LandCount == LandTime)
            {
                //�W�����v����
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
                    //�ڒn������Ȃ���
                    onGround = false;
                    //�W�����v����
                    rb.linearVelocity = new Vector3(rb.linearVelocity.x, JumpPower, rb.linearVelocity.z);
                    AS.PlayOneShot(JumpSE);

                    JumpTime++;

                    yield return null;
                }
            }
            

            
            
        }
        else if (DoubleJump)
        {   //�󒆂ŋ󒆃W�����v���c���Ă���Ȃ�
            //�W�����v����
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

        if (canJump&&isJump)
        {
            animator.SetBool("Jump", false);

            //�㏸����
            for (int i = 0; i < 2; i++)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y / 2, rb.linearVelocity.z);

                yield return null;
            }
        }
        animator.SetBool("Jump", false);

        JumpTime = 0;
        //��W�����v��Ԃ�
        isJump = false;

        yield return null;
    }

    public void Attack()
    {
        Debug.Log("�U��");

        ActiveSword.SetActive(true);
        DisposeSword.SetActive(false);
        Dash = false;

        canMove = false;
        canRotate = false;
        canAction = false;

        AttackNum++;
        switch (AttackNum)
        {
            case 1://���i

                if (onGround)
                {//�n��
                    AttackState = AttackType.G1;
                }
                else
                {//��
                    AttackState = AttackType.A1;
                }

                break;
            case 2://��i��

                if (onGround)
                {//�n��
                    AttackState = AttackType.G2;
                }
                else
                {//��
                    AttackState = AttackType.A2;
                }

                break;
            case 3://�O�i��

                if (onGround)
                {//�n��
                    AttackState = AttackType.G3;
                }
                else
                {//��
                    AttackState = AttackType.A3;
                }

                break;
            case 4://�t�B�j�b�V��

                if (onGround)
                {//�n��
                    AttackState = AttackType.GF;
                }
                else
                {//��
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
                Debug.Log("�n����");
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
                Debug.Log("�󒆉��");
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