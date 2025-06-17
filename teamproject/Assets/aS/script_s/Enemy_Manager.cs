using System.ComponentModel;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
   
    [SerializeField] GameObject Enemy_Object;//敵のタグを見つける変数
    [SerializeField] Move_Enemy Enemy;//敵のスクリプト取得変数
    [SerializeField] GameObject Player;//プレイヤーのタグを見つける変数
    [SerializeField] PlayerController_y Player_Script;//プレイヤーのスクリプト取得変数

    private bool Enemy_Die;
    private int Enemy_Number;
    private string Player_Combo;//プレイヤーのコンボ状況を確認するスクリプト
    

    public int Enemy_HP;
    public int Enemy_Power;
    public Enemy_Manager Instance;

    //インスタンスの代入
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Enemy_HP = 100;//仮数値
        Enemy_Die = false;//trueで消える
        Enemy_Number = 10;//仮敵の数
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
    public void Entry_Enemy(Enemy_Status Enemy)
    {

    }

    //IDの登録解除
    public void Delite_ListEnemy(Enemy_Status Enemy)
    {

    }
}
