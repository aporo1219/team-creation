using UnityEngine;

public class EnemyStore : MonoBehaviour
{
    GameObject spawn_obj;
    EnemySpawnManager spawnManager;
    DualEnemySpawnManager dualspawnManager;

    [SerializeField] GameObject viability_obj;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawn_obj = transform.parent.gameObject;
        spawnManager = spawn_obj.GetComponent<EnemySpawnManager>();
        if (spawnManager == null)
            dualspawnManager = spawn_obj.GetComponent<DualEnemySpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(viability_obj.activeSelf)
        {
            if (spawnManager != null)
                spawnManager.death_num++;
            else if (dualspawnManager != null)
                dualspawnManager.death_num++;
                Destroy(gameObject);
        }
    }
}
