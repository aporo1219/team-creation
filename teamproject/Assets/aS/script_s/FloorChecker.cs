using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorChecker : MonoBehaviour
{
    //インスタンス
    public static FloorChecker Instance;
    public  int Current_Floor = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        

    }
    // Update is called once per frame
    void Update()
    {
        //Current_Floorが１だたったらゲームオーバーからやり直すときステージ１に戻す。２ならばステージ２
        if(SceneManager.GetActiveScene().name == "stage1")
        {
            Current_Floor = 1;
        }
        else if(SceneManager.GetActiveScene().name == "stage2")
        {
            Current_Floor = 2;
        }
    }
}
