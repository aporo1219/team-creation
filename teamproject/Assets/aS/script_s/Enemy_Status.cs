using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Status : MonoBehaviour
{
    [SerializeField] GameObject Player;//プレイヤーのタグを見つける変数
    [SerializeField] PlayerController_y Player_Script;//プレイヤーのスクリプト取得変数

    private string Player_Combo;//プレイヤーのコンボ状況を確認するスクリプト
    private int Count;
    private Enemy_Manager Enemy_manager;
   

    public static Enemy_Status Instance;
    public int Enemy_HP;
    public int Enemy_Power;
    //敵のID
    public int Enemy_ID { get; private set; }

     void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
       Enemy_ID = Enemy_Manager.Entry_Enemy_ID(this);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Count = 5;
        Enemy_HP = 120;//仮数値
        Enemy_Power = 5;//仮数値
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Enemy_HP < 0)
        {
            Die();
        }

        if (Move_Enemy.Damede_Hit)
        {
            Debug.Log("攻撃を受けた");
            Be_Attack(PlayerController_y.instance.AttackState);
        }
    }

    //雑魚敵の攻撃を受けた処理の関数
    void Be_Attack(string Combo)
    {
        //コンボ一段階目
        if (Combo == "GroundFirst" || Combo == "AirFirst")
        {
            Debug.Log($"敵ID {Enemy_ID} は {Enemy_HP} HP");
            Enemy_HP -= 1;
        }
        //コンボ二段目
        if (Combo == "GroundSecond" || Combo == "AirSecond")
        {
            Debug.Log($"敵ID {Enemy_ID} は {Enemy_HP} HP");
            Enemy_HP -= 2;
        }
        //コンボ三段目
        if (Combo == "GroundThird" || Combo == "AirThird")
        {
            Debug.Log($"敵ID {Enemy_ID} は {Enemy_HP} HP");
            Enemy_HP -= 3;
        }
        //コンボ四段目
        if (Combo == "GroundFinish" || Combo == "AirFinish")
        {
            Debug.Log($"敵ID {Enemy_ID} は {Enemy_HP} HP");
            Enemy_HP -= 4;
        }


    }

    //雑魚敵が死んだ後の処理
    void OnDestroy()
    {
        Enemy_Manager.Delite_ListEnemy(Enemy_ID);
        Debug.Log("死亡");
        //Enemy_Manager.Destroy_Enemy_ID(1);
    }

    //死亡時の関数
    void Die()
    {
        Destroy(gameObject);
    }
}
