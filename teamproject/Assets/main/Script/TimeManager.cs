using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public int time;
    public int second;
    public int minute;
    public int hour;

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Title")
        {
            time = 0;
        }
        if(SceneManager.GetActiveScene().name != "Title" && SceneManager.GetActiveScene().name == "Result" && SceneManager.GetActiveScene().name == "GameOver")
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
