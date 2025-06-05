using System.ComponentModel;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
   
   [SerializeField] GameObject Enemy_Object;//敵のタグを見つける変数
    [SerializeField] Move_Enemy Enemy;//敵のスクリプト取得変数

    private int Enemy_HP;
    private int Enemy_Power;
    private bool Enemy_Die;
    private int Enemy_Number;
    



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Enemy_HP = 11;//仮数値
        Enemy_Die = false;//trueで消える
        Enemy_Number = 10;//仮敵の数
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
             Debug.Log("消えた");
             Enemy_Die = true; 
             if(Enemy_Die)
             {
                After_Die();
             }
        }
       
    }

    //雑魚敵の攻撃を受けた処理の関数
    void Be_Attack()
    {
        switch(Enemy.Combo_Number)
        {
            //コンボはじめ
            case 1:
                Enemy_HP -= 1;
                break;
            case 2:
                Enemy_HP -= 1;
                break;
            //コンボ2番目
            case 3:
                Enemy_HP -= 2;
                break;
            case 4:
                Enemy_HP -= 2;
                break;
            //コンボ３番目
            case 5:
                Enemy_HP -= 3;
                break;
            case 6:
                Enemy_HP -= 3;
                break;
            //コンボ最終
            case 7:
                Enemy_HP -= 4;
                break;
            case 8:
                Enemy_HP -= 4;
                break;
        }
    }

    //雑魚敵が死んだ後の処理
    void After_Die()
    {
        Destroy(Enemy);
    }  
}
