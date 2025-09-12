using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    [Header("�v���C����")]
    public int time;
    public int second;
    public int minute;
    public int hour;

    [Header("�G�̃L����")]
    public int kill;
    public int kill_puls;

    GameObject ClearObj;
    ClearScene clear;

    GameObject scene;
    SceneNameChecker scenename;

    GameObject task_obj;
    ShowTaskSystem tasksystem;

    // Update is called once per frame
    void Update()
    {
        //�V�[���`�F�b�N
        scene = GameObject.Find("Scene");
        if (scene != null)
            scenename = scene.GetComponent<SceneNameChecker>();

        if (scenename != null)
        {         //�V�[���ɂ���ăv���C���Ԃ𑝂₷���ǂ����ύX
            if (scenename.scene == "Title")
            {
                time = 0;
            }
            if (scenename.scene != "Title" && scenename.scene != "Result" && scenename.scene != "GameOver")
            {
                time++;
            }
        }

        //����
        if (time == 60)
        {
            second++;
            time = 0;
        }
        if (second == 60)
        {
            minute++;
            second = 0;
        }
        if (minute == 60)
        {
            hour++;
            minute = 0;
        }

        if (kill_puls > 0)
        {
            task_obj = GameObject.Find("Main Camera");
            if (task_obj != null)
                tasksystem = task_obj.GetComponent<ShowTaskSystem>();
            //�^�X�N���u�G��|���v�Ȃ�^�X�N�ė��̓|�������ɃL������ǉ�����
            if (tasksystem.now_kill_task && tasksystem != null)
            {
                tasksystem.now_kill_num++;
            }
            kill++;
            kill_puls--;
        }
    }
}
