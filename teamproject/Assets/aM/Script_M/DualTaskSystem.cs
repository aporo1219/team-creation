using UnityEngine;

public class DualTaskSystem : MonoBehaviour
{
    public bool finish_task = false;

    public string text;
    public string now_task;

    [SerializeField] DualEnemySpawnManager dualspawnmanager;
    [SerializeField] ShowTaskSystem tasksystem;

    // Update is called once per frame
    void Update()
    {
        if(finish_task)
        {
            tasksystem.change_task = text;
            tasksystem.change_task_flag = true;

            if (text == "“G‚ð“|‚»‚¤")
            {
                tasksystem.kill_enemy_num = dualspawnmanager.spawn_count;
            }
            Destroy(this);
        }
    }
}
