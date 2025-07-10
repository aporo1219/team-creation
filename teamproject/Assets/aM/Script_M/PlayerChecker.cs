using UnityEngine;

public class PlayerChecker : MonoBehaviour
{
    [SerializeField] DualEnemySpawnManager spawnmanager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player" && !spawnmanager.do_spawn)
        {
            spawnmanager.in_player = true;
        }
    }
}
