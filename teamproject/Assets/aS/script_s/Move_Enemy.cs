using System.Security.Cryptography;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class Move_Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject MainCharacter;
    
    private float Jump_Enemy;//�W�����v�̕ϐ�
    private float Speed_Enemy;//�X�s�[�h�̕ϐ�
 �@ private bool Search_Enemy;//��l����T���ϐ�
    private bool Discovery;
  

    Rigidbody Rigidbody_Enemy;
    Vector3 Goal_Position;//�ڕW���_�̍��W�ϐ��i�G���G�j
    Vector3 Main;
    float Distance;

    void Start()
    {
        //������
        Goal_Position = new Vector3(4,(float) 0.75, 4);
        Rigidbody_Enemy = GetComponent<Rigidbody> ();
        Speed_Enemy = 10.0f;
        Discovery = SearchErea.Discovery_Main;
        
    }

    

    // Update is called once per frame
    void FixedUpdate()
    {
        //�ڕW���_�܂ňړ�����iGoal_Position�̒l��Player�̍��W�ɂ����Player�Ɍ������j
        transform.position = Vector3.MoveTowards(transform.position, Goal_Position, Time.deltaTime);
       
        //�e�X�g�p�̃W�����v
        /*if (Input.GetKey(KeyCode.UpArrow))
        {
            Rigidbody_Enemy.AddForce(transform.up * Speed_Enemy);
        }*/

        if(Discovery == true)
        {
            Debug.Log("������");
        }
    }

    //�v���C���[�̋󒆑ł��グ�U�����������Ƃ��ɔ���
    void OnCollisionEnter(Collision collision)
    {
        
    }

    //�̈�̊֐�
    void Search()
    {
       
    }
}
