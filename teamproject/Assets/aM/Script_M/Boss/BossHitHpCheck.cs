using UnityEngine;

public class BossHitHpCheck : MonoBehaviour
{
    public BossStatus status;
    bool hit = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "GC" && !hit)
        {
            status.HP -= 3;
            hit = true;
        }
        if (collision.gameObject.name == "GF" && !hit)
        {
            status.HP -= 4;
            hit = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "GC" || collision.gameObject.name == "GF")
        {
            hit = false;
        }
    }
}
