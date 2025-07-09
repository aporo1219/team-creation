using UnityEngine;

public class EnemyStore : MonoBehaviour
{
    GameObject spawn_obj;
    EnemySpawnManager spawnManager;

    [SerializeField] GameObject viability_obj;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawn_obj = transform.parent.gameObject;
        spawnManager = spawn_obj.GetComponent<EnemySpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(viability_obj.activeSelf)
        {
            spawnManager.death_num++;
            Destroy(gameObject);
        }
    }
}
