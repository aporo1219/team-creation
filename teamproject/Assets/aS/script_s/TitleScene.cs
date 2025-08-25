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
    int GameStartTime = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        rb = player.GetComponent<Rigidbody>();
        cinemachineCamera = GameObject.Find("CinemachineCamera");
        cinemachine = cinemachineCamera.GetComponent<CinemachinePanTilt>();

        selectAction = InputSystem.actions.FindAction("Select");

        //コントローラとUIボタンの紐づけ
        //EventSystem.current.SetSelectedGameObject(GB);
        LB = GB;
        Button.onClick.AddListener(() => OnButtonPressed());
        if (Button != null)
        {
            TargetImage = Button.targetGraphic as Image;
        }
        GameStartTime = 30;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerpos_changetime != 0)
            playerpos_changetime--;
        if (playerpos_changetime == 1)
        {
            rb.isKinematic = true;
            player.transform.position = new Vector3(0, 1.5f, 0);
            player.transform.rotation = new Quaternion(0, 0, 0, 0);
            cinemachine.PanAxis.Value = 0;
            cinemachine.TiltAxis.Value = 10;
            rb.isKinematic = false;
        }

        if (GameStartTime != 0) GameStartTime--;

        if (selectAction.WasPressedThisFrame() && !On_Click && GameStartTime == 0)
        {
            OnButtonPressed();
            On_Click = true;
            Default = TargetImage.color;
            TargetImage.color = Color.yellow;
        }
        if (selectAction.WasReleasedThisFrame() && GameStartTime == 0)
        {
            TargetImage.color = Default;
        }
    }

    private void FixedUpdate()
    {
      
    }

    void OnButtonPressed()
    {
        AS.PlayOneShot(Push_Button);
        SceneChenger.instance.ChangeScene(2);
        playerpos_changetime = 20;
    }

}
