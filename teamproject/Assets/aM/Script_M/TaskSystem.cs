using UnityEngine;

public class TaskSystem : MonoBehaviour
{
    public string text;

    public string now_text;

    [SerializeField] ShowTaskSystem tasksystem;
    EnemySpawnManager spawnmanager;
    DualEnemySpawnManager dualspawnmanager;
    ButtonSystem buttonsystem;

    private void Start()
    {
        spawnmanager = GetComponent<EnemySpawnManager>();
        dualspawnmanager = GetComponent<DualEnemySpawnManager>();
        buttonsystem = GetComponent<ButtonSystem>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (spawnmanager != null)
        {
            if (other.tag == "Player" && now_text == tasksystem.task && spawnmanager.spawn_count != 0)
            {
                tasksystem.change_task = text;
                tasksystem.change_task_flag = true;

                if (spawnmanager != null)
                {
                    if (text == "�G��|����")
                    {
                        tasksystem.kill_enemy_num = spawnmanager.spawn_count;
                    }
                }

                if (gameObject.name == "Task")
                    Destroy(gameObject);
                else
                    Destroy(this);
            }
        }
        else if(dualspawnmanager != null)
        {
            if (other.tag == "Player" && now_text == tasksystem.task && dualspawnmanager.spawn_count != 0)
            {
                tasksystem.change_task = text;
                tasksystem.change_task_flag = true;

                if (dualspawnmanager != null)
                {
                    if (text == "�G��|����")
                    {
                        tasksystem.kill_enemy_num = dualspawnmanager.spawn_count;
                    }
                }

                if (gameObject.name == "Task")
                    Destroy(gameObject);
                else
                    Destroy(this);
            }
        }
        else if(buttonsystem != null)
        {
            if((other.gameObject.name == "GC" ||  other.gameObject.name == "GF") && now_text == tasksystem.task)
            {
                tasksystem.change_task = text;
                tasksystem.change_task_flag = true;
                Destroy(this);
            }
        }
        else
        {
            if (other.tag == "Player" && now_text == tasksystem.task)
            {
                tasksystem.change_task = text;
                tasksystem.change_task_flag = true;

                if (gameObject.name == "Task")
                    Destroy(gameObject);
                else
                    Destroy(this);
            }
        }
    }
}
