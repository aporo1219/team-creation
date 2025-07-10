using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class StageSelectUI : MonoBehaviour
{
    private int f;
    private int Level_Floor = 0;
    private Gamepad GP;
    private Color Choice = Color.yellow;
    private Color Default = Color.white;

    [SerializeField] Text Floor_Display;
    [SerializeField] Button[] Button;
    [SerializeField] GameObject[] G_Button;
    [SerializeField] GameObject First_Button;
    [SerializeField] GameObject[] GB;
    [SerializeField] Material[] Normal_M;
    [SerializeField] Material[] Change_M;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FloorDisplay(Level_Floor);
        //�R���g���[����UI�{�^���̕R�Â�
        EventSystem.current.SetSelectedGameObject(First_Button);

        f = 0;
        //�{�^���̊Ǘ�
        for (int i = 0; i < Button.Length; i++)
        {
            if (Button[i] == null && G_Button[i] == null)
            {
                Debug.LogError($"Button[{i}] ���ݒ肳��Ă��܂���I");
                Debug.LogError($"GB[{i}] ���ݒ肳��Ă��܂���I");
            }
            else
            {
                int index = i;
                Button[i].onClick.AddListener(() => OnButtonPressed(index));
                //G_Button[i].SetActive(false);
                if (Release(index) || G_Button[i] == First_Button)
                {
                    G_Button[i].SetActive(true);
                }
                else
                {
                    G_Button[i].SetActive(false);
                }
                Debug.Log($"Button[{i}] �Ƀ��X�i�[�o�^����");
                Debug.Log($"GB[{i}] �Ƀ��X�i�[�o�^����");
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
        GameObject Selected = EventSystem.current.currentSelectedGameObject;
        //�Q�[���p�b�h�̓���
        GP = Gamepad.current;
        if (GP == null)
        {
            return;
        }
        Vector2 Right_Stick = GP.rightStick.ReadValue();

        //�I���̃}�e���A���ύX����
        for (int i = 0; i < GB.Length; i++)
        {
            Renderer R = GB[i].GetComponent<Renderer>();

            if(R == null)
            {
                continue;
            }
            if (Selected == G_Button[i])
            {
                R.material = Change_M[i];
            }
            else�@
            {
                R.material = Normal_M[i];
            }
        }
    }

    //�X�e�[�W���
    bool Release(int stage)
    {
        return PlayerPrefs.GetInt("Release_" + stage, 0) == 1;

        PlayerPrefs.SetInt("StageUnlocked_1", 1); // �X�e�[�W1�����
        PlayerPrefs.Save();
    }

    //�{�^���������ꂽ���̏���
    public void OnButtonPressed(int floor)
    {
        switch(floor)
        {
            case 3:
                Debug.Log("�ŏI�X�e�[�W��");
                Invoke(nameof(ChangeScene), 0.1f);
                f = 3;
                Level_Floor = 10;
                FloorDisplay(Level_Floor);
                break;
            case 2:
                Debug.Log("2�X�e�[�W��");
                Invoke(nameof(ChangeScene), 0.1f);
                f = 2;
                Level_Floor = 1;
                FloorDisplay(Level_Floor);
                break;
            case 1:
                Debug.Log("1�X�e�[�W��");
                Invoke(nameof(ChangeScene), 0.1f);
                f = 1;
                Level_Floor = 2;
                FloorDisplay(Level_Floor);
                break;
            case 0:
                Debug.Log("�`���[�g���A���X�e�[�W��");
                Invoke(nameof(ChangeScene), 0.1f);
                f = 0;
                Level_Floor = 0;
                FloorDisplay(Level_Floor);
                break;
        }
    }

    //�V�[���ړ�
    private void ChangeScene()
    {
       switch(f)
        {
            case 3:
                SceneManager.LoadScene("Finalstage");
                break;
            case 2:
                SceneManager.LoadScene("stage2");
                break;
            case 1:
                SceneManager.LoadScene("stage1");
                break;
            case 0:
                SceneManager.LoadScene("tutorial");
                break;
        }
    }

    //�t���A�\��
    void FloorDisplay(int F)
    {
        F = Level_Floor;
        //��K
        if(F == 10)
        {
            Floor_Display.text ="1";
        }
        //�`���[�g���A��
        else if(F == 0)
        {
            Floor_Display.text = "BT" + 2;
        }
        else
        {
             Floor_Display.text = "B" + F ;
        }
           
    }

}
