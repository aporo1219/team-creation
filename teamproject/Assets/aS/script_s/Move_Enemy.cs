using System.Security.Cryptography;
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
 



    Rigidbody Rigidbody_Enemy;
    Vector3 Goal_Position;//目標時点の座標変数（雑魚敵）
    Vector3 Main;
    float Distance;

    void Start()
    {
        //初期化
        Goal_Position = new Vector3(4, (float)0.75, 4);
        //Rigidbody_Enemy = GetComponent<Rigidbody>();
        Speed_Enemy = 10.0f;
        Erea = GameObject.Find("SearchErea");
        Search_Enemy = Erea.GetComponent<SearchErea>();

    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if (Search_Enemy.Discovery_Main)
        {
           Discovery();

        }
        else if(!Search_Enemy.Discovery_Main)
        {
            Lost();
        }

    } 
    //プレイヤーの空中打ち上げ攻撃があったときに発動
    void OnCollisionEnter(Collision collision)
    {

    }

    //主人公を見つけた時の関数
    void Discovery()
    { 
        //目標時点まで移動する（Goal_Positionの値をPlayerの座標にすればPlayerに向かう）
        transform.position = Vector3.MoveTowards(transform.position, Goal_Position, Time.deltaTime);
        //Debug.Log("見つけた");
    }

    void Lost()
    {
        transform.position = new Vector3(0, (float)0.75, 0);
        Debug.Log("見失った");
    }
}