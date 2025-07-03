using UnityEngine;

public class AttackErea : MonoBehaviour
{
    private Transform EnemyPos;
    public bool Find;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnemyPos = transform.parent;
        Find = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Find = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Find = false;
        }
    }
}
