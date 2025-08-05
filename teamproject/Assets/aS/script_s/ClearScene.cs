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
    }

    // Update is called once per frame
    void Update()
    {
        //�����ꂽ�Ƃ��ɐF�ύX,�^�C�g���֖߂�
        if (Select != null && Select.WasPressedThisFrame())
        {
            Default = TargetImage.color;
            TargetImage.color = Color.yellow;
            OnButtonPressed();
        }

        //�f�t�H���g
        if (Select != null && Select.WasReleasedThisFrame())
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
        Result.text = "�X�R�A�� :" + Isscore;
    }
}
