using UnityEngine;

public class BossHitHPCheck : MonoBehaviour
{
    bool hit = false;

    [SerializeField] BossStatus status;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerAttack")
        {
            status.HP -= 3;
        }
    }
}
