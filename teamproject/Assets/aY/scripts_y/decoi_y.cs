using UnityEngine;

public class decoi_y : MonoBehaviour
{
    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "GC")
        {
            rb.linearVelocity = new Vector3(0, 5, 0);
            Debug.Log("hit,C");
        }

        if (collision.gameObject.name == "GF")
        {
            rb.linearVelocity = new Vector3(0, 10, 0);
            Debug.Log("hit,F");
        }
    }
}
