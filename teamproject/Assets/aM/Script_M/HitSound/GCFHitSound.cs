using UnityEngine;

public class GCFHitSound : MonoBehaviour
{
    [SerializeField] AudioSource AS;
    [SerializeField] AudioClip HitThunder_SE;
    float HitThunder_v = 2.0f;

    public bool sound_start = false;
    bool hit = false;
    int hit_time = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(sound_start)
        {
            AS.PlayOneShot(HitThunder_SE);
            AS.volume = HitThunder_v;
            hit = true;
            sound_start = false;
        }
        if(hit)
        {
            hit_time++;
        }
        if(hit_time > 60)
        {
            hit = false;
            hit_time = 0;
        }
    }
}
