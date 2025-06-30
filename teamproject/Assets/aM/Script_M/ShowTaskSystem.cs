using UnityEngine;
using UnityEngine.UI;

public class ShowTaskSystem : MonoBehaviour
{
    //1180,760

    public string task;
    public string change_task;

    public int move_time = 0;
    public int stay_time = 0;
    public bool show_task_flag = false;

    int show_pos = 774;

    [SerializeField] GameObject Main_Show_Task;
    [SerializeField] Text Task_Text;

    Vector3 pos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        task = change_task = "現在タスクはありません。";
        pos = new Vector3(960, 1180, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Main_Show_Task.transform.position = pos;

        if (task != change_task)
        {
            Show_Task();
        }

        if (move_time != 0) move_time--;
        if (stay_time != 0) stay_time--;
        //タスクを出すモーション
        if (move_time != 0 && !show_task_flag)
        {
            pos.y -= 14;
        }
        if (stay_time == 0 && pos.y == show_pos && !show_task_flag) Remove_Task();
        if (stay_time == 0 && move_time == 0 && !show_task_flag) stay_time = 120;
        //タスクをしまうモーション
        if (move_time != 0 && show_task_flag)
        {
            pos.y += 14;
        }
        else
        {
            show_task_flag = false;
        }
        
    }

    void Show_Task()
    {
        task = change_task;
        Task_Text.text = task;
        move_time = 30;
    }

    void Remove_Task()
    {
        move_time = 30;
        show_task_flag = true;
    }
}
