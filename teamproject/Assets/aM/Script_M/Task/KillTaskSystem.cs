using UnityEngine;

public class KillTaskSystem : MonoBehaviour
{
    GameObject task_obj;
    ShowTaskSystem tasksystem;

    public string text;
    public string next_task = "null";

    public int need_kill_num;

    public GameObject tasks;
    public GameObject destory_obj;

    bool do_task = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        task_obj = GameObject.Find("Main Camera");
        if (task_obj != null)
            tasksystem = task_obj.GetComponent<ShowTaskSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(do_task && tasksystem != null)
        {
            tasksystem.change_task = text;
            tasksystem.change_task_flag = true;
            tasksystem.kill_enemy_num = need_kill_num;
            tasksystem.now_kill_task = true;
            if (next_task != "null")
                tasksystem.previous_text = next_task;
            else
                tasksystem.previous_text = tasksystem.task;
            if (destory_obj != null)
                tasksystem.destroy_obj = destory_obj;
            do_task = false;
            tasks.SetActive(false);
            Destroy(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            do_task = true;
        }
    }
}
