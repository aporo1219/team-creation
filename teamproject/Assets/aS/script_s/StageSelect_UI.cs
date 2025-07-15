using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class StageSelectUI : MonoBehaviour
{
    private int f;
    private int Level_Floor = 0;
    private Gamepad GP;

    [SerializeField] Text Floor_Display;
    [SerializeField] Button[] Button;
    [SerializeField] GameObject[] G_Button;
    [SerializeField] GameObject First_Button;
    [SerializeField] GameObject[] GB;

    //SE
    [SerializeField] private AudioSource AS;
    [SerializeField] private AudioClip Select_SE;
    [SerializeField] private AudioClip Push_SE;
    private float Select_SE_v = 0.5f;
    private float Push_SE_v = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FloorDisplay(Level_Floor);
        //コントローラとUIボタンの紐づけ
        EventSystem.current.SetSelectedGameObject(First_Button);

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
                if (Release(index) || G_Button[i] == First_Button || i == 2)
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
        if(Time.timeScale == 0)
        {
            return;
        }
    }

    private void FixedUpdate()
    {
        GameObject Selected = EventSystem.current.currentSelectedGameObject;
        //ゲームパッドの入力
        GP = Gamepad.current;
        if (GP == null)
        {
            return;
        }
        Vector2 Right_Stick = GP.rightStick.ReadValue();


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
                Invoke(nameof(ChangeScene), 0.1f);
                f = 3;
                Level_Floor = 10;
                FloorDisplay(Level_Floor);
                //SE
                AS.PlayOneShot(Push_SE);
                AS.volume = Push_SE_v;
                break;
            case 2:
                Debug.Log("2ステージへ");
                Invoke(nameof(ChangeScene), 0.1f);
                f = 2;
                Level_Floor = 1;
                FloorDisplay(Level_Floor);
                //SE
                AS.PlayOneShot(Push_SE);
                AS.volume = Push_SE_v;
                break;
            case 1:
                Debug.Log("1ステージへ");
                Invoke(nameof(ChangeScene), 0.1f);
                f = 1;
                Level_Floor = 2;
                FloorDisplay(Level_Floor);
                //SE
                AS.PlayOneShot(Push_SE);
                AS.volume = Push_SE_v;
                break;
            case 0:
                Debug.Log("チュートリアルステージへ");
                Invoke(nameof(ChangeScene), 0.1f);
                f = 0;
                Level_Floor = 0;
                FloorDisplay(Level_Floor);
                //SE
                AS.PlayOneShot(Push_SE);
                AS.volume = Push_SE_v;
                break;
        }
    }

    //シーン移動
    private void ChangeScene()
    {
       switch(f)
        {
            case 3:
                SceneManager.LoadScene("Finalstage");
                break;
            case 2:
                SceneManager.LoadScene("stage2");
                break;
            case 1:
                SceneManager.LoadScene("stage1");
                break;
            case 0:
                SceneManager.LoadScene("tutorial");
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
