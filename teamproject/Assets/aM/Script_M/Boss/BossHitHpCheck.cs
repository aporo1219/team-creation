using UnityEngine;

public class BossHitHPCheck : MonoBehaviour
{
    bool hit = false;

    [SerializeField] GameObject Boss;
    [SerializeField] BossStatus status;
    [SerializeField] GameObject HitEffect;

    private void Update()
    {
        this.gameObject.transform.position = Boss.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerAttack")
        {
            status.HP -= 3;
            Instantiate(HitEffect, new Vector3(this.transform.position.x, this.transform.position.y + 10, this.transform.position.z), Quaternion.identity);
        }

        if(other.gameObject.name == "Thunder")
        {
            status.HP -= 8;
            Instantiate(HitEffect, new Vector3(this.transform.position.x, this.transform.position.y + 10, this.transform.position.z), Quaternion.identity);
        }

        if(other.gameObject.name == "FireBall")
        {
            status.HP -= 8;
            Instantiate(HitEffect, new Vector3(this.transform.position.x, this.transform.position.y + 10, this.transform.position.z), Quaternion.identity);
        }
    }
}
