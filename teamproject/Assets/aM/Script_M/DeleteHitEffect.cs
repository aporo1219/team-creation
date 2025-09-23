using UnityEngine;

public class DeleteHitEffect : MonoBehaviour
{
    int show_time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        show_time++;

        if(show_time > 60)
        {
            Destroy(this.gameObject);
        }
    }
}
