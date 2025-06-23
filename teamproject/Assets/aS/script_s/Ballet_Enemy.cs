using Unity.Cinemachine;
using UnityEngine;

public class Ballet_Enemy : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private Transform  Pos;
    [SerializeField] GameObject Bullet_OBJ;

    private float Bullet_Speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Bullet_OBJ.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //弾の発射
    public void Shot()
    {
        //弾のゲームオブジェクトに代入
        if(!Bullet_OBJ.activeSelf)
        {
            //オブジェクトの出現
            Bullet_OBJ.SetActive(true);
            var Bullet = Instantiate(Bullet_OBJ, Pos.position, Pos.rotation);
            //弾のRigidbodyを取得
            Rigidbody rd = Bullet.GetComponent<Rigidbody>();
            Bullet.SetActive(true);


            if(rd != null)
            {
                 rd.angularVelocity = Pos.forward * Bullet_Speed;
            }
        }

        else
        {
            Debug.Log("設定なし");
        }
    }
}
