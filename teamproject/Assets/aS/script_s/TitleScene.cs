using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour
{
    [SerializeField] Button Button;
    [SerializeField] GameObject GB;
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

    bool On_Click = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        rb = player.GetComponent<Rigidbody>();
        cinemachineCamera = GameObject.Find("CinemachineCamera");
        cinemachine = cinemachineCamera.GetComponent<CinemachinePanTilt>();

        selectAction = InputSystem.actions.FindAction("Select");

        //コントローラとUIボタンの紐づけ
        EventSystem.current.SetSelectedGameObject(GB);
        LB = GB;
        Button.onClick.AddListener(() => OnButtonPressed());
        if (Button != null)
        {
            TargetImage = Button.targetGraphic as Image;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerpos_changetime != 0) 
            playerpos_changetime--;
        if (playerpos_changetime == 1)
        {
            rb.isKinematic = true;
            player.transform.position = new Vector3(0, 1.7f, 0);
            player.transform.rotation = new Quaternion(0, 0, 0, 0);
            cinemachine.PanAxis.Value = 0;
            cinemachine.TiltAxis.Value = 10;
            rb.isKinematic = false;
        }

        if (selectAction.WasPressedThisFrame() && !On_Click)
        {
            OnButtonPressed();
            On_Click = true;
            Default = TargetImage.color;
            TargetImage.color = Color.yellow;
        }
        if(selectAction.WasReleasedThisFrame())
        {
            TargetImage.color = Default;
        }
    }

    private void FixedUpdate()
    {
        /*GameObject Selected = EventSystem.current.currentSelectedGameObject;
        GameObject CrossKey = EventSystem.current.currentSelectedGameObject;

        //押されたときに色変更
        //if()
        if(Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            Default = TargetImage.color;
            TargetImage.color = Color.yellow;
        }

        //デフォルト
        if (Gamepad.current.buttonSouth.wasReleasedThisFrame)
        {
            TargetImage.color = Default;
        }
        //ゲームパッドの入力
        Gp = Gamepad.current;
        if (Gp == null)
        {
            return;
        }

        // 現在の選択が無効 or null なら復帰
        if (Selected == null)
        {
            if (LB!= null)
            {
                EventSystem.current.SetSelectedGameObject(LB);
            }
            else
            {
                // 最後の選択が未設定なら First_Button に戻す
                EventSystem.current.SetSelectedGameObject(GB);
            }
        }
        else
        {
            // 有効なUIボタンが選ばれていれば記録しておく
            LB = Selected;
        }*/
    }

    void OnButtonPressed()
    {
        AS.PlayOneShot(Push_Button);
        SceneChenger.instance.ChangeScene(2);
        playerpos_changetime = 20;
    }

}
