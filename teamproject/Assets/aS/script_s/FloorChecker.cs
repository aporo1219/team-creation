using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Cinemachine;

public class FloorChecker : MonoBehaviour
{
    //インスタンス
    public static FloorChecker Instance;
    public  int Current_Floor = 0;

    GameObject Player;
    GameObject CinemachineCamera;
    CinemachinePanTilt cinemachine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        //プレイヤー情報の取得
        CinemachineCamera = GameObject.Find("CinemachineCamera");
        cinemachine = CinemachineCamera.GetComponent<CinemachinePanTilt>();
        Player = GameObject.Find("Player");

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

    public void Scene_Player()
    {

    }
}
