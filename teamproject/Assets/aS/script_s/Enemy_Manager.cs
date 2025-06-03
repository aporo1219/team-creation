using System.ComponentModel;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
   
   [SerializeField] GameObject Enemy;

    private int Enemy_HP;
    private int Enemy_Power;
    private bool Enemy_Die;
    private int Enemy_Number;
    private string Player_Attack;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Enemy_HP = 10;//�����l
        Enemy_Die = false;//true�ŏ�����
        Enemy_Number = 10;//�G�̐�
        Player_Attack = PlayerController_y.instance.AttackState;
    }


    private void OnCollisionEnter(Collision collision)
    {
        
    }


    // Update is called once per frame
    void FixedUpdate()
    {
            Debug.Log("������");
            Enemy_Die = true; 
            if(Enemy_Die)
            {
              After_Die();
            }
    }


    //�G���G�����񂾌�̏���
    void After_Die()
    {
        Destroy(Enemy);
    }  
}
