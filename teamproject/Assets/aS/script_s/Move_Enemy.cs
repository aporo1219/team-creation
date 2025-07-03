using System.Security.Cryptography;
using Unity.Cinemachine;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;



public class Move_Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static bool Damede_Hit;

    [SerializeField] float x, y, z, w;

    [SerializeField] GameObject MainCharacter;
    [SerializeField] SearchErea Search_Enemy;//��l����T���ϐ�
    [SerializeField] private Transform ModelRoot;// �� ���f���iJR-1�j���w�肷��p
    [SerializeField] private float RotationOffsetY = 0f;//�����̕␳
    [SerializeField] private GameObject[] ModelPrefabs;
    

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
    private Rigidbody rd;
    

    Vector3 Goal_Position;//�ڕW���_�̍��W�ϐ��i�G���G�j
    Vector3 Initial_Value;//�����n�_�̍��W�ϐ�
    Vector3 Search_Position_Right;//���񂷂�p�̃x�N�g���E
    Vector3 Search_Position_Left;//���񂷂�p�̃x�N�g����
  


    [SerializeField] Vector3 Local_Space_Vec;//�O����̃��[�J����ԃx�N�g��


    void Start()
    {
        //������
        Search_Enemy = GetComponentInChildren<SearchErea>();
        Anim = GetComponent<Animator>();
        Local_Space_Vec = Vector3.forward;
        rd = GetComponent<Rigidbody>();
        //�v���C���[�ɋ߂Â��ϐ�
        MainCharacter = GameObject.FindGameObjectWithTag("Player");
        if(MainCharacter != null)
        {
            Goal_Position = MainCharacter.transform.position;
        }
      
        Speed_Enemy = 2.0f;
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
        //Around();

        //������̐���
        /*bool isMovingUp = Vector3.Dot(rd.linearVelocity.normalized, Vector3.up) > 0.7f;

        if (isMovingUp && gameObject.tag == "FlyEnemy")
        {
                Debug.Log("������Ɉړ����Ă��邽�ߒ��~");
                this.rd.linearVelocity = Initial_Value;
                return;
        }*/
        

        //����
        if (Search_Enemy.Find)
        {
            Discovery();
        }
        //�v���C���[��������
        if (!Search_Enemy.Find)
        {
            Debug.Log("���������P");
            Lost();
        }
    }

    //���񂷂�֐�
    /*void Around()
    {
        if (!Search_Enemy.Find)
        {
            if (!Right_Or_Left)
            {
                transform.position = Vector3.MoveTowards(transform.position, Search_Position_Right, Speed_Enemy * Time.deltaTime);
                //�ڕW�n�_�ɂ�����A���̏�ŉ�]���t�����ɍs��
                if (transform.position.x >= Around_Position)
                {
                    Around_Position = -8.0f;
                    Right_Or_Left = true;
                    ModelRoot.Rotate(0, , 0);
                }
            }

            //�t�����ɍs��
            else if (Right_Or_Left)
            {
                Debug.Log("��]");
                transform.position = Vector3.MoveTowards(transform.position, Search_Position_Left, Speed_Enemy * Time.deltaTime);

                if (transform.position.x <= Around_Position)
                {
                    Around_Position = 9.9f;
                    Debug.Log("��]1");
                    ModelRoot.Rotate(0, 180, 0);
                    Right_Or_Left = false;
                }

            }
        }
    }*/

    //��l�������������̊֐�
    public void Discovery()
    {
        Time_Lapse = 0;
        //MainCharacter.transform.position = 

        Debug.Log("������");
        Time_Lapse = 0;
        if(gameObject.tag == "Enemy")
        {
           Anim.SetBool("Walk", true);
        }
        else if (gameObject.tag == "WheellEnemy")
        {
            Anim.SetBool("Walk_1", true);
        }
       

        Vector3 Distance = MainCharacter.transform.position - ModelRoot.transform.position;
        
            Distance = new Vector3(Distance.x, 0, Distance.z);

            Quaternion Rotation = Quaternion.LookRotation(Distance);

        //Vector3 rot = Rotation.eulerAngles;

        //transform.rotation = Rotation;

        x = Rotation.x;
        y = Rotation.y;
        z = Rotation.z;
        w = Rotation.w;
        if (Rotation.y <= 1)
        {
            Rotation = new Quaternion(Rotation.x, Rotation.y + 0.5f, Rotation.z, Rotation.w);
        }
        else
        {
            Rotation = new Quaternion(Rotation.x, Rotation.y - 0.5f, Rotation.z, Rotation.w);
        }

        //Rotation = new Quaternion(Rotation.x, Rotation.y + 0.5f, Rotation.z, Rotation.w);

        transform.rotation = Rotation;

            Debug.Log("����");
        

        Distance.Normalize();
        //�ڕW���_�܂ňړ�����iGoal_Position�̒l��Player�̍��W�ɂ����Player�Ɍ������j
        this.rd.linearVelocity = Distance;


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
           Anim.SetBool("Attack", false);
        }
        else if (gameObject.tag == "WheellEnemy")
        {
            Anim.SetBool("Walk_1", false);
            Anim.SetBool("Attack_1", false);
        }
        else if (gameObject.tag == "FlyEnemy")
        { 
            Anim.SetBool("Attack_2", false);
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
        if (collision.gameObject.name == "GC")
        {
            var Enemy = GetComponent<Enemy_Status>();
            int combo = PlayerController_y1.instance.AttackNum;
            //�G�𕂂���
            if(gameObject.tag == "Enemy" || gameObject.tag == "WheellEnemy")
            {
              rd.linearVelocity = new Vector3(0, 5, 0);
            }
            Enemy.Be_Attack(combo);
            Debug.Log("hit,C");
        }

        if (collision.gameObject.name == "GF")
        {
            var Enemy = GetComponent<Enemy_Status>();
            int combo = PlayerController_y1.instance.AttackNum;
            if(gameObject.tag == "Enemy" || gameObject.tag == "WheellEnemy")
            {
                rd.linearVelocity = new Vector3(0, 10, 0);
            }
            Enemy.Be_Attack(combo);
            Debug.Log("hit,F");
        }
    }
}

