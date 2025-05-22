using System.Security.Cryptography;
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
 



    Rigidbody Rigidbody_Enemy;
    Vector3 Goal_Position;//�ڕW���_�̍��W�ϐ��i�G���G�j
    Vector3 Main;
    float Distance;

    void Start()
    {
        //������
        Goal_Position = new Vector3(4, (float)0.75, 4);
        //Rigidbody_Enemy = GetComponent<Rigidbody>();
        Speed_Enemy = 10.0f;
        Erea = GameObject.Find("SearchErea");
        Search_Enemy = Erea.GetComponent<SearchErea>();

    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if (Search_Enemy.Discovery_Main)
        {
           Discovery();

        }
        else if(!Search_Enemy.Discovery_Main)
        {
            Lost();
        }

    } 
    //�v���C���[�̋󒆑ł��グ�U�����������Ƃ��ɔ���
    void OnCollisionEnter(Collision collision)
    {

    }

    //��l�������������̊֐�
    void Discovery()
    { 
        //�ڕW���_�܂ňړ�����iGoal_Position�̒l��Player�̍��W�ɂ����Player�Ɍ������j
        transform.position = Vector3.MoveTowards(transform.position, Goal_Position, Time.deltaTime);
        //Debug.Log("������");
    }

    void Lost()
    {
        transform.position = new Vector3(0, (float)0.75, 0);
        Debug.Log("��������");
    }
}