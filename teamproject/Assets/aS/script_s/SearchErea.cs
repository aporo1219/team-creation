using UnityEngine;

public class SearchErea : MonoBehaviour
{

    [SerializeField] GameObject SmallEnemy;

    public  bool Discovery_Main;
    public bool First_Time;
    public static SearchErea Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Discovery_Main = false;
        First_Time = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //�G�̃T�[�`�̈���G���G�ɒǏ]����悤�ɂ���
        transform.position = SmallEnemy.transform.position;
    }

    void OnTriggerStay(Collider other)
    {
        //��l�����T�[�`�̈�ɓ�������G����l���Ɍ������M���𑗂�
        if(other.gameObject.tag == "Player")
        {
            First_Time = true;
          // Debug.Log("�q�b�g");
           Discovery_Main = true;
        }
       
    }


    void OnTriggerExit(Collider other)
    {
        //��l�����T�[�`�̈�ɓ�������G����l���Ɍ������M���𑗂�
        if (other.gameObject.tag == "Player")
        {

            // Debug.Log("�q�b�g");
            Discovery_Main = false;
        }

    }
}
