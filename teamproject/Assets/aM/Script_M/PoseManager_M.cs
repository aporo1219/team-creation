using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PoseManager_M : MonoBehaviour
{
    //public int x = 0;
    //public int y = 0;

    [SerializeField] ShowTaskSystem tasksystem;

    public int showing_pose;//�|�[�Y���\������Ă邩

    [SerializeField] GameObject pose_obj;       //�|�[�Y�L�����o�X��ǂݍ���
    [SerializeField] GameObject game_obj;       //�Q�[���L�����o�X��ǂݍ���
    InputAction poseAction;                     //�|�[�Y��ʂɐ؂�ւ��邽�߂̃A�N�V����
    InputAction selectorAction;                 //�|�[�Y��ʓ��̃Z���N�^�[�̓���
    InputAction poseClick;                      //�|�[�Y��ʂŌ���{�^����������
    InputAction poseCancel;

    Vector2 selectValue;                        //�Z���N�^�[�̏㉺���E�̔���

    int selector_move_time = 0;                 //�Z���N�^�[�̘A���œ����܂ł̎���

    public int selector_pos = 0;                //�Z���N�^�[�̈ʒu

    bool push_selector = false;                 //����{�^���������ꂽ��

    [SerializeField] Image BackGround;          //�|�[�Y��ʂ̔w�i
    int alpha_time = 0;                         //�����x

    bool Click_UI = false;                      //�X�e�[�^�X�����������UI���N���b�N���ꂽ��
    int comment_move_time = 0;                  //�����p�l������������
    bool comment_move = false;                  //�����p�l���������Ă��邩
    int comment_num = 0;                        //�ǂ̐����p�l�����J����Ă��邩
    bool push_cancel = false;                   //�L�����Z���{�^���������ꂽ��

    [SerializeField] RawImage Map;              //�}�b�v�摜
    [SerializeField] Image ReStart;             //�ĊJ�摜
    [SerializeField] Image Status;              //�X�e�[�^�X�摜
    [SerializeField] Image OperationMethod;     //����m�F�摜
    [SerializeField] Image FinishStage;         //�X�e�[�W�I���摜
    [SerializeField] Image Passive;             //�p�b�V�u�X�L���摜
    [SerializeField] Image Active;              //�A�N�e�B�u�X�L���摜
    [SerializeField] Image Comment_Panel;       //�����p�l���摜

    [SerializeField] GameObject Frame;          //�Z���N�^�[�̃t���[���摜

    [SerializeField] Text Comment_Text;         //�����p�l���̕���

    Vector3[] UI_pos;                           //UI�̈ʒu
    int pose_start = 0;                         //�|�[�Y��ʂ̃t�F�[�h�C�����t�F�[�h�A�E�g�̎���

    enum Selector_ID
    {
        RESTART,
        STATUS,
        OPERATIONMETHOD,
        FINISHSTAGE
    }

    private void Awake()
    {
        Application.targetFrameRate = 120;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        showing_pose = 0;
        poseAction = InputSystem.actions.FindAction("Pose");
        selectorAction = InputSystem.actions.FindAction("PoseSelect");
        poseClick = InputSystem.actions.FindAction("PoseClick");
        poseCancel = InputSystem.actions.FindAction("PoseCancel");

        UI_pos = new Vector3[8];

        UI_pos[0] = new Vector3(-800, 250, 0);
        UI_pos[1] = new Vector3(-370, 250, 0);
        UI_pos[2] = new Vector3(-800, 90, 0);
        UI_pos[3] = new Vector3(-370, 90, 0);
        UI_pos[4] = new Vector3(-600, 700, 0);
        UI_pos[5] = new Vector3(2450, 135, 0);
        UI_pos[6] = new Vector3(2450, 370, 0);
        UI_pos[7] = new Vector3(1400, 1350, 0);
    }
    //1350
    // Update is called once per frame
    void Update()
    {
        //UI�̈ړ�����
        ReStart.rectTransform.position = UI_pos[0];
        Status.rectTransform.position = UI_pos[1];
        OperationMethod.rectTransform.position = UI_pos[2];
        FinishStage.rectTransform.position = UI_pos[3];
        Map.rectTransform.position = UI_pos[4];
        Passive.rectTransform.position = UI_pos[5];
        Active.rectTransform.position = UI_pos[6];
        Comment_Panel.rectTransform.position = UI_pos[7];

        //Escape�L�[�̓��͂Ń|�[�Y��ʂ̐؂�ւ����s��
        //�R���g���[���[�̃|�[�Y�{�^���Ń|�[�Y��ʂ̐؂�ւ����s��
        if ((Input.GetKeyDown(KeyCode.Escape) || poseAction.WasPressedThisFrame()) && !Click_UI && SceneChenger.gameState == "playing")
        {
            Comment_Text.text = tasksystem.task;
            showing_pose++;
        }

        if (pose_start == 0)
            Change_Pose(showing_pose);

        //���l���P���傫���Ȃ�����O�ɖ߂�
        if (showing_pose > 1)
            showing_pose = 0;

        //�|�[�Y��ʂɈړ������琔�l��200�ɂȂ�܂Ŕw�i�̓����x��ύX����
        if(showing_pose == 1 && alpha_time < 200)
        {
            alpha_time += 8;
            BackGround.color = new Color32(0, 0, 0, (byte)alpha_time);
        }
        //�|�[�Y��ʂ��痣�ꂽ��w�i�̓����x���O�ɖ߂�
        else if(showing_pose == 0 && alpha_time > 0)
        {
            alpha_time -= 8;
            BackGround.color = new Color32(0, 0, 0, (byte)alpha_time);
        }

        //�|�[�Y��ʒ��̍s��
        if (showing_pose == 1)
        {
            //�|�[�Y��ʒ��̓Q�[�����̓������~�߂�
            Time.timeScale = 0.0f;
            //30�t���[���̊Ԃ�UI��\������
            if (pose_start < 30)
            {
                pose_start++;
                int i;
                for (i = 0; i < 5; i++)
                {
                    UI_pos[i].x += 35;
                }
                for (; i < 7; i++)
                {
                    UI_pos[i].x -= 35;
                }
                UI_pos[i].y -= 19;
            }
            else
            {
                //���X�e�B�b�N�̗͂̓���������Ă���
                selectValue = selectorAction.ReadValue<Vector2>();
                //���X�e�B�b�N���|���ꂽ��
                if ((selectValue.x != 0 || selectValue.y != 0) && !push_selector && !Click_UI)
                {
                    //�Z���N�^�[�̘A���̓�����15�b�ɂP�x�ɂ���
                    if (selector_move_time >= 15) selector_move_time = 0;
                    //���X�e�B�b�N�����ɓ|���ꂽ��J�[�\�������ɓ�����
                    if (selectValue.x < -0.5 && selector_move_time == 0 && selector_pos != 2)
                    {
                        selector_pos--;
                        if (selector_pos < 0) selector_pos = 0;
                        Debug.Log("selector_moveleft");
                    }
                    //���X�e�B�b�N���E�ɓ|���ꂽ��J�[�\�����E�ɓ�����
                    if (selectValue.x > 0.5 && selector_move_time == 0 && selector_pos != 1)
                    {
                        selector_pos++;
                        if (selector_pos > 3) selector_pos = 3;
                        Debug.Log("selector_moveright");
                    }
                    //���X�e�B�b�N����ɓ|���ꂽ��J�[�\������ɓ�����
                    if (selectValue.y > 0.5 && selector_move_time == 0)
                    {
                        selector_pos -= 2;
                        if (selector_pos < 0) selector_pos += 2;
                        Debug.Log("selector_moveup");
                    }
                    //���X�e�B�b�N�����ɓ|���ꂽ��J�[�\�������ɓ�����
                    if (selectValue.y < -0.5 && selector_move_time == 0)
                    {
                        selector_pos += 2;
                        if (selector_pos > 3) selector_pos -= 2;
                        Debug.Log("selector_movedown");
                    }
                    //���X�e�B�b�N���㉺�ɌX����ꂽ��selector_move_time���P�����₷
                    if (selectValue.y > 0.5 || selectValue.y < -0.5)
                        selector_move_time++;
                }
                else
                {
                    //���X�e�B�b�N���X�����ĂȂ�������selector_move_time���O�ɂ���
                    selector_move_time = 0;
                }

                //����{�^���������ꂽ��
                if (poseClick.WasPressedThisFrame() && !push_selector && !Click_UI)
                {
                    push_selector = true;
                    Click_Select(selector_pos);
                }
                //����{�^���������ꂽ��
                if (poseClick.WasReleasedThisFrame() && push_selector && !Click_UI)
                {
                    push_selector = false;
                    Remove_Color(selector_pos);
                }

                //�L�����Z���{�^���������ꂽ��
                if(poseCancel.WasPressedThisFrame() && !push_selector && comment_num == selector_pos)
                {
                    push_cancel = true;
                }
                //�L�����Z���{�^���������ꂽ��
                if(poseCancel.WasReleasedThisFrame() && push_selector && !push_cancel)
                {
                    push_selector = false;
                }
                //UI���N���b�N���ꂽ��
                if (Click_UI && selector_pos != comment_num)
                {
                    if (comment_move_time < 30 && !comment_move)
                    {
                        comment_move_time++;
                        UI_pos[7].y += 19;
                    }
                    if (comment_move_time > 0 && comment_move)
                    {
                        comment_move_time--;
                        UI_pos[7].y -= 19;
                    }
                    if (comment_move_time == 30)
                    {
                        comment_move = true;
                        Change_Text(selector_pos);
                    }
                    if (comment_move_time == 0)
                    {
                        comment_move = false;
                        push_selector = false;
                        comment_num = selector_pos;
                    }
                }
                //UI���L�����Z�����ꂽ��
                if(push_cancel && (selector_pos == 1 || selector_pos == 2))
                {
                    push_selector = true;
                    if (comment_move_time < 30 && !comment_move)
                    {
                        comment_move_time++;
                        UI_pos[7].y += 19;
                    }
                    if (comment_move_time > 0 && comment_move)
                    {
                        comment_move_time--;
                        UI_pos[7].y -= 19;
                    }
                    if (comment_move_time == 30)
                    {
                        comment_move = true;
                        Change_Text(0);
                    }
                    if (comment_move_time == 0)
                    {
                        comment_move = false;
                        push_selector = false;
                        comment_num = 0;
                        push_cancel = false;
                        comment_num = 0;
                        Click_UI = false;
                        Remove_Color(selector_pos);
                    }
                }
                else
                {
                    push_cancel = false;
                }
            }
        }
        //�|�[�Y��ʂ𗣂ꂽ��Q�[�����𓮂���
        else
        {
            //30�t���[���̊Ԃ�UI���\���ɂ���
            if (pose_start > 0)
            {
                pose_start--;
                int i;
                for (i = 0; i < 5; i++)
                {
                    UI_pos[i].x -= 35;
                }
                for (; i < 7; i++)
                {
                    UI_pos[i].x += 35;
                }
                UI_pos[i].y += 19;
            }
            //UI����\���ɂȂ�����Q�[�����𓮂���
            if(pose_start == 0)
            {
                Time.timeScale = 1.0f;
                selector_pos = 0;
            }
        }

        //�Z���N�^�[�̈ʒu��\������
        Show_Selector(selector_pos);
    }

    //�|�[�Y�̕\����\�����Ǘ�����֐�
    void Change_Pose(int showing)
    {
        if (showing == 1)
        {
            ReStart.color = new Color32(255, 255, 255, 255);
            pose_obj.SetActive(true);
            game_obj.SetActive(false);
        }
        else
        {
            pose_obj.SetActive(false);
            game_obj.SetActive(true);
        }
    }

    //�Z���N�^�[�t���[���̈ʒu��ύX����֐�
    void Show_Selector(int spos)
    {
        switch(spos)
        {
            case (int)Selector_ID.RESTART:
                Frame.transform.position = ReStart.transform.position;
                break;
            case (int)Selector_ID.STATUS:
                Frame.transform.position = Status.transform.position;
                break;
            case (int)Selector_ID.OPERATIONMETHOD:
                Frame.transform.position = OperationMethod.transform.position;
                break;
            case (int)Selector_ID.FINISHSTAGE:
                Frame.transform.position = FinishStage.transform.position;
                break;
        }
    }

    //�I�����ꂽ�ꏊ�̐F���Â�����
    void Click_Select(int spos)
    {
        switch (spos)
        {
            case (int)Selector_ID.RESTART:
                ReStart.color = new Color32(100, 100, 100,255);
                break;
            case (int)Selector_ID.STATUS:
                Status.color = new Color32(100, 100, 100, 255);
                Click_UI = true;
                break;
            case (int)Selector_ID.OPERATIONMETHOD:
                OperationMethod.color = new Color32(100, 100, 100, 255);
                Click_UI = true;
                break;
            case (int)Selector_ID.FINISHSTAGE:
                FinishStage.color = new Color32(100, 100, 100, 255);
                break;
        }
    }

    //���Ԃ��߂�����F��߂�
    void Remove_Color(int spos)
    {
        switch (spos)
        {
            case (int)Selector_ID.RESTART:
                ReStart.color = new Color32(255, 255, 255, 255);
                Change_Pose(showing_pose);
                showing_pose++;
                break;
            case (int)Selector_ID.STATUS:
                Status.color = new Color32(255, 255, 255, 255);
                break;
            case (int)Selector_ID.OPERATIONMETHOD:
                OperationMethod.color = new Color32(255, 255, 255, 255);
                break;
            case (int)Selector_ID.FINISHSTAGE:
                FinishStage.color = new Color32(255, 255, 255, 255);
                break;
        }
    }

    //�����p�l���̕�����ς���
    void Change_Text(int spos)
    {
        switch(spos)
        {
            case 0:
                Comment_Text.fontSize = 60;
                Comment_Text.text = tasksystem.task;
                break;
            case 1:
                Comment_Text.fontSize = 60;
                Comment_Text.text = "�e�X�g�Q";
                break;
            case 2:
                Comment_Text.fontSize = 40;
                Comment_Text.text = "\n\n�ړ��F���X�e�B�b�N�b�U���FX�{�^��\n\n" +
                                    "���_�F�E�X�e�B�b�N�b�W�����v�FA�{�^��\n\n" +
                                    "����FRT�{�^���b�_�b�V���F���X�e�B�b�N��������\n\n";
                break;
        }
    }
}
