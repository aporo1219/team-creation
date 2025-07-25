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

    GameObject cinemachineCamera;
    CinemachinePanTilt cinemachine;

    GameObject player;
    int playerpos_changetime = 0;
    int floor_num;

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

        FloorDisplay(Level_Floor);
        //コントローラとUIボタンの紐づけ
        EventSystem.current.SetSelectedGameObject(First_Button);

        Last_Button = First_Button;

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
                int index = i;
                Button[i].onClick.AddListener(() => OnButtonPressed(index));
                //G_Button[i].SetActive(false);
                if (Release(index) || G_Button[i] == First_Button || i == 2|| i == 1)
                {
                    G_Button[i].SetActive(true);
                }
                else
                {
                    G_Button[i].SetActive(false);
                }
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
            if(floor_num == 0)
            {
                player.gameObject.transform.position = new Vector3(-118, 22, 5);
                player.gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
                cinemachine.PanAxis.Value = 180;
                cinemachine.TiltAxis.Value = 10;
            }
        }
    }

    private void FixedUpdate()
    {
        GameObject Selected = EventSystem.current.currentSelectedGameObject;
        GameObject CrossKey = EventSystem.current.currentSelectedGameObject;

        //ゲームパッドの入力
        GP = Gamepad.current;
        if (GP == null)
        {
            return;
        }

        //スティックの方向取得
        Vector2 Right_Stick = GP.rightStick.ReadValue();
        //十字キーの方向取得
        Vector2 Cross = GP.dpad.ReadValue();


        //ボタンの色の変更
        for (int i = 0; i < Button.Length; i++)
        {
            if (Button[i] != null)
            {
                if (Button[i].GetComponent<Button_s>() == null)
                {
                    Button[i].gameObject.AddComponent<Button_s>();

                }
            }
        }

        // 現在の選択が無効 or null なら復帰
        if (Selected == null || !IsButtonCheck(Selected))
        {
            if (Last_Button != null)
            {
                EventSystem.current.SetSelectedGameObject(Last_Button);
            }
            else
            {
                // 最後の選択が未設定なら First_Button に戻す
                EventSystem.current.SetSelectedGameObject(First_Button);
            }
        }
        else
        {
            // 有効なUIボタンが選ばれていれば記録しておく
            Last_Button = Selected;
        }
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

    //ステージ解放
    bool Release(int stage)
    {
        return PlayerPrefs.GetInt("Release_" + stage, 0) == 1;

        PlayerPrefs.SetInt("StageUnlocked_1", 1); // ステージ1を解放
        PlayerPrefs.Save();
    }

    //ボタンが押された時の処理
    public void OnButtonPressed(int floor)
    {
        switch(floor)
        {
            case 3:
                Debug.Log("最終ステージへ");
                Invoke(nameof(ChangeScene1), 0.1f);
                f = 3;
                Level_Floor = 10;
                FloorDisplay(Level_Floor);
                //SE
                AS.PlayOneShot(Push_SE);
                AS.volume = Push_SE_v;
                break;
            case 2:
                Debug.Log("2ステージへ");
                Invoke(nameof(ChangeScene1), 0.1f);
                f = 2;
                Level_Floor = 1;
                FloorDisplay(Level_Floor);
                //SE
                AS.PlayOneShot(Push_SE);
                AS.volume = Push_SE_v;
                break;
            case 1:
                Debug.Log("1ステージへ");
                Invoke(nameof(ChangeScene1), 0.1f);
                f = 1;
                Level_Floor = 2;
                FloorDisplay(Level_Floor);
                //SE
                AS.PlayOneShot(Push_SE);
                AS.volume = Push_SE_v;
                break;
            case 0:
                Debug.Log("チュートリアルステージへ");
                Invoke(nameof(ChangeScene1), 0.1f);
                f = 0;
                Level_Floor = 0;
                FloorDisplay(Level_Floor);
                //SE
                AS.PlayOneShot(Push_SE);
                AS.volume = Push_SE_v;
                break;
        }
    }

    public void OnButtonPressed2(int floor)
    {
        floor_num = floor;
        playerpos_changetime = 35;
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
