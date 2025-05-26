using System.Security.Cryptography;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class Move_Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject MainCharacter;
    public GameObject Erea;//SearchEreaのスクリプトを呼び出すGameObject
    public SearchErea Search_Enemy;//主人公を探す変数

    private float Jump_Enemy;//ジャンプの変数
    private float Speed_Enemy;//スピードの変数
    private float  Time_Lapse;//敵が主人公を見失ったときの経過時間
    private int Return;//敵が初期値に戻る時間の変数
    private Rigidbody Rigidbody_Enemy;
    private bool Attack_Enemy;//敵の攻撃の判定
    private float Attack_Enemy_Time;//敵の攻撃のクールタイム
    private int Cool_Time;//攻撃のクール時間

    Vector3 Goal_Position;//目標時点の座標変数（雑魚敵）
    Vector3 Initial_Value;//初期地点の座標変数
    Vector3 Search_Posiotion_Front;//周回する用のベクトル右
    Vector3 Search_Posiotion_Back;//周回する用のベクトル左

    void Start()
    {
        //初期化
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

        //見つけたらプレイヤーに近づく
        if (Search_Enemy.Discovery_Main)
        { 
            Discovery();
        }
        //見失ったら初期値に戻す
        else if(!Search_Enemy.Discovery_Main && Search_Enemy.First_Time)
        {
            Lost(); 
        }
        //敵のアタック
        if(Attack_Enemy_Time >=  Cool_Time)
        {
            Attack();
        }

    } 

    //周回する関数
    void Around()
    {
        if (!Search_Enemy.Discovery_Main)
        {
            Debug.Log("周回");
           
        }
    }

    //主人公を見つけた時の関数
    void Discovery()
    { 
        Time_Lapse = 0;
        //目標時点まで移動する（Goal_Positionの値をPlayerの座標にすればPlayerに向かう）
        transform.position = Vector3.MoveTowards(transform.position, Goal_Position, Speed_Enemy * Time.deltaTime);
    }

    //主人王を見失たった時の関数
    void Lost()
    {
        Time_Lapse += Time.deltaTime;

        if(Time_Lapse > Return)
        {
            //初期値に戻す
            transform.position = Initial_Value;
            Time_Lapse = 0;
        }
    }

    //敵の攻撃の関数
    void Attack()
    {
        Attack_Enemy = true;
        Attack_Enemy_Time = 0.0f;
    }
}