using UnityEngine;
using UnityEngine.UI;

public class ShowTaskSystem : MonoBehaviour
{
    //1180,760,1f14

    public string task;
    public string change_task;

    int move_time = 0;
    int remove_time = 0;
    int stay_time = 0;
    public bool change_task_flag = false;

    bool pop_now = false;

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

        //現在のタスクと変更前のタスクが違う場合同じにしてポップする
        if (change_task_flag && !pop_now && remove_time == 0) Show_Task();
        if (move_time > 0)
        {
            move_time--;
            pos.y -= 13;
            stay_time = 120;
        }
        if(move_time == 0 && stay_time > 0)
        {
            stay_time--;
        }
        if(remove_time > 0)
        {
            remove_time--;
            pos.y += 14;
        }
        if(stay_time == 0 && pop_now && remove_time == 0)
        {
            remove_time = 30;
            pop_now = false;
        }
    }

    void Show_Task()
    {
        task = change_task;
        Task_Text.text = task;
        move_time = 30;
        pop_now = true;
        change_task_flag = false;
    }

    void Remove_Task()
    {
        move_time = 30;
    }
}
