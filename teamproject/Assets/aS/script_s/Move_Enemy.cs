using System.Security.Cryptography;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class Move_Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject MainCharacter;
    public GameObject Erea;//SearchErea�̃X�N���v�g���Ăяo��GameObject
    public SearchErea Search_Enemy;//��l����T���ϐ�

    private float Jump_Enemy;//�W�����v�̕ϐ�
    private float Speed_Enemy;//�X�s�[�h�̕ϐ�
    private float  Time_Lapse;//�G����l�������������Ƃ��̌o�ߎ���
    private int Return;//�G�������l�ɖ߂鎞�Ԃ̕ϐ�
    private Rigidbody Rigidbody_Enemy;
    private bool Attack_Enemy;//�G�̍U���̔���
    private float Attack_Enemy_Time;//�G�̍U���̃N�[���^�C��
    private int Cool_Time;//�U���̃N�[������

    Vector3 Goal_Position;//�ڕW���_�̍��W�ϐ��i�G���G�j
    Vector3 Initial_Value;//�����n�_�̍��W�ϐ�
    Vector3 Search_Posiotion_Front;//���񂷂�p�̃x�N�g���E
    Vector3 Search_Posiotion_Back;//���񂷂�p�̃x�N�g����

    void Start()
    {
        //������
        Goal_Position = new Vector3(10, (float)0.75, 10);
        Speed_Enemy = 2.0f;
        Erea = GameObject.Find("SearchErea");
        Search_Enemy = Erea.GetComponent<SearchErea>();
        Time_Lapse = 0;
        Return = 5;
        Initial_Value = new Vector3(0,(float)2.08,0);
        Rigidbody_Enemy = GetComponent<Rigidbody>();
        Attack_Enemy = false;
        Attack_Enemy_Time = 0;
        Cool_Time = 5;
        Search_Posiotion_Front = new Vector3(10, (float)0.75, 0);
        Search_Posiotion_Back = new Vector3(-10, (float)0.75, 0);
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
            Debug.Log("����");
           
        }
    }

    //��l�������������̊֐�
    void Discovery()
    { 
        Time_Lapse = 0;
        //�ڕW���_�܂ňړ�����iGoal_Position�̒l��Player�̍��W�ɂ����Player�Ɍ������j
        transform.position = Vector3.MoveTowards(transform.position, Goal_Position, Speed_Enemy * Time.deltaTime);
    }

    //��l�����������������̊֐�
    void Lost()
    {
        Time_Lapse += Time.deltaTime;

        if(Time_Lapse > Return)
        {
            //�����l�ɖ߂�
            transform.position = Initial_Value;
            Time_Lapse = 0;
        }
    }

    //�G�̍U���̊֐�
    void Attack()
    {
        Attack_Enemy = true;
        Attack_Enemy_Time = 0.0f;
    }
}