using UnityEngine;

public class KillTaskSystem_a : MonoBehaviour
{
    public string text;

    public ShowTaskSystem tasksystem;
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
        
    }

    public void Next_Task()
    {
        if (spawnmanager != null)
        {
            tasksystem.change_task = text;
            tasksystem.change_task_flag = true;
            Destroy(this);
        }
        else if (dualspawnmanager != null)
        {
            tasksystem.change_task = text;
            tasksystem.change_task_flag = true;
            Destroy(this);
        }
    }
}
