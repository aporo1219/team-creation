using UnityEngine;

public class BossAttackHItCheck : MonoBehaviour
{
    public BossStatus bs;
    PlayerStatus ps;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ps = other.GetComponent<PlayerStatus>();
            if (ps != null)
            {
                ps.PlayerDamage(bs.Attack);
                this.gameObject.SetActive(false);
            }
        }
    }
}
