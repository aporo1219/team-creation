using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet_Enemy : MonoBehaviour
{ 
    [SerializeField] GameObject Bullet_OBJ;
    [SerializeField] Transform Enemy;
   
 
    private float Bullet_Speed;

    Vector3 SpawnBullet;
    Vector3 Enemy_Pos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Bullet_Speed = 10.0f;
        Enemy_Pos = Enemy.position;
        SpawnBullet = transform.position + new Vector3(Enemy_Pos.x,1.15f,Enemy_Pos.z);
       
    }

  

    
    //弾発射
    public void Shot()
    {
        Enemy_Pos = Enemy.position;
        SpawnBullet = transform.position + new Vector3(Enemy_Pos.x, 1.15f, Enemy_Pos.z);
        GameObject bullet = Instantiate(Bullet_OBJ,SpawnBullet, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        { 
            rb.AddForce( Vector3.left * Bullet_Speed, ForceMode.VelocityChange); // 向きを適宜変更
        }

        Destroy(Bullet_OBJ,3f);

    }

    //�e��������
    void Delite()
    {
        Destroy(gameObject);
    }

    //�v���C���[�Ƃ��������������
    void OnTriggerEnter(Collider other)
    {
       if(gameObject.tag == ("Player"))
       {
            Delite();
       }
    }
        
}
