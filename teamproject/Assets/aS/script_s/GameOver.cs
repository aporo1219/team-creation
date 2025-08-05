using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms.Impl;
using Unity.Cinemachine;
using UnityEngine.InputSystem.Switch;

public class GameOver : MonoBehaviour
{
    [SerializeField] Button[] Button;
    [SerializeField] GameObject[] G_Button;
    [SerializeField] GameObject GB;
    [SerializeField] Text GO;
    [SerializeField] AudioSource AS;
    [SerializeField] AudioClip Push_Button;
    
    private Gamepad Gp;
    private GameObject LB;

    private Image TargetImage;
    private Color Push;
    private Color Default;
    private bool IsHover = false;

    GameObject player;
    int playerpos_changetime = 0;
    Rigidbody rb;
    GameObject cinemachineCamera;
    CinemachinePanTilt cinemachine;

    InputAction selectAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
        if(player == null)
        { 
        //player = GameObject.Find("Player");
        //rb = player.GetComponent<Rigidbody>();
        //cinemachineCamera = GameObject.Find("CinemachineCamera");
        //cinemachine = cinemachineCamera.GetComponent<CinemachinePanTilt>();
        }
        selectAction = InputSystem.actions.FindAction("Select");

        //コントローラとUIボタンの紐づけ
        EventSystem.current.SetSelectedGameObject(GB);
        LB = GB;
        //ボタンの登録
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
        
        Display_TEXT();
    }

        
    

    // Update is called once per frame
    void Update()
    {
        
        //Bボタンがおされた処理
        if (selectAction != null && selectAction.WasPressedThisFrame())
        {
            Debug.Log("あ");
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

    //ボタンが押されたら
    void OnButtonPressed(GameObject b)
    {
       if(b == G_Button[0])
       {
            SceneChenger.instance.ChangeScene(0);
            AS.PlayOneShot(Push_Button);
       }
       else if(b == G_Button[1])
       {
            if (FloorChecker.Instance.Current_Floor == 1)
            {
                SceneChenger.instance.ChangeScene(3);
            }
            else if (FloorChecker.Instance.Current_Floor == 2)
            {
                SceneChenger.instance.ChangeScene(4);
            }
        }
        
    }

    //テキスト表示関数
    void Display_TEXT()
    {
        GO.text = "ゲームオーバー";
    }
}
