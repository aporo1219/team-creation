using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorChecker : MonoBehaviour
{
    //�C���X�^���X
    public static FloorChecker Instance;
    public  int Current_Floor = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        

    }
    // Update is called once per frame
    void Update()
    {
        //Current_Floor���P����������Q�[���I�[�o�[�����蒼���Ƃ��X�e�[�W�P�ɖ߂��B�Q�Ȃ�΃X�e�[�W�Q
        if(SceneManager.GetActiveScene().name == "stage1")
        {
            Current_Floor = 1;
        }
        else if(SceneManager.GetActiveScene().name == "stage2")
        {
            Current_Floor = 2;
        }
    }
}
