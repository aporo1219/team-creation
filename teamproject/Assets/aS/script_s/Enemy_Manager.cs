using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
   
    [SerializeField] GameObject Enemy_Object;//敵のタグを見つける変数
    [SerializeField] Move_Enemy Enemy;//敵のスクリプト取得変数
    [SerializeField] GameObject Player;//プレイヤーのタグを見つける変数
    [SerializeField] PlayerController_y Player_Script;//プレイヤーのスクリプト取得変数

    private bool Enemy_Die;
    private static int Enemy_Number;//ID割り当て
    private static Dictionary<int, Enemy_Status> Enemys = new();
   
    public static Enemy_Manager Instance;
    

    //インスタンスの代入
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        Enemy_Die = false;//trueで消える
        Enemy_Number = 0;//仮敵の数
        Enemy_Object = GameObject.FindWithTag("Enemy");
        Enemy = Enemy_Object.GetComponent<Move_Enemy>();
        Player = GameObject.FindWithTag("Player");
        Player_Script = Player.GetComponent<PlayerController_y>();
    }
    


    // Update is called once per frame
    void FixedUpdate()
    {
        
       
    }

   
    //IDの登録
    public static int Entry_Enemy_ID(Enemy_Status Enemy)
    {
        int Enemy_Tag = Enemy_Number++;
        Enemys[Enemy_Tag] = Enemy;
        return Enemy_Tag ;
    }

    //IDの登録解除(破壊されたとき）
    public static void Delite_ListEnemy(int Enemy_Tag)
    {
        Enemys.Remove(Enemy_Tag);
       
    }

    //ID指定での敵の破壊
    public static void Destroy_Enemy_ID(int Enemy_Tag)
    {
        if(Enemys.TryGetValue(Enemy_Tag, out Enemy_Status Enemy))
        {
            if(Enemy != null)
            {
                Object.Destroy(Enemy.gameObject);
            }
            else
            {
                Debug.Log($"敵ID{Enemy_Tag}のオブジェクトはnullです");
            }
        }
        else
        {
            Debug.Log($"敵ID{Enemy_Tag}は存在しない");
        }
    }
}
