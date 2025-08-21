using UnityEngine;

public class BossHitCheck : MonoBehaviour
{
    public bool wall_hit = false;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision != null && !collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("�ǃq�b�g");
            wall_hit = true;
        }
    }
}
