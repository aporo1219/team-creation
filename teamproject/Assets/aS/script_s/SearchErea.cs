using UnityEngine;

public class SearchErea : MonoBehaviour
{

    [SerializeField] GameObject SmallEnemy;

    public  bool Discovery_Main;
    public bool First_Time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
     void Start()
    {
        Discovery_Main = false;
        First_Time = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //敵のサーチ領域を雑魚敵に追従するようにする
        transform.position = SmallEnemy.transform.position;
    }

    void OnTriggerStay(Collider other)
    {
        //主人公がサーチ領域に入ったら敵が主人公に向かう信号を送る
        if(other.gameObject.name == "Main")
        {
            First_Time = true;
          // Debug.Log("ヒット");
           Discovery_Main = true;
        }
       
    }


    void OnTriggerExit(Collider other)
    {
        //主人公がサーチ領域に入ったら敵が主人公に向かう信号を送る
        if (other.gameObject.name == "Main")
        {

            // Debug.Log("ヒット");
            Discovery_Main = false;
        }

    }
}
