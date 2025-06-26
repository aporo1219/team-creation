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

    public bool Attack_Enemy;//敵の攻撃の判定
    public static bool Damede_Hit;

    [SerializeField] GameObject MainCharacter;
    [SerializeField] GameObject Erea;//SearchEreaのスクリプトを呼び出すGameObject
    [SerializeField] SearchErea Search_Enemy;//主人公を探す変数
    [SerializeField] private Transform ModelRoot;// ← モデル（JR-1）を指定する用
    [SerializeField] private float RotationOffsetY = 0f;//向きの補正

    private float Speed_Enemy;//スピードの変数
    private float Time_Lapse;//敵が主人公を見失ったときの経過時間
    private int Return;//敵が初期値に戻る時間の変数
    private float Attack_Enemy_Time;//敵の攻撃のクールタイム
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
    Vector3 Player_Distance;//プレイヤーの距離間のベクトル


    [SerializeField] Vector3 Local_Space_Vec;//前方基準のローカル空間ベクトル


    void Start()
    {
        //初期化
        Search_Enemy = GetComponentInChildren<SearchErea>();
        Anim = GetComponent<Animator>();
        Local_Space_Vec = Vector3.forward;
        rd = GetComponent<Rigidbody>();
        //プレイヤーに近づく変数
        //MainCharacter = GameObject.FindWithTag("Player");
        Goal_Position = MainCharacter.transform.position;
        Speed_Enemy = 2.0f;
        Turn = false;//trueならば回転
        Local_Space_Vec = Vector3.up;
        Player_Distance = MainCharacter.transform.position - ModelRoot.position;
        //攻撃関連の変数
        Attack_Enemy = false;
        Attack_Enemy_Time = 0;
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

        //発見
        if(Search_Enemy.Find)
        {
            Discovery();
        }
        //プレイヤーを見失う
        if (!Search_Enemy.Find && Search_Enemy.FirstTime)
        {
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

        //this.transform.Rotate(MainCharacter.transform.position);
        Vector3 direction = MainCharacter.transform.position - this.transform.position;
        direction.y = 0; // 上下を無視して水平だけ向く

        if (direction.magnitude > 0.01f)
        {
            float Rotation_Speed = 5.0f;
            Quaternion rot = Quaternion.LookRotation(direction);
            rot *= Quaternion.Euler(0, -90, 0); // X-が前向きの補正
            ModelRoot.rotation = Quaternion.Slerp(ModelRoot.rotation, rot, Time.deltaTime * Rotation_Speed);
        }

        //アニメーション切り替え
        Anim.SetBool("Walk", true);
        // 前向き（モデルのforward方向）に進む
        Vector3 moveDir = ModelRoot.forward;
        moveDir.y = 0;
        transform.position += moveDir.normalized * Speed_Enemy * Time.deltaTime;
        //Goal位置の更新
        /*Goal_Position = MainCharacter.transform.position;
        //目標時点まで移動する（Goal_Positionの値をPlayerの座標にすればPlayerに向かう）
        transform.position = Vector3.MoveTowards(ModelRoot.position, Goal_Position, Speed_Enemy * Time.deltaTime);*/

        Mode_Serch = true;
    }

    //主人王を見失たった時の関数
    public void Lost()
    {
        Time_Lapse += Time.deltaTime;
        //アニメーション切り替え（止まる）
        Anim.SetBool("Walk", false);
        Anim.SetBool("Attack", false);
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
            string combo = PlayerController_y.instance.AttackState;
            rd.linearVelocity = new Vector3(0, 5, 0);
            Enemy.Be_Attack(combo);
            Debug.Log("hit,C");
        }

        if (collision.gameObject.name == "GF")
        {
            var Enemy = GetComponent<Enemy_Status>();
            string combo = PlayerController_y.instance.AttackState;
            rd.linearVelocity = new Vector3(0, 10, 0);
            Enemy.Be_Attack(combo);
            Debug.Log("hit,F");
        }
    }
}