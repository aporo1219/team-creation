using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms.Impl;

public class ClearScene : MonoBehaviour
{
    [SerializeField] Button Button;
    [SerializeField] GameObject GB;
    [SerializeField] Text Result;
    [SerializeField] AudioSource AS;
    [SerializeField] AudioClip Push_Button;

    private Gamepad Gp;
    private GameObject LB;

    private Image TargetImage;
    private Color Push;
    private Color Default;
    private bool IsHover = false;
    private int Isscore;//�X�R�A�̕ϐ�

    private InputAction Select;

    GameObject Timeobj;
    TimeManager time;


    bool On_Click = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //�R���g���[����UI�{�^���̕R�Â�
        EventSystem.current.SetSelectedGameObject(GB);
        LB = GB;
        if (Button != null)
        {
            TargetImage = Button.targetGraphic as Image;
        }

        Display_TEXT();

        Select = InputSystem.actions.FindAction("Select");

        Timeobj = GameObject.Find("PlayTime");
        time = Timeobj.GetComponent<TimeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Select.WasPressedThisFrame() && !On_Click)
        {
            OnButtonPressed();
            On_Click = true;
            Default = TargetImage.color;
            TargetImage.color = Color.yellow;
        }
        if (Select.WasReleasedThisFrame())
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
        SceneChenger.instance.ChangeScene(0);
    }

    //�X�R�A�v�Z�֐�
    void CountScore()
    {

    }

    //�e�L�X�g�\���֐�
    void Display_TEXT()
    {
        Result.text = "�v���C���ԁF" + time.hour + "����" + time.minute + "��" + time.second + "�b" + time.time;
    }
}
