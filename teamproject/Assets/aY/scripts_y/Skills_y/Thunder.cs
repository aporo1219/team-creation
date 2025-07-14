using UnityEngine;

public class Thunder : MonoBehaviour
{
    int LifeTimeLimit = 40;
    int LifeTime = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
     
        LifeTime++;

        if (LifeTime == LifeTimeLimit)
            Destroy(gameObject);
    }
}
