using UnityEngine;

public class KillTaskSystem : MonoBehaviour
{
    public string text;

    [SerializeField] ShowTaskSystem tasksystem;
    EnemySpawnManager spawnmanager;
    DualEnemySpawnManager dualspawnmanager;

    private void Start()
    {
        spawnmanager = GetComponent<EnemySpawnManager>();
        dualspawnmanager = GetComponent<DualEnemySpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnmanager != null)
        {
            //敵をすべて倒したら次のタスクに進む
            if(spawnmanager.spawn_count == spawnmanager.death_num && spawnmanager.spawn_count != 0)
            {
                tasksystem.change_task = text;
                tasksystem.change_task_flag = true;
                Destroy(this);
            }
        }
        else if (dualspawnmanager != null)
        {
            if (dualspawnmanager.spawn_count == dualspawnmanager.death_num && dualspawnmanager.spawn_count != 0)
            {
                tasksystem.change_task = text;
                tasksystem.change_task_flag = true;
                Destroy(this);
            }
        }
    }
}
