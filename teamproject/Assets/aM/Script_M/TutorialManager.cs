using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public bool tutorial_start_flag = true;
    public List<bool> tutorial_clear = new List<bool>();
    [SerializeField] List<GameObject> Tutorial_Obj = new List<GameObject>();

    [SerializeField] ShowTaskSystem tasksystem;

    [SerializeField] BoxCollider button;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //初めのタスク以外表示しない
        Tutorial_Obj[0].SetActive(true);
        for (int i = 1; i < Tutorial_Obj.Count; i++)
        {
            Tutorial_Obj[i].SetActive(false);
        }
        //チュートリアルタスクを表示
        Start_Tutorial();
    }

    //チュートリアルタスクの表示
    void Start_Tutorial()
    {
        tasksystem.change_task = "影を追いかけよう";
        tasksystem.change_task_flag = true;
        tasksystem.assist_text = "Lスティックを傾けて移動";
    }

    //2個目以降のチュートリアルタスクの表示
    public void Tutorial_Clear(int route_num)
    {
        if (route_num < 6)
        {
            Tutorial_Obj[route_num - 1].SetActive(false);
            if (route_num < 5)
                Tutorial_Obj[route_num].SetActive(true);
        }
        switch (route_num)
        {
            case 1:
                tasksystem.change_task = "影を追いかけよう";
                tasksystem.change_task_flag = true;
                tasksystem.assist_text = "Lスティックの傾きによって\nキャラの移動方向も変わる";
                break;
            case 2:
                tasksystem.change_task = "影を追いかけよう";
                tasksystem.change_task_flag = true;
                tasksystem.assist_text = "Rスティックを傾けて視点移動";
                break;
            case 3:
                tasksystem.change_task = "影を追いかけよう";
                tasksystem.change_task_flag = true;
                tasksystem.assist_text = "Aボタンを押してジャンプ";
                break;
            case 4:
                tasksystem.change_task = "影を追いかけよう";
                tasksystem.change_task_flag = true;
                tasksystem.assist_text = "空中でAボタンを押したら\n空中ジャンプ";
                break;
            case 5:
                tasksystem.change_task = "敵を倒そう";
                tasksystem.change_task_flag = true;
                tasksystem.assist_text = "Xボタンを押して攻撃\nRTボタンを押して回避";
                tasksystem.kill_enemy_num = 2;
                break;
            case 6:
                tasksystem.change_task = "宝箱を開けよう";
                tasksystem.change_task_flag = true;
                tasksystem.assist_text = "攻撃を当てると開く";
                break;
            case 7:
                tasksystem.change_task = "ボタンを押そう";
                tasksystem.change_task_flag = true;
                tasksystem.assist_text = "攻撃を当てるとどこかの仕掛けが動く";
                button.enabled = true;
                break;
            case 8:
                tasksystem.change_task = "エレベーターまで行こう";
                tasksystem.change_task_flag = true;
                tasksystem.assist_text = "操作を忘れたらSTARTボタンを押して\nポーズ画面に行って操作説明を見よう";
                break;
        }
    }
}
