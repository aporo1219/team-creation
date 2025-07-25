using UnityEngine;

public class bullet : MonoBehaviour
{
    private GameObject Enemy;
    private Enemy_Status Enemy_Status;

    private int Damege;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Enemy = GameObject.FindGameObjectWithTag("Enemy");

        Enemy_Status = Enemy.GetComponent<Enemy_Status>();
        Damege = Enemy_Status.Enemy_Power;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
Å@Å@
    //ìGçUåÇÇó^Ç¶ÇÈ
    void OnCollisionEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerStatus.Instance.PlayerDamage(Damege);
        }
    }
}
