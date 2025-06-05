using System.ComponentModel;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
   
   [SerializeField] GameObject Enemy_Object;//�G�̃^�O��������ϐ�
    [SerializeField] Move_Enemy Enemy;//�G�̃X�N���v�g�擾�ϐ�

    private int Enemy_HP;
    private int Enemy_Power;
    private bool Enemy_Die;
    private int Enemy_Number;
    



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Enemy_HP = 11;//�����l
        Enemy_Die = false;//true�ŏ�����
        Enemy_Number = 10;//���G�̐�
        Enemy_Object = GameObject.FindWithTag("Enemy");
        Enemy = Enemy_Object.GetComponent<Move_Enemy>();
    }
    


    // Update is called once per frame
    void FixedUpdate()
    {
        if(Enemy.Combo_Number > 0)
        {
            Be_Attack();
        }

        if(Enemy_HP <= 0)
        {
             Debug.Log("������");
             Enemy_Die = true; 
             if(Enemy_Die)
             {
                After_Die();
             }
        }
       
    }

    //�G���G�̍U�����󂯂������̊֐�
    void Be_Attack()
    {
        switch(Enemy.Combo_Number)
        {
            //�R���{�͂���
            case 1:
                Enemy_HP -= 1;
                break;
            case 2:
                Enemy_HP -= 1;
                break;
            //�R���{2�Ԗ�
            case 3:
                Enemy_HP -= 2;
                break;
            case 4:
                Enemy_HP -= 2;
                break;
            //�R���{�R�Ԗ�
            case 5:
                Enemy_HP -= 3;
                break;
            case 6:
                Enemy_HP -= 3;
                break;
            //�R���{�ŏI
            case 7:
                Enemy_HP -= 4;
                break;
            case 8:
                Enemy_HP -= 4;
                break;
        }
    }

    //�G���G�����񂾌�̏���
    void After_Die()
    {
        Destroy(Enemy);
    }  
}
