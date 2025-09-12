using UnityEngine;

public class EnemyStore : MonoBehaviour
{
    int destroy_time = -1;

    GameObject spawn_obj;
    EnemySpawnManager spawnManager;
    DualEnemySpawnManager dualspawnManager;

    FreeEnemySpawnManager killmanager;

    [SerializeField] GameObject viability_obj;
    [SerializeField] ViabilityChecker viability_scr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //spawn_obj = transform.parent.gameObject;
        //spawnManager = spawn_obj.GetComponent<EnemySpawnManager>();
        //if (spawnManager == null)
        //    dualspawnManager = spawn_obj.GetComponent<DualEnemySpawnManager>();

        spawn_obj = transform.parent.gameObject;
        killmanager = spawn_obj.GetComponent<FreeEnemySpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (destroy_time > 0) destroy_time--;
        if (destroy_time == 0) Destroy(gameObject);

        if (viability_obj.activeSelf && viability_scr.do_puls && destroy_time == -1)
        {
            if (spawnManager != null)
                spawnManager.death_num++;
            else if (dualspawnManager != null)
                dualspawnManager.death_num++;
            if (killmanager != null)
            {
                killmanager.death_num++;
                killmanager.spawn_count--;
            }
            destroy_time = 240;
        }
    }
}
