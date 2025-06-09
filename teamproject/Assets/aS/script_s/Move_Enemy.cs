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
    public bool Attack_Enemy;//敵の攻撃の判定
    public bool Damede_Hit;
    public int Combo_Number;

    [SerializeField] GameObject MainCharacter;
    [SerializeField] GameObject Erea;//SearchEreaのスクリプトを呼び出すGameObject
    [SerializeField] SearchErea Search_Enemy;//主人公を探す変数
   
    private float Speed_Enemy;//スピードの変数
    private float  Time_Lapse;//敵が主人公を見失ったときの経過時間
    private int Return;//敵が初期値に戻る時間の変数
    private float Attack_Enemy_Time;//敵の攻撃のクールタイム
    private int Cool_Time;//攻撃のクール時間
    private bool Mode_Serch;//探索モードかの判定
    private bool  Right_Or_Left;//周回時の右か左かの判定
    private float Around_Position;
    private bool Turn;//回転の変数
    private bool Be_Attacked;//攻撃を受けたかの判定
    private InputAction Player_Attack;


    Vector3 Goal_Position;//目標時点の座標変数（雑魚敵）
    Vector3 Initial_Value;//初期地点の座標変数
    Vector3 Search_Position_Right;//周回する用のベクトル右
    Vector3 Search_Position_Left;//周回する用のベクトル左
    Vector3 Player_Distance;//プレイヤーの距離間のベクトル
    [SerializeField] Vector3 Local_Space_Vec;//前方基準のローカル空間ベクトル
   

    void Start()
    {
        //初期化
        //プレイヤーに近づく変数
        Goal_Position = MainCharacter.transform.position;
        Speed_Enemy = 2.0f;
        MainCharacter = GameObject.FindWithTag("Player");
        Erea = GameObject.Find("SearchErea");
        Search_Enemy = Erea.GetComponent<SearchErea>();
        Turn = false;//trueならば回転
        Local_Space_Vec = Vector3.up;
        Player_Distance = MainCharacter.transform.position - this.transform.position;
        //攻撃関連の変数
        Attack_Enemy = false;
        Attack_Enemy_Time = 0;
        Cool_Time = 5;
        Damede_Hit = false;
        Be_Attacked = PlayerControllerTest_s.instance.T_Attack;
        Player_Attack = InputSystem.actions.FindAction("Attack");

        //探索関連の変数
        Mode_Serch = false;
        Search_Position_Right = new Vector3(this.transform.position.x + 10, (float)0.75, 0);
        Search_Position_Left = new Vector3(this.transform.position.x - 10, (float)0.75, 0);
        Right_Or_Left = false;//falseならば右,trueならば左
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
            
            if(!Right_Or_Left)
            {
               transform.position = Vector3.MoveTowards(transform.position, Search_Position_Right, Speed_Enemy * Time.deltaTime); 
            //目標地点についたら、その場で回転し逆方向に行く
              if(transform.position.x >= Around_Position)
              {
                    Right_Or_Left = true;
                    transform.Rotate(0, 180, 0);
              }
            }

            //逆方向に行く
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

    //主人公を見つけた時の関数
    void Discovery()
    {
        

        //Debug.Log("見つけた");
        Time_Lapse = 0;
        Turn = true;
        if(Turn)
        {
            //主人公の方向に回転
            var Rotate_Discovery = Quaternion.LookRotation(Player_Distance,Vector3.up); //プレイヤー発見の回転ベクトル
            var Rotate_Correction = Quaternion.FromToRotation(Local_Space_Vec, Vector3.up);


            //回転補正
            this.transform.rotation = Rotate_Discovery * Rotate_Correction;

            //Debug.Log("旋回");  
        }
        //目標時点まで移動する（Goal_Positionの値をPlayerの座標にすればPlayerに向かう）
        transform.position = Vector3.MoveTowards(transform.position, Goal_Position, Speed_Enemy * Time.deltaTime);

        Mode_Serch = true;
    }

    //主人王を見失たった時の関数
    void Lost()
    {
        Time_Lapse += Time.deltaTime;

        if(Time_Lapse > Return && Mode_Serch == true)
        {
            //初期値に戻す
            transform.position = Initial_Value;
            Time_Lapse = 0;
            Mode_Serch = false;
        }
    }

    //敵の攻撃の関数
    void Attack()
    {
        Attack_Enemy = true;
        Attack_Enemy_Time = 0.0f;
    }

    //攻撃を受けた
    public void OnCollisionEnter(Collision other)
    {

        if(other.gameObject.tag == "Player"&& Be_Attacked)
        {
            Debug.Log("ヒット");
            Damede_Hit = true;
        }
    }
}               