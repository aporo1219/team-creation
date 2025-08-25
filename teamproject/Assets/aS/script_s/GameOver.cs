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
    PlayerController_y1 controller;
    PlayerStatus status;
    PlayerSceneChecker scenechecker;
    int playerpos_changetime = 0;
    Rigidbody rb;
    GameObject cinemachineCamera;
    CinemachinePanTilt cinemachine;

    InputAction selectAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        if(player != null)
        {
            status = player.GetComponent<PlayerStatus>();
            scenechecker = player.GetComponent<PlayerSceneChecker>();
            controller = player.GetComponent<PlayerController_y1>();
        }

        cinemachineCamera = GameObject.Find("CinemachineCamera");
        cinemachine = cinemachineCamera.GetComponent<CinemachinePanTilt>();

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
        // 現在選択されているオブジェクト
        GameObject current = EventSystem.current.currentSelectedGameObject;

        //すべてのOutlineフレームを非表示
        foreach (var btn in Button)
        {
            if (btn != null)
            {
                var OL = btn.GetComponent<Outline>();
                if (OL != null)
                {
                    OL.enabled = false;
                }
            }
        }

        // 無効または配列外なら最後に選択していたボタンに戻す
        if (current == null || !IsButtonCheck(current))
        {
            EventSystem.current.SetSelectedGameObject(LB);
        }
        else
        {
            //OutLineをＯｎ
            var OL = current.GetComponent<Outline>();
            if (OL != null)
            {
                OL.enabled = true;
            }
            // 有効なボタンなら更新
            LB = current;
        }

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
            controller.AnimationPlay("Idle");
            controller.canAction = true;
            controller.canMove = true;
            controller.canRotate = true;
            SceneChenger.instance.ChangeScene(0);
            AS.PlayOneShot(Push_Button);
       }
       else if(b == G_Button[1])
       {
            if (status != null)
                status.HP = status.MaxHP;
            if (scenechecker.Now_Scene == "Stage1")
            {
                //エレベーター前でリスポーン
                controller.AnimationPlay("Idle");
                player.gameObject.transform.position = new Vector3(-52, 1.5f, -58);
                player.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
                cinemachine.PanAxis.Value = 0;
                cinemachine.TiltAxis.Value = 10;
                controller.canAction = true;
                controller.canMove = true;
                controller.canRotate = true;
                SceneChenger.instance.ChangeScene(3);
            }
            else if (scenechecker.Now_Scene == "Stage2")
            {
                //エレベーター前でリスポーン
                controller.AnimationPlay("Idle");
                player.gameObject.transform.position = new Vector3(0, 1.5f, 23);
                player.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
                cinemachine.PanAxis.Value = 0;
                cinemachine.TiltAxis.Value = 10;
                controller.canAction = true;
                controller.canMove = true;
                controller.canRotate = true;
                SceneChenger.instance.ChangeScene(4);
            }
        }
        
    }

    //テキスト表示関数
    void Display_TEXT()
    {
        GO.text = "ゲームオーバー";
    }

    //ボタンが配列に入っているかのチェック
    bool IsButtonCheck(GameObject b)
    {
        foreach (var bt in Button)
        {
            if (bt != null && bt.gameObject == b)
            {
                return true;
            }
        }

        return false;
    }
}
