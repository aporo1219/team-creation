using UnityEngine;

public class PlayerChecker_DualSpawner : MonoBehaviour
{
    [SerializeField] DualTaskSystem dt_system;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            dt_system.finish_task = true;
            Destroy(this);
        }
    }
}
