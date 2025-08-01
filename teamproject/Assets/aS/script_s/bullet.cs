using UnityEngine;

public class bullet : MonoBehaviour
{
    public GameObject Enemy;
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

    //ìGçUåÇÇó^Ç¶ÇÈ
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerStatus.Instance.PlayerDamage(Damege);
            Destroy(gameObject);
        }
    }
}
