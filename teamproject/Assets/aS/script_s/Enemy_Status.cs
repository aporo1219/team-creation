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
    public string Enemy_Name;//IDの設定

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Count = 5;
        Enemy_HP = 100;//仮数値
        Enemy_Power = 5;//仮数値
        Enemy_Manager.Instance.Entry_Enemy(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Enemy_HP < 0)
        {
            After_Die();
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
            Enemy_HP -= 1;
        }
        //コンボ二段目
        if (Combo == "GroundSecond" || Combo == "AirSecond")
        {
            Enemy_HP -= 2;
        }
        //コンボ三段目
        if (Combo == "GroundThird" || Combo == "AirThird")
        {
            Enemy_HP -= 3;
        }
        //コンボ四段目
        if (Combo == "GroundFinish" || Combo == "AirFinish")
        {
            Enemy_HP -= 4;
        }


    }

    //雑魚敵が死んだ後の処理
    void After_Die()
    {
        Debug.Log("死亡");
        Enemy_Manager.Instance.Delite_ListEnemy(this);
        //Destroy();
    }
}
