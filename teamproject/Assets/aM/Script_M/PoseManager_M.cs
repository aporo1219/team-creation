using UnityEngine;

public class PoseManager_M : MonoBehaviour
{
    int showing_pose;//�|�[�Y���\������Ă邩

    public GameObject pose_obj;//�|�[�Y�L�����o�X��ǂݍ���

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        showing_pose = 0;
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
        if (showing_pose > 1)
            showing_pose = 0;
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
}
