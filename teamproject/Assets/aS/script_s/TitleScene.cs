using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //�R���g���[����UI�{�^���̕R�Â�
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
        
    }

    private void FixedUpdate()
    {
        GameObject Selected = EventSystem.current.currentSelectedGameObject;
        GameObject CrossKey = EventSystem.current.currentSelectedGameObject;

        //�����ꂽ�Ƃ��ɐF�ύX
        //if()
        if(Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            Default = TargetImage.color;
            TargetImage.color = Color.yellow;
        }

        //�f�t�H���g
        if (Gamepad.current.buttonSouth.wasReleasedThisFrame)
        {
            TargetImage.color = Default;
        }
        //�Q�[���p�b�h�̓���
        Gp = Gamepad.current;
        if (Gp == null)
        {
            return;
        }

        // ���݂̑I�������� or null �Ȃ畜�A
        if (Selected == null)
        {
            if (LB!= null)
            {
                EventSystem.current.SetSelectedGameObject(LB);
            }
            else
            {
                // �Ō�̑I�������ݒ�Ȃ� First_Button �ɖ߂�
                EventSystem.current.SetSelectedGameObject(GB);
            }
        }
        else
        {
            // �L����UI�{�^�����I�΂�Ă���΋L�^���Ă���
            LB = Selected;
        }
    }

    void OnButtonPressed()
    {
        AS.PlayOneShot(Push_Button);
        SceneChenger.instance.ChangeScene(2);
    }

}
