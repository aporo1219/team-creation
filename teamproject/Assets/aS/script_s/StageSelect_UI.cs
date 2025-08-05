using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using Unity.Cinemachine;

public class StageSelectUI : MonoBehaviour
{
    private int f;
    private int Level_Floor = 0;
    private Gamepad GP;

    [SerializeField] Text Floor_Display;
    [SerializeField] Button[] Button;
    [SerializeField] GameObject[] G_Button;
    [SerializeField] GameObject First_Button;
    private GameObject Last_Button;

    private bool Push_Button;

    GameObject cinemachineCamera;
    CinemachinePanTilt cinemachine;

    GameObject player;
    int playerpos_changetime = 0;
    int floor_num;

    InputAction Select;

    //SE
    [SerializeField] private AudioSource AS;
    [SerializeField] private AudioClip Push_SE;
    private float Push_SE_v = 0.7f;

    public SceneChenger SC;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cinemachineCamera = GameObject.Find("CinemachineCamera");
        cinemachine = cinemachineCamera.GetComponent<CinemachinePanTilt>();

        player = GameObject.Find("Player");

        Select = InputSystem.actions.FindAction("Select");

        FloorDisplay(Level_Floor);
        //コントローラとUIボタンの紐づけ
        EventSystem.current.SetSelectedGameObject(First_Button);

        Last_Button = First_Button;

        Push_Button = false;

        f = 0;
        //ボタンの管理
        for (int i = 0; i < Button.Length; i++)
        {
            if (Button[i] == null && G_Button[i] == null)
            {
                Debug.LogError($"Button[{i}] が設定されていません！");
                Debug.LogError($"GB[{i}] が設定されていません！");
            }
            else
            { 
                Debug.Log($"Button[{i}] にリスナー登録完了");
                Debug.Log($"GB[{i}] にリスナー登録完了");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if(Time.timeScale == 0)
        //{
        //    return;
        //}
        if (playerpos_changetime != 0) playerpos_changetime--;
        if (playerpos_changetime == 1)
        {
            if (floor_num == 1)
            {
                player.gameObject.transform.position = new Vector3(-52, 1.5f, -58);
                player.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
                cinemachine.PanAxis.Value = 0;
                cinemachine.TiltAxis.Value = 10;
            }
            if (floor_num == 2)
            {
                player.gameObject.transform.position = new Vector3(0, 1.5f, 23);
                player.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
                cinemachine.PanAxis.Value = 0;
                cinemachine.TiltAxis.Value = 10;
            }
            if (floor_num == 0)
            {
                player.gameObject.transform.position = new Vector3(-118, 22, 5);
                player.gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
                cinemachine.PanAxis.Value = 180;
                cinemachine.TiltAxis.Value = 10;
            }
        }
        //Bボタンがおされた処理
        if (Select != null && Select.WasPressedThisFrame())
        {
            GameObject selected = EventSystem.current.currentSelectedGameObject;

            if (selected != null)
            {
                for (int i = 0; i < Button.Length; i++)
                {
                    if (Button[i] != null && Button[i].gameObject == selected)
                    {
                        Debug.Log("Bボタンで選択中のボタンを決定！");
                        OnButtonPressed(G_Button[i]);
                        break;
                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {

    }

    //gameObjectがUIボタン配列に入っているかの確認
    bool IsButtonCheck(GameObject b)
    {
        foreach(var bt in Button)
        {
            if(bt != null && bt.gameObject == b)
            {
                return true;
            }
        }

        return false;
    }

    //ボタンが押された時の処理
    public void OnButtonPressed(GameObject floor)
    {
       
        int FloorNum = 0;
        if (!Push_Button)
        {
            Debug.Log("OnButtonPressed");
            Push_Button = true;
            if(floor == G_Button[0])
            {
                Debug.Log("チュートリアルステージへ");
                Invoke(nameof(ChangeScene1), 0.1f);
                f = 0;
                Level_Floor = 0;
                FloorDisplay(Level_Floor);
                FloorNum = 0;
                //SE
                AS.PlayOneShot(Push_SE);
                AS.volume = Push_SE_v;
            }
            else if(floor == G_Button[1])
            {
                Debug.Log("1ステージへ");
                Invoke(nameof(ChangeScene1), 0.1f);
                f = 1;
                Level_Floor = 2;
                FloorDisplay(Level_Floor);
                FloorNum = 1;
                //SE
                AS.PlayOneShot(Push_SE);
                AS.volume = Push_SE_v;
            }
            else if(floor == G_Button[2])
            {
                Debug.Log("2ステージへ");
                Invoke(nameof(ChangeScene1), 0.1f);
                f = 2;
                Level_Floor = 1;
                FloorDisplay(Level_Floor);
                FloorNum = 2;
                //SE
                AS.PlayOneShot(Push_SE);
                AS.volume = Push_SE_v;
            }
            else if(floor == G_Button[3])
            {
                Debug.Log("最終ステージへ");
                Invoke(nameof(ChangeScene1), 0.1f);
                f = 3;
                Level_Floor = 10;
                FloorDisplay(Level_Floor);
                //SE
                AS.PlayOneShot(Push_SE);
                AS.volume = Push_SE_v;
            }

            floor_num = FloorNum;
            playerpos_changetime = 35;
        }
    }

    //シーン移動
    private void ChangeScene1()
    {
       switch(f)
        {
            case 3:
                SceneManager.LoadScene("Finalstage");
                break;
            case 2:
                //StartCoroutine("SceneChenger.instance.ChangeScene()", 4);
                SceneChenger.instance.ChangeScene(4);
                break;
            case 1:
                SceneChenger.instance.ChangeScene(3);
                break;
            case 0:
                SceneChenger.instance.ChangeScene(2);
                break;
        }
    }

    //フロア表示
    void FloorDisplay(int F)
    {
        F = Level_Floor;
        //一階
        if (F == 10)
        {
            Floor_Display.text = "1";
        }
        //チュートリアル
        else if (F == 0)
        {
            Floor_Display.text = "BT" + 2;
        }
        else
        {
            Floor_Display.text = "B" + F;
        }

    }

}
