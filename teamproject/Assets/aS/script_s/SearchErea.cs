using Unity.VisualScripting;
using UnityEngine;

public class SearchErea : MonoBehaviour
{
    //敵の位置取得の変数
    [SerializeField] GameObject EnemyPos;
    //[SerializeField] Transform SE;

    public bool Find;
    public bool FirstTime;
  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //追跡対象の設定
        //SE = EnemyPos;

        FirstTime = false;
        Find = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //敵のサーチ領域を雑魚敵に追従するようにする
        transform.position = EnemyPos.transform.position;
    }

   
    void OnTriggerStay(Collider other)
    {
        //主人公がサーチ領域に入ったら敵が主人公に向かう信号を送る
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("発見");
            Find = true;
            FirstTime = true;
            //Move.Discovery();
        }
       
    }


    void OnTriggerExit(Collider other)
    {
        //主人公がサーチ領域に入ったら敵が主人公に向かう信号を送る
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("でた");
            Find = false;
            //Move.Lost();
        }

    }
}
