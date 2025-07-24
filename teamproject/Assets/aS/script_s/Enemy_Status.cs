using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Status : MonoBehaviour
{
    [SerializeField] GameObject Die_Enemy;

    private string Player_Combo;//プレイヤーのコンボ状況を確認するスクリプト
    private int Count;
    private Enemy_Manager Enemy_manager;
    private Animator Anim;//アニメーター取得の変数
    private int CoolTime;//攻撃のクールタイム
    private int time = 0;

    [SerializeField] AttackErea Attack_Erea;
    //public static Enemy_Status Instance;
    public int Enemy_HP;
    //敵のID
    public int Enemy_ID { get; private set; }

    private float nextShotTime = 0f; // 次に撃てる時刻
    [SerializeField] float shotInterval = 2f; // 発射間隔（秒）
    Vector3 Distance;//プレイヤーの距離の計算ベクトル

    //SE
    [SerializeField] private AudioSource AS;
    [SerializeField] private AudioClip Attack_SE;
    private float Attack_v = 0.3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Anim = GetComponent<Animator>();
        Count = 5;
        Enemy_HP = 10;//仮数値
        Enemy_ID = Enemy_Manager.Entry_Enemy_ID(this);//IDの登録
        CoolTime = 5;
        Die_Enemy.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Enemy_HP < 0)
        {
            Die_Enemy.SetActive(true);
            Death_Before();
        }

        if (Time.time >= nextShotTime && Attack_Erea.Find)
        {
            nextShotTime = Time.time + shotInterval;

            // 攻撃アニメーション再生
            if (gameObject.tag == "Enemy")
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
                //弾発射時のSEを流す
                AS.PlayOneShot(Attack_SE);
                AS.volume = Attack_v;
            }

            //Debug.Log("敵が弾を撃ちました");
        }

    }

    //雑魚敵の攻撃を受けた処理の関数
    public void Be_Attack(int Combo)
    {
        //コンボ
        if (Combo ==  1)
        {
            Debug.Log($"敵ID {Enemy_ID} は {Enemy_HP} HP");
            Enemy_HP -= 3;
        }
        //コンボ最終
        if (Combo == 2)
        {
            Debug.Log($"敵ID {Enemy_ID} は {Enemy_HP} HP");
            Enemy_HP -= 7;
        }


    }

　　//雑魚敵がスキルを受けた処理の関数
    public void Be_Skill(string skill)
    {
        //ファイヤーボール
        if(skill == "FireBall")
        {
            Enemy_HP -= 8;
        }
        //サンダー
        if (skill == "Thunder")
        {
            Enemy_HP -= 8;
        }
    }
    //雑魚敵が死んだ後の処理
    void Death_Before()
    {
        //アニメーション切り替え
        if(gameObject.tag == "Enemy")
        { 
            Anim.SetBool("Walk", false);
            Anim.SetBool("Attack",false);
            Anim.CrossFade("Die", 0.0f);
            Enemy_Manager.Delite_ListEnemy(Enemy_ID);
            Debug.Log("死亡");
            //アニメーション再生後にDestroy
            Invoke(nameof(Die),0.30f);
        }
        if (gameObject.tag == "WheellEnemy")
        {
            Anim.SetBool("Walk", false);
            Anim.SetBool("Attack_1", false);
            Anim.CrossFade("Die_1", 0.0f);
            Enemy_Manager.Delite_ListEnemy(Enemy_ID);
            Debug.Log("死亡");
            //アニメーション再生後にDestroy
            Invoke(nameof(Die),0.30f);
        }
        if (gameObject.tag == "FlyEnemy")
        {
            Anim.SetBool("Walk", false);
            Anim.SetBool("Attack_2", false);
            Anim.CrossFade("Die_2", 0.0f);
            Enemy_Manager.Delite_ListEnemy(Enemy_ID);
            Debug.Log("死亡");
            //アニメーション再生後にDestroy
            Invoke(nameof(Die),0.35f);
        }
    }

    //死亡時の関数
    void Die()
    {
        Destroy(gameObject);
    }

    //攻撃
    //void OnCollisionStay(Collision other)
    //{
    //    if(other.gameObject.tag == "Player")
    //    {
    //        if (Time.time >= nextShotTime)
    //        {
    //            nextShotTime = Time.time + shotInterval;

    //            // 攻撃アニメーション再生
    //            if (gameObject.tag == "Enemy")
    //            {
    //                Anim.SetBool("Attack", true);
    //            }
    //            if (gameObject.tag == "WheellEnemy")
    //            {
    //                Anim.SetBool("Attack_1", true);
    //            }

    //            if (gameObject.tag == "FlyEnemy")
    //            {
    //                Anim.SetBool("Attack_2", true);
    //            }

    //            // 弾の発射
    //            Bullet_Enemy EB = GetComponent<Bullet_Enemy>();
    //            if (EB != null)
    //            {
    //                Debug.Log("発射");
    //                EB.Shot();
    //                //弾発射時のSEを流す
    //                AS.PlayOneShot(Attack_SE);
    //                AS.volume = Attack_v;
    //            }

    //            //Debug.Log("敵が弾を撃ちました");
    //        }

    //    }
    //}
}