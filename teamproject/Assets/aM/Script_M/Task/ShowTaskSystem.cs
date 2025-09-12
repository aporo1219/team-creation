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
    public string previous_text = "null";

    public float kill_enemy_num = 0;
    public float now_kill_num = 0;

    public bool now_kill_task = false;

    int move_time = 0;
    int remove_time = 0;
    int stay_time = 0;
    public bool change_task_flag = false;

    bool pop_now = false;

    [SerializeField] GameObject Main_Show_Task;
    [SerializeField] Text Task_Text;
    [SerializeField] GameObject Actionassist;
    [SerializeField] Text Actionassist_Text;

    [SerializeField] GameObject Kill_Slider;
    [SerializeField] Slider Slider;

    public GameObject tasks;
    public GameObject destroy_obj;

    Vector3 task_pos;
    Vector3 assist_pos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        task = change_task = "現在タスクはありません。";
        task_pos = new Vector3(Main_Show_Task.transform.position.x, 1150, 0);
        Actionassist.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Main_Show_Task.transform.position = task_pos;

        //現在のタスクと変更前のタスクが違う場合同じにしてポップする
        if (change_task_flag && !pop_now && remove_time == 0) Show_Task();
        if (move_time > 0)
        {
            move_time--;
            task_pos.y -= 13;
            stay_time = 120;
        }
        if (move_time == 0 && stay_time > 0)
        {
            stay_time--;
        }
        if (remove_time > 0)
        {
            remove_time--;
            task_pos.y += 13;
        }
        if (stay_time == 0 && pop_now && remove_time == 0)
        {
            remove_time = 30;
            pop_now = false;
        }

        if (Kill_Slider != null && task == "敵を倒そう")
        {
            Task_Text.text = task + "\n" + now_kill_num + " / " + kill_enemy_num;
            Slider.value = (float)now_kill_num / (float)kill_enemy_num;
            Kill_Slider.SetActive(true);
        }
        else if (Kill_Slider != null)
        {
            Kill_Slider.SetActive(false);
        }

        //
        if (now_kill_num >= kill_enemy_num && kill_enemy_num != 0)
        {
            now_kill_task = false;
            kill_enemy_num = now_kill_num = 0;
            Kill_Slider.SetActive(false);
            change_task = previous_text;
            previous_text = "null";
            if (destroy_obj != null)
            {
                destroy_obj.SetActive(false);
                destroy_obj = null;
            }
            tasks.SetActive(true);
            Show_Task();
        }
    }

    void Show_Task()
    {
        task = change_task;
        if (task == "敵を倒そう")
            Task_Text.text = task + "\n" + now_kill_num + " / " + kill_enemy_num;
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
