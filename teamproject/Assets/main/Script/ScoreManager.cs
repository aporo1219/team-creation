using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    [Header("プレイ時間")]
    public int time;
    public int second;
    public int minute;
    public int hour;

    [Header("敵のキル数")]
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
        //シーンチェック
        scene = GameObject.Find("Scene");
        if (scene != null)
            scenename = scene.GetComponent<SceneNameChecker>();

        if (scenename != null)
        {         //シーンによってプレイ時間を増やすかどうか変更
            if (scenename.scene == "Title")
            {
                time = 0;
            }
            if (scenename.scene != "Title" && scenename.scene != "Result" && scenename.scene != "GameOver")
            {
                time++;
            }
        }

        //時間
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
            //タスクが「敵を倒せ」ならタスク監理の倒した数にキル数を追加する
            if (tasksystem.now_kill_task && tasksystem != null)
            {
                tasksystem.now_kill_num++;
            }
            kill++;
            kill_puls--;
        }
    }
}
