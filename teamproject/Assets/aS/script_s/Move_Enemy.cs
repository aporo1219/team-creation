using System.Security.Cryptography;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;



public class Move_Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static bool Damede_Hit;

    [SerializeField] float x, y, z, w;

    [SerializeField] private GameObject MainCharacter;
    [SerializeField] GameObject SearchErea;
    [SerializeField] private SearchErea Search_Enemy;//主人公を探す変数
    [SerializeField] private Transform ModelRoot;// ← モデル（JR-1）を指定する用
    [SerializeField] private float RotationOffsetY = 0f;//向きの補正
    [SerializeField] private GameObject[] ModelPrefabs;
    [SerializeField] private AttackErea AttackErea;
    

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
    public bool OnGround = false;
    private Rigidbody rd;
    [SerializeField] LayerMask GroundLayer;//レイヤーの取得
    [SerializeField] float Ground_Distance = 0.2f;
    private bool Not_Move = false;//動きを止める(trueならば止まる)
    

    Vector3 Goal_Position;//目標時点の座標変数（雑魚敵）
    Vector3 Initial_Value;//初期地点の座標変数
    Vector3 Search_Position_Right;//周回する用のベクトル右
    Vector3 Search_Position_Left;//周回する用のベクトル左
  


    [SerializeField] Vector3 Local_Space_Vec;//前方基準のローカル空間ベクトル

    //SE
    [SerializeField] private AudioSource AS;
    [SerializeField] private AudioClip BeHit_SE;
    [SerializeField] private AudioClip HitThunder_SE;
    [SerializeField] private AudioClip HitFireBall_SE;
    //[SerializeField] private AudioClip Find;
    private float Be_Hit_v = 2.0f;
    private float HitThunder_v = 2.0f;
    private float HitFireBall_v = 2.0f;
    void Start()
    {
        //初期化
        Search_Enemy = SearchErea.GetComponent<SearchErea>();
        Anim = GetComponent<Animator>();
        Local_Space_Vec = Vector3.forward;
        rd = GetComponent<Rigidbody>();
        GroundLayer = LayerMask.GetMask("Ground");
        //プレイヤーに近づく変数
        MainCharacter = GameObject.FindGameObjectWithTag("Player");
        if (MainCharacter != null)
        {
            Goal_Position = MainCharacter.transform.position;
        }
        //車輪の敵だったらスピードが6、それ以外だったら
        if(gameObject.tag == "WheellEnemy")
        {
            Speed_Enemy = 6.0f;
        }
        else
        {
              Speed_Enemy = 5.0f;
        }
           
       
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
        //地面についているかのチェック
        if (gameObject.tag == "Enemy" || gameObject.tag == "WheellEnemy")
        {
            if (IsGrond())
            {
                if (!OnGround)
                {
                    rd.linearVelocity = Vector3.zero;
                    OnGround = true;
                }
            }
            else
            {
                OnGround = false;
                rd.AddForce(0, -100, 0);
            }
        }

        //上方向の制御
        if (Initial_Value.y < this.transform.position.y  && gameObject.tag == "FlyEnemy")
        {
            Debug.Log("上方向に移動しているため中止");
            rd.linearVelocity = Vector3.zero;

            if (this.transform.position.y > 1)
            {
                Debug.Log("下降");
                rd.AddForce(0,-50,0);
            } 
        }


        //発見
        if (OnGround)
        {
            if (Search_Enemy.Find && !Not_Move)
            {
                Discovery();
                //AS.PlayOneShot(Find);
                float time = 0;
                time += (float)Time.deltaTime;
            }

            //プレイヤーを見失う
            if (!Search_Enemy.Find)
            {
                Debug.Log("見失った１");
                Lost();
            }
        }
    }


    //主人公を見つけた時の関数
    public void Discovery()
    {
        Time_Lapse = 0;
        //MainCharacter.transform.position = 

        float time = 0;
        time += (float)Time.deltaTime;
        Debug.Log("見つけた");
        Time_Lapse = 0;
        if(gameObject.tag == "Enemy")
        {
           Anim.SetBool("Walk", true);
           time = 0;

        }
        else if (gameObject.tag == "WheellEnemy")
        {
            Anim.SetBool("Walk_1", true);
        }
        else if (gameObject.tag == "FlyEnemy")
        {
           
        }

        Vector3 Distance = MainCharacter.transform.position - ModelRoot.transform.position;
        
            Distance = new Vector3(Distance.x, 0, Distance.z);

            Quaternion Rotation = Quaternion.LookRotation(new Vector3(Distance.z,0,Distance.x * -1));

        //Vector3 rot = Rotation.eulerAngles;

        //transform.rotation = Rotation;

        x = Rotation.x;
        y = Rotation.y;
        z = Rotation.z;
        w = Rotation.w;
        if (Rotation.y <= 1)
        {
            Rotation = new Quaternion(Rotation.x, Rotation.y, Rotation.z, Rotation.w);
        }
        else
        {
            Rotation = new Quaternion(Rotation.x, Rotation.y, Rotation.z, Rotation.w);
        }

        //Rotation = new Quaternion(Rotation.x, Rotation.y + 0.5f, Rotation.z, Rotation.w);

        transform.rotation = Rotation;

            Debug.Log("旋回");
        

        Distance.Normalize();
        //目標時点まで移動する（Goal_Positionの値をPlayerの座標にすればPlayerに向かう）
        this.rd.linearVelocity = Distance * Speed_Enemy;


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
            if (!AttackErea.Find)
            {
                Anim.SetBool("Attack", false);
            }
        }
        else if (gameObject.tag == "WheellEnemy")
        {
            Anim.SetBool("Walk_1", false);
            if (!AttackErea.Find)
            {
                Anim.SetBool("Attack_1", false);
            }
        }
        else if (gameObject.tag == "FlyEnemy")
        {
            if (!AttackErea.Find)
            {
                Anim.SetBool("Attack_2", false);
            }
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
        int combo = 0;
        if (collision.gameObject.name == "GC")
        {
            var Enemy = GetComponent<Enemy_Status>();
            combo = 1;
            //敵を近づけさせる
            if(gameObject.tag == "Enemy" || gameObject.tag == "WheellEnemy")
            {
                var D = MainCharacter.transform.position - ModelRoot.transform.position;
                D.Normalize();
                rd.linearVelocity = D;
            }
            Enemy.Be_Attack(combo);
            Debug.Log("hit,C");
            //攻撃を受けたSEを流す
            AS.PlayOneShot(BeHit_SE);
            AS.volume = Be_Hit_v;
        }

        if (collision.gameObject.name == "GF")
        {
            var Enemy = GetComponent<Enemy_Status>();
            combo = 2;
            if(gameObject.tag == "Enemy" || gameObject.tag == "WheellEnemy")
            {
                var D = MainCharacter.transform.position - ModelRoot.transform.position;
                D.Normalize();
                rd.linearVelocity = D;
            }
            Enemy.Be_Attack(combo);
            Debug.Log("hit,F");
            //攻撃を受けたSEを流す
            AS.PlayOneShot(BeHit_SE);
            AS.volume = Be_Hit_v;
        }

        string Skill = "";
        //スキルを受けた時
        //ファイヤーボール
        if(collision.gameObject.name == "Explosion")
        {
            var Enemy = GetComponent<Enemy_Status>();
            Skill = "FireBall";
            Enemy.Be_Skill(Skill);
            Debug.Log("hit,FB");
            //HitSE
            AS.PlayOneShot(HitFireBall_SE);
            AS.volume = HitFireBall_v;
        }
        //サンダー
        if (collision.gameObject.name == "Lightning")
        {
            var Enemy = GetComponent<Enemy_Status>();
            Skill = "Thunder";
            Enemy.Be_Skill(Skill);
            Debug.Log("hit,Th");
            Not_Move = true;
            Invoke(nameof(Mobile), 2.0f);
            //SE
            AS.PlayOneShot(HitThunder_SE);
            AS.volume = HitThunder_v;
        }
    }

    //地面判定
    bool IsGrond()
    {
        return Physics.Raycast(transform.position, Vector3.down, Ground_Distance, GroundLayer);
    }
    
    //移動可能にする関数
   void  Mobile()
   {
        Not_Move = false;
   }
}

