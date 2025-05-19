using UnityEngine;

public class PoseManager_M : MonoBehaviour
{
    int showing_pose;//ポーズが表示されてるか

    public GameObject pose_obj;//ポーズキャンバスを読み込む

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        showing_pose = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Escapeキーの入力でポーズ画面の切り替えを行う
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Change_Pose(showing_pose);
            showing_pose++;
        }
        if (showing_pose > 1)
            showing_pose = 0;
    }

    //ポーズの表示非表示を管理する関数
    void Change_Pose(int showing)
    {
        if (showing == 0)
        {
            pose_obj.SetActive(true);
            Debug.Log("1");
        }
        else
        {
            pose_obj.SetActive(false);
            Debug.Log("2");
        }
    }
}
