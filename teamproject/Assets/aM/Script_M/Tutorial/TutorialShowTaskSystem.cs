using UnityEngine;
using UnityEngine.UI;

public class TutorialShowTaskSystem : MonoBehaviour
{
    public int x;
    public int y;

    public string task;
    public string change_task;
    public string assist_text;

    public float kill_enemy_num = 0;
    public float now_kill_num = 0;

    public bool change_task_flag = false;

    [SerializeField] GameObject Tutorial;
    [SerializeField] GameObject Invisible;

    [SerializeField] GameObject Main_Show_Task;
    [SerializeField] Text Task_Text;
    [SerializeField] GameObject Actionassist;
    [SerializeField] Text Actionassist_Text;

    [SerializeField] GameObject Kill_Slider;

    [SerializeField] Slider slider;

    [SerializeField] TutorialManager manager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        task = change_task = "���݃^�X�N�͂���܂���B";
        Main_Show_Task.transform.position = new Vector3(970, 1000, 0);
        Actionassist.transform.position = new Vector3(970, 850, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //�\���^�X�N�̕ύX
        if (change_task_flag)
            Show_Task();

        //�G��|���^�X�N�̏���
        if (kill_enemy_num != 0 && now_kill_num == kill_enemy_num)
        {
            manager.Tutorial_Clear(6);
            now_kill_num = kill_enemy_num = 0;
            Invisible.SetActive(false);
        }

        if (Kill_Slider != null && task == "�G��|����")
        {
            Task_Text.text = task + "\n" + now_kill_num + " / " + kill_enemy_num;
            slider.value = (float)now_kill_num / (float)kill_enemy_num;
            Kill_Slider.SetActive(true);
        }
        else if (Kill_Slider != null)
        {
            Kill_Slider.SetActive(false);
        }

    }

    //�^�X�N�ύX
    void Show_Task()
    {
        task = change_task;
        if (task == "�G��|����")
            Task_Text.text = task + "\n" + now_kill_num + " / " + kill_enemy_num;
        Task_Text.text = task;
        Actionassist_Text.text = assist_text;
        change_task_flag = false;
    }
}
