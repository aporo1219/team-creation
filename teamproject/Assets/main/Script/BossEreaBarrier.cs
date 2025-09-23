using UnityEngine;

public class BossEreaBarrier : MonoBehaviour
{
    public GameObject bar1;
    public GameObject bar2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            bar1.SetActive(true);
            bar2.SetActive(true);
        }
    }
}
