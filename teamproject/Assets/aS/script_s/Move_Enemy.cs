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
    public static Move_Enemy instance;
    public bool Attack_Enemy;//�G�̍U���̔���
    public bool Damede_Hit;
    public int Combo_Number;

    [SerializeField] GameObject MainCharacter;
    [SerializeField] GameObject Erea;//SearchErea�̃X�N���v�g���Ăяo��GameObject
    [SerializeField] SearchErea Search_Enemy;//��l����T���ϐ�
   
    private float Speed_Enemy;//�X�s�[�h�̕ϐ�
    private float  Time_Lapse;//�G����l�������������Ƃ��̌o�ߎ���
    private int Return;//�G�������l�ɖ߂鎞�Ԃ̕ϐ�
    private float Attack_Enemy_Time;//�G�̍U���̃N�[���^�C��
    private int Cool_Time;//�U���̃N�[������
    private bool Mode_Serch;//�T�����[�h���̔���
    private bool  Right_Or_Left;//���񎞂̉E�������̔���
    private float Around_Position;
    private bool Turn;//��]�̕ϐ�
    private bool Be_Attacked;//�U�����󂯂����̔���
    private InputAction Player_Attack;


    Vector3 Goal_Position;//�ڕW���_�̍��W�ϐ��i�G���G�j
    Vector3 Initial_Value;//�����n�_�̍��W�ϐ�
    Vector3 Search_Position_Right;//���񂷂�p�̃x�N�g���E
    Vector3 Search_Position_Left;//���񂷂�p�̃x�N�g����
    Vector3 Player_Distance;//�v���C���[�̋����Ԃ̃x�N�g��
    [SerializeField] Vector3 Local_Space_Vec;//�O����̃��[�J����ԃx�N�g��
   

    void Start()
    {
        //������
        //�v���C���[�ɋ߂Â��ϐ�
        Goal_Position = MainCharacter.transform.position;
        Speed_Enemy = 2.0f;
        MainCharacter = GameObject.FindWithTag("Player");
        Erea = GameObject.Find("SearchErea");
        Search_Enemy = Erea.GetComponent<SearchErea>();
        Turn = false;//true�Ȃ�Ή�]
        Local_Space_Vec = Vector3.up;
        Player_Distance = MainCharacter.transform.position - this.transform.position;
        //�U���֘A�̕ϐ�
        Attack_Enemy = false;
        Attack_Enemy_Time = 0;
        Cool_Time = 5;
        Damede_Hit = false;
        Be_Attacked = PlayerControllerTest_s.instance.T_Attack;
        Player_Attack = InputSystem.actions.FindAction("Attack");

        //�T���֘A�̕ϐ�
        Mode_Serch = false;
        Search_Position_Right = new Vector3(this.transform.position.x + 10, (float)0.75, 0);
        Search_Position_Left = new Vector3(this.transform.position.x - 10, (float)0.75, 0);
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
        if(Time.timeScale == 0)
        {
            return;
        }


    }

    // Update is called once per frame
    void FixedUpdate()
    {
       

        Attack_Enemy_Time += Time.deltaTime;

        Around();

        //��������v���C���[�ɋ߂Â�
        if (Search_Enemy.Discovery_Main)
        { 
            Discovery();
        }
        //���������珉���l�ɖ߂�
        else if(!Search_Enemy.Discovery_Main && Search_Enemy.First_Time)
        {
            Lost(); 
        }
        //�G�̃A�^�b�N
        if(Attack_Enemy_Time >=  Cool_Time)
        {
            Attack();
        }

    } 

    //���񂷂�֐�
    void Around()
    {
        
        if (!Search_Enemy.Discovery_Main)
        {
            
            if(!Right_Or_Left)
            {
               transform.position = Vector3.MoveTowards(transform.position, Search_Position_Right, Speed_Enemy * Time.deltaTime); 
            //�ڕW�n�_�ɂ�����A���̏�ŉ�]���t�����ɍs��
              if(transform.position.x >= Around_Position)
              {
                    Right_Or_Left = true;
                    transform.Rotate(0, 180, 0);
              }
            }

            //�t�����ɍs��
            else if (Right_Or_Left)
            {
                transform.position = Vector3.MoveTowards(transform.position, Search_Position_Left, Speed_Enemy * Time.deltaTime);

                if(transform.position.x <= -Around_Position)
                {
                    transform.Rotate(0, 180, 0);
                    Right_Or_Left = false;
                }
                
            }
          
           
        }
    }

    //��l�������������̊֐�
    void Discovery()
    {
        

        //Debug.Log("������");
        Time_Lapse = 0;
        Turn = true;
        if(Turn)
        {
            //��l���̕����ɉ�]
            var Rotate_Discovery = Quaternion.LookRotation(Player_Distance,Vector3.up); //�v���C���[�����̉�]�x�N�g��
            var Rotate_Correction = Quaternion.FromToRotation(Local_Space_Vec, Vector3.up);


            //��]�␳
            this.transform.rotation = Rotate_Discovery * Rotate_Correction;

            //Debug.Log("����");  
        }
        //�ڕW���_�܂ňړ�����iGoal_Position�̒l��Player�̍��W�ɂ����Player�Ɍ������j
        transform.position = Vector3.MoveTowards(transform.position, Goal_Position, Speed_Enemy * Time.deltaTime);

        Mode_Serch = true;
    }

    //��l�����������������̊֐�
    void Lost()
    {
        Time_Lapse += Time.deltaTime;

        if(Time_Lapse > Return && Mode_Serch == true)
        {
            //�����l�ɖ߂�
            transform.position = Initial_Value;
            Time_Lapse = 0;
            Mode_Serch = false;
        }
    }

    //�G�̍U���̊֐�
    void Attack()
    {
        Attack_Enemy = true;
        Attack_Enemy_Time = 0.0f;
    }

    //�U�����󂯂�
    public void OnCollisionEnter(Collision other)
    {

        if(other.gameObject.tag == "Player"&& Be_Attacked)
        {
            Debug.Log("�q�b�g");
            Damede_Hit = true;
        }
    }
}               