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
    [SerializeField] Text Score;
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

    GameObject Scoreobj;
    ScoreManager scoremanager;


    bool On_Click = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //�R���g���[����UI�{�^���̕R�Â�
        //EventSystem.current.SetSelectedGameObject(GB);
        LB = GB;
        if (Button != null)
        {
            TargetImage = Button.targetGraphic as Image;
        }

        Select = InputSystem.actions.FindAction("Select");

        Scoreobj = GameObject.Find("Score");
        scoremanager = Scoreobj.GetComponent<ScoreManager>();

        Display_TEXT();
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
        Result.text = "�v���C���ԁF" + scoremanager.hour + "����" + scoremanager.minute + "��" + scoremanager.second + "�b" + scoremanager.time;
        Score.text = "�G�L�����F" + scoremanager.kill;
    }
}
