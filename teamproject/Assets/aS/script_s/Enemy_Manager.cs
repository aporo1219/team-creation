using System.ComponentModel;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
   
    [SerializeField] GameObject Enemy_Object;//�G�̃^�O��������ϐ�
    [SerializeField] Move_Enemy Enemy;//�G�̃X�N���v�g�擾�ϐ�
    [SerializeField] GameObject Player;//�v���C���[�̃^�O��������ϐ�
    [SerializeField] PlayerController_y Player_Script;//�v���C���[�̃X�N���v�g�擾�ϐ�

    private bool Enemy_Die;
    private int Enemy_Number;
    private string Player_Combo;//�v���C���[�̃R���{�󋵂��m�F����X�N���v�g
    

    public int Enemy_HP;
    public int Enemy_Power;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Enemy_HP = 100;//�����l
        Enemy_Die = false;//true�ŏ�����
        Enemy_Number = 10;//���G�̐�
        Enemy_Object = GameObject.FindWithTag("Enemy");
        Enemy = Enemy_Object.GetComponent<Move_Enemy>();
        Player = GameObject.FindWithTag("Player");
        Player_Script = Player.GetComponent<PlayerController_y>();
        for(int i = 0;i<5;i++)
        {
            Instantiate(Enemy_Object);
        }
    }
    


    // Update is called once per frame
    void FixedUpdate()
    {
        
       if(Move_Enemy.Damede_Hit)
        {
            Debug.Log("�U�����󂯂�");
            Be_Attack(PlayerController_y.instance.AttackState);
        }

       if(Enemy_HP < 0)
        {
            After_Die();
        }
    }

    //�G���G�̍U�����󂯂������̊֐�
    void Be_Attack(string Combo)
    {
        //�R���{��i�K��
       if(Combo == "GroundFirst" || Combo == "AirFirst" )
       {
            Enemy_HP -= 1;
       }
       //�R���{��i��
       if(Combo == "GroundSecond" || Combo == "AirSecond")
       {
            Enemy_HP -= 2;
       }
       //�R���{�O�i��
      if (Combo == "GroundThird" || Combo == "AirThird")
      {
            Enemy_HP -= 3;
      }
      //�R���{�l�i��
      if(Combo == "GroundFinish" || Combo == "AirFinish")
      {
            Enemy_HP -= 4;
      }

        
    }

    //�G���G�����񂾌�̏���
    void After_Die()
    {
        Destroy(Enemy_Object);
    }
}
