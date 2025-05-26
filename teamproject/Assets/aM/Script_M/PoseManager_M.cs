using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PoseManager_M : MonoBehaviour
{
    int showing_pose;//�|�[�Y���\������Ă邩

    public GameObject pose_obj;//�|�[�Y�L�����o�X��ǂݍ���
    InputAction poseAction;
    InputAction selectorAction;

    Vector2 selectValue;

    int selector_move_time = 0;

    int selector_pos = 0;

    [SerializeField] Image ReStart;
    [SerializeField] Image Status;
    [SerializeField] Image OperationMethod;
    [SerializeField] Image FinishStage;

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
    }

    // Update is called once per frame
    void Update()
    {
        //Escape�L�[�̓��͂Ń|�[�Y��ʂ̐؂�ւ����s��
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Change_Pose(showing_pose);
            showing_pose++;
        }

        //�R���g���[���[�̃|�[�Y�{�^���Ń|�[�Y��ʂ̐؂�ւ����s��
        if(poseAction.WasPressedThisFrame())
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
            if (selectValue.y != 0)
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
            if(selectValue.x > 0.5)
            {
                selector_pos = 0;
            }
            if(selectValue.x < -0.5)
            {
                selector_pos = 3;
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
            pose_obj.SetActive(true);
            Debug.Log("1");
        }
        else
        {
            pose_obj.SetActive(false);
            Debug.Log("2");
        }
    }

    //
    void Show_Selector(int spos)
    {
        switch(spos)
        {
            case (int)Selector_ID.RESTART:
                ReStart.color = new Color32(100, 100, 100, 255);
                Status.color = new Color32(255,255,255,255);
                OperationMethod.color = new Color32(255, 255, 255, 255);
                FinishStage.color = new Color32(255, 255, 255, 255);
                break;
            case (int)Selector_ID.STATUS:
                ReStart.color = new Color32(255, 255, 255, 255);
                Status.color = new Color32(100, 100, 100, 255);
                OperationMethod.color = new Color32(255, 255, 255, 255);
                FinishStage.color = new Color32(255, 255, 255, 255);
                break;
            case (int)Selector_ID.OPERATIONMETHOD:
                ReStart.color = new Color32(255, 255, 255, 255);
                Status.color = new Color32(255, 255, 255, 255);
                OperationMethod.color = new Color32(100, 100, 100, 255);
                FinishStage.color = new Color32(255, 255, 255, 255);
                break;
            case (int)Selector_ID.FINISHSTAGE:
                ReStart.color = new Color32(255, 255, 255, 255);
                Status.color = new Color32(255, 255, 255, 255);
                OperationMethod.color = new Color32(255, 255, 255, 255);
                FinishStage.color = new Color32(100, 100, 100, 255);
                break;
        }
    }
}
