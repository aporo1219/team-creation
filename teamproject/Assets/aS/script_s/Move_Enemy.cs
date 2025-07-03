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
    public static bool Damede_Hit;

    [SerializeField] float x, y, z, w;

    [SerializeField] GameObject MainCharacter;
    [SerializeField] SearchErea Search_Enemy;//主人公を探す変数
    [SerializeField] private Transform ModelRoot;// ← モデル（JR-1）を指定する用
    [SerializeField] private float RotationOffsetY = 0f;//向きの補正
    [SerializeField] private GameObject[] ModelPrefabs;
    

    private float Speed_Enemy;//スピードの変数
    private float Time_Lapse;//敵が主人公を見失ったときの経過時間
    private int Return;//敵が初期値に戻る時間の変数
    private int Cool_Time;//攻撃のクール時間
    private bool Mode_Serch;//探索モードかの判定
    private bool Right_Or_Left;//周回時の右か左かの判定
    private float Around_Position;
    private bool Turn;//回転の変数
    private bool Be_Attacked;//攻撃を受けたかの判定
    private Animator Anim;//アニメーションコンポーネントの取得
    private float Distance;//距離の計算
    private Rigidbody rd;
    

    Vector3 Goal_Position;//目標時点の座標変数（雑魚敵）
    Vector3 Initial_Value;//初期地点の座標変数
    Vector3 Search_Position_Right;//周回する用のベクトル右
    Vector3 Search_Position_Left;//周回する用のベクトル左
  


    [SerializeField] Vector3 Local_Space_Vec;//前方基準のローカル空間ベクトル


    void Start()
    {
        //初期化
        Search_Enemy = GetComponentInChildren<SearchErea>();
        Anim = GetComponent<Animator>();
        Local_Space_Vec = Vector3.forward;
        rd = GetComponent<Rigidbody>();
        //プレイヤーに近づく変数
        MainCharacter = GameObject.FindGameObjectWithTag("Player");
        if(MainCharacter != null)
        {
            Goal_Position = MainCharacter.transform.position;
        }
      
        Speed_Enemy = 2.0f;
        Turn = false;//trueならば回転
        Local_Space_Vec = Vector3.up;
        //攻撃関連の変数
        Cool_Time = 5;
        Damede_Hit = false;
        //探索関連の変数
        Mode_Serch = false;
        Search_Position_Right.x = this.transform.position.x + 10;
        Search_Position_Right.y = this.transform.position.y;
        Search_Position_Right.z = this.transform.position.z;
        Search_Position_Left.x = this.transform.position.x - 10;
        Search_Position_Left.y = this.transform.position.y;
        Search_Position_Left.z = this.transform.position.z;
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
        if (Time.timeScale == 0)
        {
            return;
        }

        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Around();

        //上方向の制御
        /*bool isMovingUp = Vector3.Dot(rd.linearVelocity.normalized, Vector3.up) > 0.7f;

        if (isMovingUp && gameObject.tag == "FlyEnemy")
        {
                Debug.Log("上方向に移動しているため中止");
                this.rd.linearVelocity = Initial_Value;
                return;
        }*/
        

        //発見
        if (Search_Enemy.Find)
        {
            Discovery();
        }
        //プレイヤーを見失う
        if (!Search_Enemy.Find)
        {
            Debug.Log("見失った１");
            Lost();
        }
    }

    //周回する関数
    /*void Around()
    {
        if (!Search_Enemy.Find)
        {
            if (!Right_Or_Left)
            {
                transform.position = Vector3.MoveTowards(transform.position, Search_Position_Right, Speed_Enemy * Time.deltaTime);
                //目標地点についたら、その場で回転し逆方向に行く
                if (transform.position.x >= Around_Position)
                {
                    Around_Position = -8.0f;
                    Right_Or_Left = true;
                    ModelRoot.Rotate(0, , 0);
                }
            }

            //逆方向に行く
            else if (Right_Or_Left)
            {
                Debug.Log("回転");
                transform.position = Vector3.MoveTowards(transform.position, Search_Position_Left, Speed_Enemy * Time.deltaTime);

                if (transform.position.x <= Around_Position)
                {
                    Around_Position = 9.9f;
                    Debug.Log("回転1");
                    ModelRoot.Rotate(0, 180, 0);
                    Right_Or_Left = false;
                }

            }
        }
    }*/

    //主人公を見つけた時の関数
    public void Discovery()
    {
        Time_Lapse = 0;
        //MainCharacter.transform.position = 

        Debug.Log("見つけた");
        Time_Lapse = 0;
        if(gameObject.tag == "Enemy")
        {
           Anim.SetBool("Walk", true);
        }
        else if (gameObject.tag == "WheellEnemy")
        {
            Anim.SetBool("Walk_1", true);
        }
       

        Vector3 Distance = MainCharacter.transform.position - ModelRoot.transform.position;
        
            Distance = new Vector3(Distance.x, 0, Distance.z);

            Quaternion Rotation = Quaternion.LookRotation(Distance);

        //Vector3 rot = Rotation.eulerAngles;

        //transform.rotation = Rotation;

        x = Rotation.x;
        y = Rotation.y;
        z = Rotation.z;
        w = Rotation.w;
        if (Rotation.y <= 1)
        {
            Rotation = new Quaternion(Rotation.x, Rotation.y + 0.5f, Rotation.z, Rotation.w);
        }
        else
        {
            Rotation = new Quaternion(Rotation.x, Rotation.y - 0.5f, Rotation.z, Rotation.w);
        }

        //Rotation = new Quaternion(Rotation.x, Rotation.y + 0.5f, Rotation.z, Rotation.w);

        transform.rotation = Rotation;

            Debug.Log("旋回");
        

        Distance.Normalize();
        //目標時点まで移動する（Goal_Positionの値をPlayerの座標にすればPlayerに向かう）
        this.rd.linearVelocity = Distance;


        Mode_Serch = true;
    }

    //主人王を見失たった時の関数
    public void Lost()
    {
        //移動を止める
        if (gameObject.tag == "FlyEnemy")
        {
            rd.linearVelocity = Vector3.zero;
        }
        Time_Lapse += Time.deltaTime;
        //アニメーション切り替え（止まる）
        if(gameObject.tag == "Enemy")
        {
           Anim.SetBool("Walk", false);
           Anim.SetBool("Attack", false);
        }
        else if (gameObject.tag == "WheellEnemy")
        {
            Anim.SetBool("Walk_1", false);
            Anim.SetBool("Attack_1", false);
        }
        else if (gameObject.tag == "FlyEnemy")
        { 
            Anim.SetBool("Attack_2", false);
        }

        if (Time_Lapse > Return && Mode_Serch == true)
        {
            Debug.Log("戻る");
            //初期値に戻す
            transform.position = Initial_Value;
            Time_Lapse = 0;
            Mode_Serch = false;
        }
    }

    //攻撃を受けた時
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "GC")
        {
            var Enemy = GetComponent<Enemy_Status>();
            int combo = PlayerController_y1.instance.AttackNum;
            //敵を浮かす
            if(gameObject.tag == "Enemy" || gameObject.tag == "WheellEnemy")
            {
              rd.linearVelocity = new Vector3(0, 5, 0);
            }
            Enemy.Be_Attack(combo);
            Debug.Log("hit,C");
        }

        if (collision.gameObject.name == "GF")
        {
            var Enemy = GetComponent<Enemy_Status>();
            int combo = PlayerController_y1.instance.AttackNum;
            if(gameObject.tag == "Enemy" || gameObject.tag == "WheellEnemy")
            {
                rd.linearVelocity = new Vector3(0, 10, 0);
            }
            Enemy.Be_Attack(combo);
            Debug.Log("hit,F");
        }
    }
}

