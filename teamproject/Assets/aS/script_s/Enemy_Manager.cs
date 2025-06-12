using System.ComponentModel;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
   
    [SerializeField] GameObject Enemy_Object;//敵のタグを見つける変数
    [SerializeField] Move_Enemy Enemy;//敵のスクリプト取得変数
    [SerializeField] GameObject Player;//プレイヤーのタグを見つける変数
    [SerializeField] PlayerController_y Player_Script;//プレイヤーのスクリプト取得変数

    private bool Enemy_Die;
    private int Enemy_Number;
    private string Player_Combo;//プレイヤーのコンボ状況を確認するスクリプト
    

    public int Enemy_HP;
    public int Enemy_Power;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Enemy_HP = 100;//仮数値
        Enemy_Die = false;//trueで消える
        Enemy_Number = 10;//仮敵の数
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
            Debug.Log("攻撃を受けた");
            Be_Attack(PlayerController_y.instance.AttackState);
        }

       if(Enemy_HP < 0)
        {
            After_Die();
        }
    }

    //雑魚敵の攻撃を受けた処理の関数
    void Be_Attack(string Combo)
    {
        //コンボ一段階目
       if(Combo == "GroundFirst" || Combo == "AirFirst" )
       {
            Enemy_HP -= 1;
       }
       //コンボ二段目
       if(Combo == "GroundSecond" || Combo == "AirSecond")
       {
            Enemy_HP -= 2;
       }
       //コンボ三段目
      if (Combo == "GroundThird" || Combo == "AirThird")
      {
            Enemy_HP -= 3;
      }
      //コンボ四段目
      if(Combo == "GroundFinish" || Combo == "AirFinish")
      {
            Enemy_HP -= 4;
      }

        
    }

    //雑魚敵が死んだ後の処理
    void After_Die()
    {
        Destroy(Enemy_Object);
    }
}
