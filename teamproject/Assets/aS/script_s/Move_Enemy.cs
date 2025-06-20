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

    public bool Attack_Enemy;//�G�̍U���̔���
    public static bool Damede_Hit;

    [SerializeField] GameObject MainCharacter;
    [SerializeField] GameObject Erea;//SearchErea�̃X�N���v�g���Ăяo��GameObject
    [SerializeField] SearchErea Search_Enemy;//��l����T���ϐ�
    [SerializeField] private Transform ModelRoot;// �� ���f���iJR-1�j���w�肷��p

    private float Speed_Enemy;//�X�s�[�h�̕ϐ�
    private float Time_Lapse;//�G����l�������������Ƃ��̌o�ߎ���
    private int Return;//�G�������l�ɖ߂鎞�Ԃ̕ϐ�
    private float Attack_Enemy_Time;//�G�̍U���̃N�[���^�C��
    private int Cool_Time;//�U���̃N�[������
    private bool Mode_Serch;//�T�����[�h���̔���
    private bool Right_Or_Left;//���񎞂̉E�������̔���
    private float Around_Position;
    private bool Turn;//��]�̕ϐ�
    private bool Be_Attacked;//�U�����󂯂����̔���
    private Animator Anim;//�A�j���[�V�����R���|�[�l���g�̎擾


    Vector3 Goal_Position;//�ڕW���_�̍��W�ϐ��i�G���G�j
    Vector3 Initial_Value;//�����n�_�̍��W�ϐ�
    Vector3 Search_Position_Right;//���񂷂�p�̃x�N�g���E
    Vector3 Search_Position_Left;//���񂷂�p�̃x�N�g����
    Vector3 Player_Distance;//�v���C���[�̋����Ԃ̃x�N�g��
    [SerializeField] Vector3 Local_Space_Vec;//�O����̃��[�J����ԃx�N�g��


    void Start()
    {
        //������
        Search_Enemy = GetComponentInChildren<SearchErea>();
        Anim = GetComponent<Animator>();
        //�v���C���[�ɋ߂Â��ϐ�
        //MainCharacter = GameObject.FindWithTag("Player");
        Goal_Position = MainCharacter.transform.position;
        Speed_Enemy = 2.0f;
        Turn = false;//true�Ȃ�Ή�]
        Local_Space_Vec = Vector3.up;
        Player_Distance = MainCharacter.transform.position - ModelRoot.position;
        //�U���֘A�̕ϐ�
        Attack_Enemy = false;
        Attack_Enemy_Time = 0;
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

        //����
        if(Search_Enemy.Find)
        {
            Discovery();
        }
        //�v���C���[��������
        if (!Search_Enemy.Find && Search_Enemy.FirstTime)
        {
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
        Turn = true;

      
        //�v���C���[�̈ʒu���擾���ĕ����X�V
        Player_Distance = MainCharacter.transform.position - ModelRoot.position;
        if (Turn)
        {
            //�A�j���[�V�����؂�ւ�
            Anim.SetBool("Walk", true);
            //��l���̕����ɉ�]
            var Rotate_Discovery = Quaternion.LookRotation(Player_Distance.normalized, Vector3.up); //�v���C���[�����̉�]�x�N�g��
            ModelRoot.rotation = Rotate_Discovery;


            //Debug.Log("����");  
        }
        //Goal�ʒu�̍X�V
        Goal_Position = MainCharacter.transform.position;
        //�ڕW���_�܂ňړ�����iGoal_Position�̒l��Player�̍��W�ɂ����Player�Ɍ������j
        ModelRoot.position = Vector3.MoveTowards(ModelRoot.position, Goal_Position, Speed_Enemy * Time.deltaTime);

        Mode_Serch = true;
    }

    //��l�����������������̊֐�
    public void Lost()
    {
        Time_Lapse += Time.deltaTime;
        //�A�j���[�V�����؂�ւ��i�~�܂�j
        Anim.SetBool("Walk", false);

        if (Time_Lapse > Return && Mode_Serch == true)
        {
            Debug.Log("�߂�");
            //�����l�ɖ߂�
            ModelRoot.position = Initial_Value;
            Time_Lapse = 0;
            Mode_Serch = false;
        }
    }

    //�U�����󂯂���
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == ("Player"))
        {
            Debug.Log("�U�����󂯂�");
            var Enemy = GetComponent<Enemy_Status>();
            if(Enemy != null)
            {
               string combo = PlayerController_y.instance.AttackState;
               Enemy.Be_Attack(combo);
            }
        }
    }
}