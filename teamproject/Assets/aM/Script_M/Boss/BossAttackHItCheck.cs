using UnityEngine;

public class BossAttackHItCheck : MonoBehaviour
{
    PlayerStatus ps;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ps = other.GetComponent<PlayerStatus>();
            if (ps != null)
            {
                ps.PlayerDamage(20);
            }
        }
    }
}
