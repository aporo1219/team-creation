using UnityEngine;

public class ChangeBGM : MonoBehaviour
{

    private int ChangeTime = 2;
    private bool BGM = false;

    [SerializeField] AudioSource ASBefore;
    [SerializeField] AudioSource ASAfter;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //���BGM���~���[�g�ɂ���
        ASAfter.mute = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag == "Player")
        {
            //�v���C���[���������痬��Ă���BGM���~���[�g�ɂ��A�ʂ̂a�f�l�𗬂�
            Debug.Log("������");
            ASBefore.mute = true;
            BGM = true;
            //2�b���BGM�؂�ւ�   
            Invoke(nameof(Change), ChangeTime);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        //if (other.gameObject.tag == "Player")
        //{
        //    //�v���C���[���o���痬��Ă���BGM���~���[�g�ɂ��A�ʂ̂a�f�l�𗬂�
        //    Debug.Log("�o��");
        //    ASAfter.mute = true;
        //    BGM = false;
        //    //2�b���BGM�؂�ւ�   
        //    Invoke(nameof(Change), ChangeTime);
        //}
    }

    private void Change()
    {
        //after��BGM��true�ɂ���
        if (BGM)
        {
            ASAfter.mute = false;
            ASAfter.Play();
        }
        else
        {
            ASBefore.mute = false;
            ASBefore.Play();
        }
    }
}
