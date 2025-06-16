using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PoseManager_M : MonoBehaviour
{
    public int showing_pose;//�|�[�Y���\������Ă邩

    [SerializeField] GameObject pose_obj;//�|�[�Y�L�����o�X��ǂݍ���
    [SerializeField] GameObject game_obj;//�Q�[���L�����o�X��ǂݍ���
    InputAction poseAction;
    InputAction selectorAction;
    InputAction poseClick;

    Vector2 selectValue;

    int selector_move_time = 0;

    public int selector_pos = 0;

    bool push_selector = false;

    [SerializeField] Image ReStart;
    [SerializeField] Image Status;
    [SerializeField] Image OperationMethod;
    [SerializeField] Image FinishStage;

    [SerializeField] GameObject Frame;

    enum Selector_ID
    {
        RESTART,
        STATUS,
        OPERATIONMETHOD,
        FINISHSTAGE
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        showing_pose = 0;
        poseAction = InputSystem.actions.FindAction("Pose");
        selectorAction = InputSystem.actions.FindAction("PoseSelect");
        poseClick = InputSystem.actions.FindAction("PoseClick");
    }

    // Update is called once per frame
    void Update()
    {
        //Escape�L�[�̓��͂Ń|�[�Y��ʂ̐؂�ւ����s��
        //�R���g���[���[�̃|�[�Y�{�^���Ń|�[�Y��ʂ̐؂�ւ����s��
        if (Input.GetKeyDown(KeyCode.Escape) || poseAction.WasPressedThisFrame())
        {
            Change_Pose(showing_pose);
            showing_pose++;
        }

        if (showing_pose > 1)
            showing_pose = 0;

        if (showing_pose == 1)
        {
            Time.timeScale = 0.0f;
            selectValue = selectorAction.ReadValue<Vector2>();
            //���X�e�B�b�N���㉺�ɓ|���ꂽ��Z���N�g�̐؂�ւ����s��
            if (selectValue.y != 0 && !push_selector)
            {
                if (selector_move_time >= 15) selector_move_time = 0;
                if (selectValue.y > 0.5 && selector_move_time == 0)
                {
                    selector_pos--;
                    if(selector_pos < 0)selector_pos = 0;
                    Debug.Log("selector_moveup");
                }
                if (selectValue.y < -0.5 && selector_move_time == 0)
                {
                    selector_pos++;
                    if (selector_pos > 3) selector_pos = 3;
                    Debug.Log("selector_movedown");
                }
                selector_move_time++;
            }
            else
            {
                selector_move_time = 0;
            }

            //����{�^���������ꂽ��
            if (poseClick.WasPressedThisFrame())
            {
                push_selector = true;
                Click_Select(selector_pos);
            }
            //����{�^���������ꂽ��
            if(poseClick.WasReleasedThisFrame())
            {
                push_selector = false;
                Remove_Color(selector_pos);
            }
        }
        else
            Time.timeScale = 1.0f;

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
