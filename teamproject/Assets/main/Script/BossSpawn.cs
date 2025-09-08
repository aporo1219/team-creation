using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    [SerializeField]GameObject Boss;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            Boss.SetActive(true);
        }
    }
}
