using UnityEngine;

public class AttackErea : MonoBehaviour
{
    [SerializeField] GameObject  EnemyPos;
    public bool Find;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.transform.position = EnemyPos.transform.position;
        Find = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        this.transform.position = EnemyPos.transform.position;
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
