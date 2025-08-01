using System.Runtime.CompilerServices;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet_Enemy : MonoBehaviour
{
    [SerializeField]  GameObject Bullet;
    [SerializeField] GameObject Enemy;

    private GameObject _Bullet;


    private int Bullet_Speed;
    public int Power;
    private Vector3 Spawn;
    private Vector3 Flont;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Bullet.SetActive(false);
        Bullet_Speed = 500;
        Power = 5;
    }




    //弾発射
    public void Shot()
    {
        if(gameObject.tag == "Enemy" || gameObject.tag == "WheellEnemy")
        {
            //スポーン位置の更新
            Spawn.x = transform.position.x;
            Spawn.y = transform.position.y + 2;
            Spawn.z = transform.position.z;
        }
        else
        {
            //スポーン位置の更新
            Spawn.x = transform.position.x;
            Spawn.y = transform.position.y + 4;
            Spawn.z = transform.position.z;
        }
        

        //弾の移動補正
        Vector3 Forward = Enemy.transform.right.normalized * -1;
        Vector3 Oblique = (Enemy.transform.forward + Vector3.down).normalized ;
        //出現
        Bullet.SetActive(true);
        //敵のRigidbodyを取得
        Rigidbody Enemy_RB = Enemy.GetComponent<Rigidbody>();
        Vector3 Move_Vector = Enemy_RB.linearVelocity;
       //弾の複製
         _Bullet = Instantiate(Bullet,Spawn, Quaternion.identity);
        Rigidbody _BulletRB = _Bullet.GetComponent<Rigidbody>();
        //弾の移動
        if(gameObject.tag == "Enemy" || gameObject.tag == "WheellEnemy")
        {
           _BulletRB.AddForce(Forward * Bullet_Speed);
        }
        else
        {
            _BulletRB.AddForce(Oblique * Bullet_Speed);
        }
            //弾の削除
            Destroy(_Bullet, 3.0f);
    }

}
