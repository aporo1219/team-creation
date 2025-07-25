using System.Security.Cryptography;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;



public class Move_Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static bool Damede_Hit;

    [SerializeField] float x, y, z, w;

    [SerializeField] private GameObject MainCharacter;
    [SerializeField] GameObject SearchErea;
    [SerializeField] private SearchErea Search_Enemy;//��l����T���ϐ�
    [SerializeField] private Transform ModelRoot;// �� ���f���iJR-1�j���w�肷��p
    [SerializeField] private float RotationOffsetY = 0f;//�����̕␳
    [SerializeField] private GameObject[] ModelPrefabs;
    [SerializeField] private AttackErea AttackErea;
    

    private float Speed_Enemy;//�X�s�[�h�̕ϐ�
    private float Time_Lapse;//�G����l�������������Ƃ��̌o�ߎ���
    private int Return;//�G�������l�ɖ߂鎞�Ԃ̕ϐ�
    private int Cool_Time;//�U���̃N�[������
    private bool Mode_Serch;//�T�����[�h���̔���
    private bool Right_Or_Left;//���񎞂̉E�������̔���
    private float Around_Position;
    private bool Turn;//��]�̕ϐ�
    private bool Be_Attacked;//�U�����󂯂����̔���
    private Animator Anim;//�A�j���[�V�����R���|�[�l���g�̎擾
    private float Distance;//�����̌v�Z
    public bool OnGround = false;
    private Rigidbody rd;
    [SerializeField] LayerMask GroundLayer;//���C���[�̎擾
    [SerializeField] float Ground_Distance = 0.2f;
    private bool Not_Move = false;//�������~�߂�(true�Ȃ�Ύ~�܂�)
    

    Vector3 Goal_Position;//�ڕW���_�̍��W�ϐ��i�G���G�j
    Vector3 Initial_Value;//�����n�_�̍��W�ϐ�
    Vector3 Search_Position_Right;//���񂷂�p�̃x�N�g���E
    Vector3 Search_Position_Left;//���񂷂�p�̃x�N�g����
  


    [SerializeField] Vector3 Local_Space_Vec;//�O����̃��[�J����ԃx�N�g��

    //SE
    [SerializeField] private AudioSource AS;
    [SerializeField] private AudioClip BeHit_SE;
    [SerializeField] private AudioClip HitThunder_SE;
    [SerializeField] private AudioClip HitFireBall_SE;
    //[SerializeField] private AudioClip Find;
    private float Be_Hit_v = 2.0f;
    private float HitThunder_v = 2.0f;
    private float HitFireBall_v = 2.0f;
    void Start()
    {
        //������
        Search_Enemy = SearchErea.GetComponent<SearchErea>();
        Anim = GetComponent<Animator>();
        Local_Space_Vec = Vector3.forward;
        rd = GetComponent<Rigidbody>();
        GroundLayer = LayerMask.GetMask("Ground");
        //�v���C���[�ɋ߂Â��ϐ�
        MainCharacter = GameObject.FindGameObjectWithTag("Player");
        if (MainCharacter != null)
        {
            Goal_Position = MainCharacter.transform.position;
        }
        //�ԗւ̓G��������X�s�[�h��6�A����ȊO��������
        if(gameObject.tag == "WheellEnemy")
        {
            Speed_Enemy = 6.0f;
        }
        else
        {
              Speed_Enemy = 5.0f;
        }
           
       
        Turn = false;//true�Ȃ�Ή�]
        Local_Space_Vec = Vector3.up;
        //�U���֘A�̕ϐ�
        Cool_Time = 5;
        Damede_Hit = false;
        //�T���֘A�̕ϐ�
        Mode_Serch = false;
        Search_Position_Right.x = this.transform.position.x + 10;
        Search_Position_Right.y = this.transform.position.y;
        Search_Position_Right.z = this.transform.position.z;
        Search_Position_Left.x = this.transform.position.x - 10;
        Search_Position_Left.y = this.transform.position.y;
        Search_Position_Left.z = this.transform.position.z;
        Right_Or_Left = false;//false�Ȃ�ΉE,true�Ȃ�΍�
        Around_Position = 9.9f;
        Initial_Value.x = this.transform.position.x;
        Initial_Value.y = this.transform.position.y;
        Initial_Value.z = this.transform.position.z;
        Time_Lapse = 0;
        Return = 5;
    }

    public void Update()
    {
        if (Time.timeScale == 0)
        {
            return;
        }

        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //�n�ʂɂ��Ă��邩�̃`�F�b�N
        if (gameObject.tag == "Enemy" || gameObject.tag == "WheellEnemy")
        {
            if (IsGrond())
            {
                if (!OnGround)
                {
                    rd.linearVelocity = Vector3.zero;
                    OnGround = true;
                }
            }
            else
            {
                OnGround = false;
                rd.AddForce(0, -100, 0);
            }
        }

        //������̐���
        if (Initial_Value.y < this.transform.position.y  && gameObject.tag == "FlyEnemy")
        {
            Debug.Log("������Ɉړ����Ă��邽�ߒ��~");
            rd.linearVelocity = Vector3.zero;

            if (this.transform.position.y > 1)
            {
                Debug.Log("���~");
                rd.AddForce(0,-50,0);
            } 
        }


        //����
        if (OnGround)
        {
            if (Search_Enemy.Find && !Not_Move)
            {
                Discovery();
                //AS.PlayOneShot(Find);
                float time = 0;
                time += (float)Time.deltaTime;
            }

            //�v���C���[��������
            if (!Search_Enemy.Find)
            {
                Debug.Log("���������P");
                Lost();
            }
        }
    }


    //��l�������������̊֐�
    public void Discovery()
    {
        Time_Lapse = 0;
        //MainCharacter.transform.position = 

        float time = 0;
        time += (float)Time.deltaTime;
        Debug.Log("������");
        Time_Lapse = 0;
        if(gameObject.tag == "Enemy")
        {
           Anim.SetBool("Walk", true);
           time = 0;

        }
        else if (gameObject.tag == "WheellEnemy")
        {
            Anim.SetBool("Walk_1", true);
        }
        else if (gameObject.tag == "FlyEnemy")
        {
           
        }

        Vector3 Distance = MainCharacter.transform.position - ModelRoot.transform.position;
        
            Distance = new Vector3(Distance.x, 0, Distance.z);

            Quaternion Rotation = Quaternion.LookRotation(new Vector3(Distance.z,0,Distance.x * -1));

        //Vector3 rot = Rotation.eulerAngles;

        //transform.rotation = Rotation;

        x = Rotation.x;
        y = Rotation.y;
        z = Rotation.z;
        w = Rotation.w;
        if (Rotation.y <= 1)
        {
            Rotation = new Quaternion(Rotation.x, Rotation.y, Rotation.z, Rotation.w);
        }
        else
        {
            Rotation = new Quaternion(Rotation.x, Rotation.y, Rotation.z, Rotation.w);
        }

        //Rotation = new Quaternion(Rotation.x, Rotation.y + 0.5f, Rotation.z, Rotation.w);

        transform.rotation = Rotation;

            Debug.Log("����");
        

        Distance.Normalize();
        //�ڕW���_�܂ňړ�����iGoal_Position�̒l��Player�̍��W�ɂ����Player�Ɍ������j
        this.rd.linearVelocity = Distance * Speed_Enemy;


        Mode_Serch = true;
    }

    //��l�����������������̊֐�
    public void Lost()
    {
        //�ړ����~�߂�
        if (gameObject.tag == "FlyEnemy")
        {
            rd.linearVelocity = Vector3.zero;
        }
        Time_Lapse += Time.deltaTime;
        //�A�j���[�V�����؂�ւ��i�~�܂�j
        if(gameObject.tag == "Enemy")
        {
           Anim.SetBool("Walk", false);
            if (!AttackErea.Find)
            {
                Anim.SetBool("Attack", false);
            }
        }
        else if (gameObject.tag == "WheellEnemy")
        {
            Anim.SetBool("Walk_1", false);
            if (!AttackErea.Find)
            {
                Anim.SetBool("Attack_1", false);
            }
        }
        else if (gameObject.tag == "FlyEnemy")
        {
            if (!AttackErea.Find)
            {
                Anim.SetBool("Attack_2", false);
            }
        }

        if (Time_Lapse > Return && Mode_Serch == true)
        {
            Debug.Log("�߂�");
            //�����l�ɖ߂�
            transform.position = Initial_Value;
            Time_Lapse = 0;
            Mode_Serch = false;
        }
    }

    //�U�����󂯂���
    private void OnTriggerEnter(Collider collision)
    {
        int combo = 0;
        if (collision.gameObject.name == "GC")
        {
            var Enemy = GetComponent<Enemy_Status>();
            combo = 1;
            //�G���߂Â�������
            if(gameObject.tag == "Enemy" || gameObject.tag == "WheellEnemy")
            {
                var D = MainCharacter.transform.position - ModelRoot.transform.position;
                D.Normalize();
                rd.linearVelocity = D;
            }
            Enemy.Be_Attack(combo);
            Debug.Log("hit,C");
            //�U�����󂯂�SE�𗬂�
            AS.PlayOneShot(BeHit_SE);
            AS.volume = Be_Hit_v;
        }

        if (collision.gameObject.name == "GF")
        {
            var Enemy = GetComponent<Enemy_Status>();
            combo = 2;
            if(gameObject.tag == "Enemy" || gameObject.tag == "WheellEnemy")
            {
                var D = MainCharacter.transform.position - ModelRoot.transform.position;
                D.Normalize();
                rd.linearVelocity = D;
            }
            Enemy.Be_Attack(combo);
            Debug.Log("hit,F");
            //�U�����󂯂�SE�𗬂�
            AS.PlayOneShot(BeHit_SE);
            AS.volume = Be_Hit_v;
        }

        string Skill = "";
        //�X�L�����󂯂���
        //�t�@�C���[�{�[��
        if(collision.gameObject.name == "Explosion")
        {
            var Enemy = GetComponent<Enemy_Status>();
            Skill = "FireBall";
            Enemy.Be_Skill(Skill);
            Debug.Log("hit,FB");
            //HitSE
            AS.PlayOneShot(HitFireBall_SE);
            AS.volume = HitFireBall_v;
        }
        //�T���_�[
        if (collision.gameObject.name == "Lightning")
        {
            var Enemy = GetComponent<Enemy_Status>();
            Skill = "Thunder";
            Enemy.Be_Skill(Skill);
            Debug.Log("hit,Th");
            Not_Move = true;
            Invoke(nameof(Mobile), 2.0f);
            //SE
            AS.PlayOneShot(HitThunder_SE);
            AS.volume = HitThunder_v;
        }
    }

    //�n�ʔ���
    bool IsGrond()
    {
        return Physics.Raycast(transform.position, Vector3.down, Ground_Distance, GroundLayer);
    }
    
    //�ړ��\�ɂ���֐�
   void  Mobile()
   {
        Not_Move = false;
   }
}

