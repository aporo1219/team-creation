using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public bool tutorial_start_flag = true;
    public List<bool> tutorial_clear = new List<bool>();
    [SerializeField] List<GameObject> Tutorial_Obj = new List<GameObject>();

    [SerializeField] TutorialShowTaskSystem tasksystem;

    [SerializeField] BoxCollider button;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //���߂̃^�X�N�ȊO�\�����Ȃ�
        Tutorial_Obj[0].SetActive(true);
        for (int i = 1; i < Tutorial_Obj.Count; i++)
        {
            Tutorial_Obj[i].SetActive(false);
        }
        //�`���[�g���A���^�X�N��\��
        Start_Tutorial();
    }

    //�`���[�g���A���^�X�N�̕\��
    void Start_Tutorial()
    {
        tasksystem.change_task = "�e��ǂ������悤";
        tasksystem.change_task_flag = true;
        tasksystem.assist_text = "L�X�e�B�b�N���X���Ĉړ�";
        tasksystem.text_size = 60;
    }

    //2�ڈȍ~�̃`���[�g���A���^�X�N�̕\��
    public void Tutorial_Clear(int route_num)
    {
        if (route_num < 6)
        {
            Tutorial_Obj[route_num - 1].SetActive(false);
            if (route_num < 5)
                Tutorial_Obj[route_num].SetActive(true);
        }
        switch (route_num)
        {
            case 1:
                tasksystem.change_task = "�e��ǂ������悤";
                tasksystem.change_task_flag = true;
                tasksystem.assist_text = "L�X�e�B�b�N�����������\n�_�b�V���̐؂�ւ�";
                tasksystem.text_size = 60;
                break;
            case 2:
                tasksystem.change_task = "�e��ǂ������悤";
                tasksystem.change_task_flag = true;
                tasksystem.assist_text = "R�X�e�B�b�N���X���Ď��_�ړ�";
                tasksystem.text_size = 60;
                break;
            case 3:
                tasksystem.change_task = "�e��ǂ������悤";
                tasksystem.change_task_flag = true;
                tasksystem.assist_text = "A�{�^���������ăW�����v";
                tasksystem.text_size = 60;
                break;
            case 4:
                tasksystem.change_task = "�e��ǂ������悤";
                tasksystem.change_task_flag = true;
                tasksystem.assist_text = "�󒆂�A�{�^������������\n�󒆃W�����v";
                tasksystem.text_size = 60;
                break;
            case 5:
                tasksystem.change_task = "�G��|����";
                tasksystem.change_task_flag = true;
                tasksystem.assist_text = "X�{�^���������čU��\nRT�{�^���������ĉ��";
                tasksystem.kill_enemy_num = 2;
                tasksystem.text_size = 60;
                break;
            case 6:
                tasksystem.change_task = "�󔠂��J���悤";
                tasksystem.change_task_flag = true;
                tasksystem.assist_text = "�U���𓖂Ă�ƊJ��";
                tasksystem.text_size = 60;
                break;
            case 7:
                tasksystem.change_task = "�{�^����������";
                tasksystem.change_task_flag = true;
                tasksystem.assist_text = "�U���𓖂Ă�Ƃǂ����̎d�|��������";
                button.enabled = true;
                tasksystem.text_size = 55;
                break;
            case 8:
                tasksystem.change_task = "�G���x�[�^�[�܂ōs����";
                tasksystem.change_task_flag = true;
                tasksystem.assist_text = "�����Y�ꂽ��START�{�^����������\n�|�[�Y��ʂɍs���đ�����������悤";
                tasksystem.text_size = 55;
                break;
        }
    }
}
