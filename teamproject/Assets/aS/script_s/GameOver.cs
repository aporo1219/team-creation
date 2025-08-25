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

        //�R���g���[����UI�{�^���̕R�Â�
        EventSystem.current.SetSelectedGameObject(GB);
        LB = GB;

        //�{�^���̓o�^
        for (int i = 0; i < Button.Length; i++)
        {
            if (Button[i] == null && G_Button[i] == null)
            {
                Debug.LogError($"Button[{i}] ���ݒ肳��Ă��܂���I");
                Debug.LogError($"GB[{i}] ���ݒ肳��Ă��܂���I");
            }
            else
            {
                Debug.Log($"Button[{i}] �Ƀ��X�i�[�o�^����");
                Debug.Log($"GB[{i}] �Ƀ��X�i�[�o�^����");
            }
        }
        
        Display_TEXT();
    }

        
    

    // Update is called once per frame
    void Update()
    {
        // ���ݑI������Ă���I�u�W�F�N�g
        GameObject current = EventSystem.current.currentSelectedGameObject;

        //���ׂĂ�Outline�t���[�����\��
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

        // �����܂��͔z��O�Ȃ�Ō�ɑI�����Ă����{�^���ɖ߂�
        if (current == null || !IsButtonCheck(current))
        {
            EventSystem.current.SetSelectedGameObject(LB);
        }
        else
        {
            //OutLine���n��
            var OL = current.GetComponent<Outline>();
            if (OL != null)
            {
                OL.enabled = true;
            }
            // �L���ȃ{�^���Ȃ�X�V
            LB = current;
        }

        //B�{�^���������ꂽ����
        if (selectAction != null && selectAction.WasPressedThisFrame())
        {
            Debug.Log("��");
            GameObject selected = EventSystem.current.currentSelectedGameObject;
            
            if (selected != null)
            {
                for (int i = 0; i < Button.Length; i++)
                {
                    if (Button[i] != null && Button[i].gameObject == selected)
                    {
                        Debug.Log("B�{�^���őI�𒆂̃{�^��������I");
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

    //�{�^���������ꂽ��
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
                //�G���x�[�^�[�O�Ń��X�|�[��
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
                //�G���x�[�^�[�O�Ń��X�|�[��
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

    //�e�L�X�g�\���֐�
    void Display_TEXT()
    {
        GO.text = "�Q�[���I�[�o�[";
    }

    //�{�^�����z��ɓ����Ă��邩�̃`�F�b�N
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
