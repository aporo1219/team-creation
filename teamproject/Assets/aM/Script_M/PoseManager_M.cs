using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PoseManager_M : MonoBehaviour
{
    public int x = 0;
    public int y = 0;

    public int showing_pose;//�|�[�Y���\������Ă邩

    [SerializeField] GameObject pose_obj;       //�|�[�Y�L�����o�X��ǂݍ���
    [SerializeField] GameObject game_obj;       //�Q�[���L�����o�X��ǂݍ���
    InputAction poseAction;                     //�|�[�Y��ʂɐ؂�ւ��邽�߂̃A�N�V����
    InputAction selectorAction;                 //�|�[�Y��ʓ��̃Z���N�^�[�̓���
    InputAction poseClick;                      //�|�[�Y��ʂŌ���{�^����������

    Vector2 selectValue;                        //�Z���N�^�[�̏㉺���E�̔���

    int selector_move_time = 0;                 //�Z���N�^�[�̘A���œ����܂ł̎���

    public int selector_pos = 0;                //�Z���N�^�[�̈ʒu

    bool push_selector = false;                 //����{�^���������ꂽ��

    [SerializeField] Image BackGround;          //�|�[�Y��ʂ̔w�i
    int alpha_time = 0;

    [SerializeField] RawImage Map;              //�}�b�v�摜
    [SerializeField] Image ReStart;             //�ĊJ�摜
    [SerializeField] Image Status;              //�X�e�[�^�X�摜
    [SerializeField] Image OperationMethod;     //����m�F�摜
    [SerializeField] Image FinishStage;         //�X�e�[�W�I���摜
    [SerializeField] Image Passive;             //�p�b�V�u�X�L���摜
    [SerializeField] Image Active;              //�A�N�e�B�u�X�L���摜

    [SerializeField] GameObject Frame;          //�Z���N�^�[�̃t���[���摜

    Vector3[] UI_pos;   //UI�̈ʒu
    int pose_start = 0;

    enum Selector_ID
    {
        RESTART,
        STATUS,
        OPERATIONMETHOD,
        FINISHSTAGE
    }

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        showing_pose = 0;
        poseAction = InputSystem.actions.FindAction("Pose");
        selectorAction = InputSystem.actions.FindAction("PoseSelect");
        poseClick = InputSystem.actions.FindAction("PoseClick");

        UI_pos = new Vector3[7];

        UI_pos[0] = new Vector3(-800, 250, 0);
        UI_pos[1] = new Vector3(-370, 250, 0);
        UI_pos[2] = new Vector3(-800, 90, 0);
        UI_pos[3] = new Vector3(-370, 90, 0);
        UI_pos[4] = new Vector3(-600, 700, 0);
        UI_pos[5] = new Vector3(2450, 135, 0);
        UI_pos[6] = new Vector3(2450, 370, 0);
    }

    // Update is called once per frame
    void Update()
    {
        ReStart.rectTransform.position = new Vector3(UI_pos[0].x, UI_pos[0].y, 0);
        Status.rectTransform.position = new Vector3(UI_pos[1].x, UI_pos[1].y, 0);
        OperationMethod.rectTransform.position = new Vector3(UI_pos[2].x, UI_pos[2].y, 0);
        FinishStage.rectTransform.position = new Vector3(UI_pos[3].x, UI_pos[3].y, 0);
        Map.rectTransform.position = new Vector3(UI_pos[4].x, UI_pos[4].y, 0);
        Passive.rectTransform.position = new Vector3(UI_pos[5].x, UI_pos[5].y, 0);
        Active.rectTransform.position = new Vector3(UI_pos[6].x, UI_pos[6].y, 0);

        //Escape�L�[�̓��͂Ń|�[�Y��ʂ̐؂�ւ����s��
        //�R���g���[���[�̃|�[�Y�{�^���Ń|�[�Y��ʂ̐؂�ւ����s��
        if (Input.GetKeyDown(KeyCode.Escape) || poseAction.WasPressedThisFrame())
        {
            Change_Pose(showing_pose);
            showing_pose++;
        }

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
        else if(showing_pose == 0)
        {
            alpha_time = 0;
            BackGround.color = new Color32(0, 0, 0, 0);
        }

        //�|�[�Y��ʒ��̍s��
        if (showing_pose == 1)
        {
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
            }
            else
            {
                //�|�[�Y��ʒ��̓Q�[�����̓������~�߂�
                Time.timeScale = 0.0f;
                //���X�e�B�b�N�̗͂̓���������Ă���
                selectValue = selectorAction.ReadValue<Vector2>();
                //���X�e�B�b�N���|���ꂽ��
                if ((selectValue.x != 0 || selectValue.y != 0) && !push_selector)
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
                if (poseClick.WasPressedThisFrame() && !push_selector)
                {
                    push_selector = true;
                    Click_Select(selector_pos);
                }
                //����{�^���������ꂽ��
                if (poseClick.WasReleasedThisFrame() && push_selector)
                {
                    push_selector = false;
                    Remove_Color(selector_pos);
                }
            }
        }
        //�|�[�Y��ʂ𗣂ꂽ��Q�[�����𓮂���
        else
        {
            Time.timeScale = 1.0f;

            UI_pos[0] = new Vector3(-800, 250, 0);
            UI_pos[1] = new Vector3(-370, 250, 0);
            UI_pos[2] = new Vector3(-800, 90, 0);
            UI_pos[3] = new Vector3(-370, 90, 0);
            UI_pos[4] = new Vector3(-600, 700, 0);
            UI_pos[5] = new Vector3(2450, 135, 0);
            UI_pos[6] = new Vector3(2450, 370, 0);
            pose_start = 0;
            selector_pos = 0;
        }

        //�Z���N�^�[�̈ʒu��\������
        Show_Selector(selector_pos);
    }

    //�|�[�Y�̕\����\�����Ǘ�����֐�
    void Change_Pose(int showing)
    {
        if (showing == 0)
        {
            ReStart.color = new Color32(255, 255, 255, 255);
            pose_obj.SetActive(true);
            game_obj.SetActive(false);
            Debug.Log("1");
        }
        else
        {
            pose_obj.SetActive(false);
            game_obj.SetActive(true);
            Debug.Log("2");
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
                break;
            case (int)Selector_ID.OPERATIONMETHOD:
                OperationMethod.color = new Color32(100, 100, 100, 255);
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
}
