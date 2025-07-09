using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class StageSelectUI : MonoBehaviour
{
    private int f;
    private int Level_Floor = 1;
    private string Arrow = "��";
    private Gamepad GP;
    //[SerializeField] InputSystem Select_Push;

    [SerializeField] Text Floor_Display;
    [SerializeField] Button[] Button;
    [SerializeField] GameObject[] G_Button;
    [SerializeField] GameObject First_Button;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FloorDisplay();
        //�R���g���[����UI�{�^���̕R�Â�
        EventSystem.current.SetSelectedGameObject(First_Button);

        f = 0;
        //�{�^���̊Ǘ�
        for (int i = 0; i < Button.Length; i++)
        {
            if (Button[i] == null && G_Button[i] == null)
            {
                Debug.LogError($"Button[{i}] ���ݒ肳��Ă��܂���I");
            }
            else
            {
                int index = i;
                Button[i].onClick.AddListener(() => OnButtonPressed(index));
                //G_Button[i].SetActive(false);
                if (G_Button[i] == First_Button)
                {
                    G_Button[i].SetActive(true);
                }
                Debug.Log($"Button[{i}] �Ƀ��X�i�[�o�^����");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }
    }

    private void FixedUpdate()
    {
        //�Q�[���p�b�h�̓���
        GP = Gamepad.current;
        if(GP == null )
        {
            return;
        }
        Vector2 Right_Stick = GP.rightStick.ReadValue();

        int CF = 1;
        ReleaseFloor(CF);
        G_Button[3].SetActive(true);
    }

    //�t���A����������玟�̃t���A�����
    public void ReleaseFloor (int CF)
    {
        //G_Button[3].SetActive(true);
    }

    public void FloorSet(int floor)
    {
        Level_Floor = floor;
        FloorDisplay();
    }

    void FloorDisplay()
    {
        Floor_Display.text =  Arrow + "B" + Level_Floor ; 
    }

    //�{�^���������ꂽ���̏���
    public void OnButtonPressed(int floor)
    {
        switch(floor)
        {
            case 3:
                Debug.Log("�ŏI�X�e�[�W��");
                Invoke(nameof(ChangeScene), 5.0f);
                f = 3;
                break;
            case 2:
                Debug.Log("2�X�e�[�W��");
                Invoke(nameof(ChangeScene), 5.0f);
                f = 2;
                break;
            case 1:
                Debug.Log("1�X�e�[�W��");
                Invoke(nameof(ChangeScene), 5.0f);
                f = 1;
                break;
            case 0:
                Debug.Log("�`���[�g���A���X�e�[�W��");
                Invoke(nameof(ChangeScene), 5.0f);
                f = 0;
                break;
        }
    }

    private void ChangeScene()
    {
       switch(f)
        {
            case 3:
                SceneManager.LoadScene("Finalstage");
                break;
            case 2:
                SceneManager.LoadScene("2stage");
                break;
            case 1:
                SceneManager.LoadScene("3stage");
                break;
            case 0:
                SceneManager.LoadScene("stage");
                break;
        }
    }
}
