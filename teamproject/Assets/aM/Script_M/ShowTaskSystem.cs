using UnityEngine;
using UnityEngine.UI;

public class ShowTaskSystem : MonoBehaviour
{
    //1180,760,1f14
    //

    public string task;
    public string change_task;
    public string assist_text;

    int move_time = 0;
    int remove_time = 0;
    int stay_time = 0;
    public bool change_task_flag = false;

    bool pop_now = false;

    [SerializeField] GameObject Tutorial;

    [SerializeField] GameObject Main_Show_Task;
    [SerializeField] Text Task_Text;
    [SerializeField] GameObject Actionassist;
    [SerializeField] Text Actionassist_Text;

    Vector3 task_pos;
    Vector3 assist_pos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        task = change_task = "現在タスクはありません。";
        task_pos = new Vector3(960, 1180, 0);
        assist_pos = new Vector3(960, 300, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //チュートリアルが最後まで来た場合文字サイズを少し小さくする
        if (change_task == "エレベーターまで行こう") Actionassist_Text.fontSize = 55;
        else Actionassist_Text.fontSize = 60;
        Actionassist.transform.position = assist_pos;

        Main_Show_Task.transform.position = task_pos;

        //現在のタスクと変更前のタスクが違う場合同じにしてポップする
        if (change_task_flag && !pop_now && remove_time == 0) Show_Task();
        if (move_time > 0)
        {
            move_time--;
            task_pos.y -= 13;
            stay_time = 120;
        }
        if(move_time == 0 && stay_time > 0)
        {
            stay_time--;
        }
        if(remove_time > 0)
        {
            remove_time--;
            task_pos.y += 14;
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
        Actionassist_Text.text = assist_text;
        move_time = 30;
        pop_now = true;
        change_task_flag = false;
    }

    void Remove_Task()
    {
        move_time = 30;
    }
}
