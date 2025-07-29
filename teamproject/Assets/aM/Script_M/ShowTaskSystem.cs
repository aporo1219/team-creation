using UnityEngine;
using UnityEngine.UI;

public class ShowTaskSystem : MonoBehaviour
{
    //1180,760,1f14
    //

    public int now_scene = 0;

    public string task;
    public string change_task;
    public string assist_text;

    public float kill_enemy_num = 0;
    public float now_kill_num = 0;

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

    [SerializeField] GameObject Kill_Slider;

    [SerializeField] TutorialManager manager;

    Vector3 task_pos;
    Vector3 assist_pos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        task = change_task = "現在タスクはありません。";
        task_pos = new Vector3(960, 1180, 0);
        if (now_scene == 0)
            assist_pos = new Vector3(960, 300, 0);
        else
            assist_pos = new Vector3(960, -300, 0);
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

        //敵を倒すタスクの処理
        if(kill_enemy_num != 0 && now_kill_num == kill_enemy_num)
        {
            //チュートリアル
            if(assist_text == "Xボタンを押して攻撃\nRTボタンを押して回避")
            {
                manager.Tutorial_Clear(6);
                now_kill_num = kill_enemy_num = 0;
            }
        }

        if(Kill_Slider != null && task == "敵を倒そう")
        {
            task = Task_Text.text = task + "\n" + now_kill_num + " / " + kill_enemy_num;
            Kill_Slider.SetActive(true);
        }
        else if(Kill_Slider != null)
        {
            Kill_Slider.SetActive(false);
        }
    }

    void Show_Task()
    {
        task = change_task;
        if (task == "敵を倒そう")
            task = change_task + "\n" + now_kill_num + " / " + kill_enemy_num;
        Task_Text.text = task;
        Actionassist_Text.text = assist_text;
        if (task != "現在タスクはありません。")
        {
            move_time = 30;
            pop_now = true;
        }
        change_task_flag = false;
    }
}
