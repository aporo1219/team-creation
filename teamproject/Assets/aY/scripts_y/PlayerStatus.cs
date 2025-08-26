using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus Instance;

    //プレイヤーのステータス変数----------
    public int Level = 1;           //レベル
    private int Exp = 0;            //獲得経験値
    public int MaxHP = 50;          //最大体力
    public int HP = 50;             //現在体力
    private int DefaultAttack = 5;  //基礎攻撃力
    private int DefaultDefense = 5; //基礎防御力
    public float AttackRate;        //攻撃力倍率
    public float DefenseRate;       //防御力倍率
    public int Attack;              //最終攻撃力(ダメージ計算にはこれを用いる)
    public int Defense;             //最終防御力(ダメージ計算にはこれを用いる)
    //------------------------------------

    private bool dead = false;

    PlayerController_y1 PlayerCont;

    [SerializeField] SceneChenger SC;
    //当たり判定の種類
    [HideInInspector] public enum ColliderMode
    {
        Neutral,    //通常
        Guard,      //ガード
        Invincible, //無敵
    }

    //プレイヤーの当たり判定
    public ColliderMode ColliderStste;
    //UIのバフリスト
   [SerializeField] private BuffList_Manager BuffList;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerCont = GetComponent<PlayerController_y1>();

        //当たり判定初期化
        ColliderStste = ColliderMode.Neutral;
        //バフリスト取得
        BuffList = FindAnyObjectByType<BuffList_Manager>();
        //攻撃力・防御力倍率初期化
        AttackRate = 1.0f;
        DefenseRate = 1.0f;
        //インスタンス設定
        Instance = this; 
    }

    

    // Update is called once per frame
    void Update()
    {

        if(BuffList == null)
        {
            BuffList = FindAnyObjectByType<BuffList_Manager>();
        }

        //現在HPが最大HPを超えたら
        if(HP > MaxHP)
        {
            HP = MaxHP;
        }
        if (HP == MaxHP)
            dead = false;
        //HPが0以下になったら
        else if (HP <= 0)
        {
            HP = 0;


            if (!dead)
                StartCoroutine(GameOver());

            dead = true;
        }

        Attack = (int)(DefaultAttack * AttackRate);
        Defense = (int)(DefaultDefense * DefenseRate);
    }

    /// <summary>
    /// プレイヤーにダメージを与える関数
    /// </summary>
    /// <param name="damage">ダメージ量</param>
    public void PlayerDamage(int damage)
    {
        if (ColliderStste == ColliderMode.Neutral)
        {
            //防御力を加味して計算し、ダメージが0以下なら固定で1ダメージ与える
            if ((damage - (int)(Defense / 4)) > 0)
                HP -= damage - (int)(Defense / 4);
            else
                HP -= 1;
        }
        else if (ColliderStste == ColliderMode.Guard)
        {
            //防御力とガードのダメージ軽減を加味して計算し、ダメージが0以下なら固定で1ダメージ与える
            if ((int)((damage - (int)(Defense / 4)) * 0.2f) > 0)
                HP -= (int)((damage - (int)(Defense / 4)) * 0.2f);
            else
                HP -= 1;
        }
        else if (ColliderStste == ColliderMode.Invincible)
        {
            
        }

    }

    /// <summary>
    /// プレイヤーの攻撃力の倍率を変更する関数
    /// </summary>
    /// <param name="ratevalue">変化倍率</param>
    /// <param name="second">効果時間(秒)</param>
    /// <returns></returns>
    public IEnumerator SetAttackRate(float ratevalue, float second)
    {
        if (ratevalue > 1.0f)
            BuffList.BuffInput(BuffList_Manager.BuffCategory.AttackUP, second);
        else
            BuffList.BuffInput(BuffList_Manager.BuffCategory.AttackDOWN, second);

        if (ratevalue >= 0)
        {
            AttackRate += ratevalue - 1.0f;

            for (float i = 0.0f; i < second; i += Time.deltaTime)
            {
                yield return null;
            }

            AttackRate -= ratevalue - 1.0f;
        }
        yield return null;
    }

    /// <summary>
    /// プレイヤーの防御力の倍率を変更する関数
    /// </summary>
    /// <param name="ratevalue">変化倍率</param>
    /// <param name="second">効果時間(秒)</param>
    /// <returns></returns>
    public IEnumerator SetDefenseRate(float ratevalue, float second)
    {
        if (ratevalue > 1.0f)
            BuffList.BuffInput(BuffList_Manager.BuffCategory.DefenseUP, second);
        else
            BuffList.BuffInput(BuffList_Manager.BuffCategory.DefenseDOWN, second);

        if (ratevalue >= 0)
        {
            DefenseRate += ratevalue - 1.0f;

            for (float i = 0.0f; i < second; i += Time.deltaTime)
            {
                yield return null;
            }

            DefenseRate -= ratevalue - 1.0f;
        }
        yield return null;
    }

    private IEnumerator GameOver()
    {
        PlayerCont.AnimationPlay("aaaa");

        PlayerCont.canAction = false;
        PlayerCont.canMove = false;
        PlayerCont.canRotate = false;

        

        for(float i=0;i<2.0f;i+=Time.deltaTime)
        {
            PlayerCont.rb.linearVelocity = Vector3.zero;
            yield return null;
        }

        //ゲームオーバーシーンに切り替え
        Debug.Log("ゲームオーバー");
        SceneManager.LoadScene("GameOver");

        yield return null;
    }
}