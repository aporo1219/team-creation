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
       

        player = GameObject.Find("Player");
        rb = player.GetComponent<Rigidbody>();
        cinemachineCamera = GameObject.Find("CinemachineCamera");
        cinemachine = cinemachineCamera.GetComponent<CinemachinePanTilt>();

        selectAction = InputSystem.actions.FindAction("Select");

        //�R���g���[����UI�{�^���̕R�Â�
        EventSystem.current.SetSelectedGameObject(GB);
        LB = GB;
        //�{�^���̓o�^
        for (int i = 0; i < Button.Length; i++)
        {
            int index = i;
            Button[i].onClick.AddListener(() => OnButtonPressed(index));
            if (Button != null)
            {
                TargetImage = Button[i].targetGraphic as Image;
            }
        }

        Display_TEXT();
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
        if (Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            Default = TargetImage.color;
            TargetImage.color = Color.yellow;
            GameObject selected = EventSystem.current.currentSelectedGameObject;
            if (selected != null)
            {
                var button = selected.GetComponent<Button>();
                if (button != null)
                {
                    button.onClick.Invoke(); // UI�{�^���̃N���b�N�C�x���g�����s
                }
            }
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
            if (LB != null)
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

    void OnButtonPressed(int b)
    {
        switch(b)
        {
            case 0:
                SceneChenger.instance.ChangeScene(0);
                AS.PlayOneShot(Push_Button);
                break;
            case 1:
                if(FloorChecker.Instance.Current_Floor == 1)
                {
                    SceneChenger.instance.ChangeScene(3);
                }
                else if (FloorChecker.Instance.Current_Floor == 2)
                {
                    SceneChenger.instance.ChangeScene(4);
                }
                break;
        }
    }

    //�e�L�X�g�\���֐�
    void Display_TEXT()
    {
        GO.text = "�X�R�A�� :";
    }
}
