using UnityEngine;

public class GCFHit : MonoBehaviour
{
    public GCFHitSound HS;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy" || other.tag == "FlyEnemy" || other.tag == "WheelEnemy")
        {
            HS.sound_start = true;
        }
    }
}
