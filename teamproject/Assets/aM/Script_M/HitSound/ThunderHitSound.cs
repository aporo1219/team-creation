using UnityEngine;

public class ThunderHitSound : MonoBehaviour
{
    [SerializeField] AudioSource AS;
    [SerializeField] AudioClip HitThunder_SE;
    float HitThunder_v = 2.0f;

    bool sound_start = false;
    bool hit = false;
    int hit_time = 0;

    private void Update()
    {
        if(sound_start)
        {
            Debug.Log("‰¹ºÄ¶™");
            AS.PlayOneShot(HitThunder_SE);
            AS.volume = HitThunder_v;
            hit = true;
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

    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Enemy" || other.tag == "FlyEnemy" || other.tag == "WheelEnemy") && !hit)
        {
            sound_start = true;
        }
    }
}
