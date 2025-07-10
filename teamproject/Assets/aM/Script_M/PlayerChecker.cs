using UnityEngine;

public class PlayerChecker : MonoBehaviour
{
    [SerializeField] DualEnemySpawnManager spawnmanager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player" && !spawnmanager.do_spawn)
        {
            if (this.name == "Enemy_Spawn")
                spawnmanager.in_player = true;
            else if(this.name == "Dual_Enemy_Spawn")
                spawnmanager.dual_in_player = true;
        }
    }
}
