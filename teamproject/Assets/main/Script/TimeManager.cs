using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public int time;
    public int second;
    public int minute;
    public int hour;

    GameObject ClearObj;
    ClearScene clear;

    GameObject scene;
    SceneNameChecker scenename;

    // Update is called once per frame
    void Update()
    {
        scene = GameObject.Find("Scene");
        scenename = scene.GetComponent<SceneNameChecker>();

        if (scenename.scene == "Title")
        {
            time = 0;
        }
        if(scenename.scene != "Title" && scenename.scene != "Result" && scenename.scene != "GameOver")
        {
            time++;
        }

        if(time == 60)
        {
            second++;
            time = 0;
        }
        if(second == 60)
        {
            minute++;
            second = 0;
        }
        if (minute == 60)
        {
            hour++;
            minute = 0;
        }
    }
}
