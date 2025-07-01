using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Status : MonoBehaviour
{
    private string Player_Combo;//プレイヤーのコンボ状況を確認するスクリプト
    private int Count;
    private Enemy_Manager Enemy_manager;
    private Animator Anim;//アニメーター取得の変数
    private int CoolTime;//攻撃のクールタイム
    private int time = 0;

    //public static Enemy_Status Instance;
    public int Enemy_HP;
    //敵のID
    public int Enemy_ID { get; private set; }

    private float nextShotTime = 0f; // 次に撃てる時刻
    [SerializeField] float shotInterval = 2f; // 発射間隔（秒）
    Vector3 Distance;//プレイヤーの距離の計算ベクトル


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Anim = GetComponent<Animator>();
        Count = 5;
        Enemy_HP = 1;//仮数値
        Enemy_ID = Enemy_Manager.Entry_Enemy_ID(this);//IDの登録
        CoolTime = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (Enemy_HP < 0)
        {
           Death_Before();
        }

    }

    //雑魚敵の攻撃を受けた処理の関数
    public void Be_Attack(int Combo)
    {
        //コンボ一段階目
        if (Combo ==  1)
        {
            Debug.Log($"敵ID {Enemy_ID} は {Enemy_HP} HP");
            Enemy_HP -= 1;
        }
        //コンボ二段目
        if (Combo ==  2)
        {
            Debug.Log($"敵ID {Enemy_ID} は {Enemy_HP} HP");
            Enemy_HP -= 2;
        }
        //コンボ三段目
        if (Combo == 3)
        {
            Debug.Log($"敵ID {Enemy_ID} は {Enemy_HP} HP");
            Enemy_HP -= 3;
        }
        //コンボ四段目
        if (Combo == 4)
        {
            Debug.Log($"敵ID {Enemy_ID} は {Enemy_HP} HP");
            Enemy_HP -= 4;
        }


    }

    //雑魚敵が死んだ後の処理
    void Death_Before()
    {
        //アニメーション切り替え
        if(gameObject.tag == "Enemy")
        {
           Anim.SetBool("Die", true);
        }
        else if (gameObject.tag == "WheellEnemy")
        {
            Anim.SetBool("Die_1", true);
        }
        else if (gameObject.tag == "FlyEnemy")
        {
            Anim.SetBool("Die_2", true);
        }
        Enemy_Manager.Delite_ListEnemy(Enemy_ID);
        Debug.Log("死亡");
        //1秒後にDestroy
        Invoke(nameof(Die), 1.0f);
        //Enemy_Manager.Destroy_Enemy_ID(1);
    }

    //死亡時の関数
    void Die()
    {
        Destroy(gameObject);
    }

    //攻撃
    private void OnCollisionStay(Collision collision)
    {
          
        if (collision.gameObject.tag == ("Player"))
        {
            if (Time.time >= nextShotTime)
            {
                nextShotTime = Time.time + shotInterval;

                // 攻撃アニメーション再生
                if(gameObject.tag == "Enemy")
                {
                   Anim.SetBool("Attack", true);
                }
                if (gameObject.tag == "WheellEnemy")
                {
                    Anim.SetBool("Attack_1", true);
                }

                if (gameObject.tag == "FlyEnemy")
                {
                    Anim.SetBool("Attack_2", true);
                }

                // 弾の発射
                Bullet_Enemy EB = GetComponent<Bullet_Enemy>();
                if (EB != null)
                {
                    Debug.Log("発射");
                    EB.Shot();
                }

                //Debug.Log("敵が弾を撃ちました");
            }


        }
    }
}