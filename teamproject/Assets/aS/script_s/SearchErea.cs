using UnityEngine;

public class SearchErea : MonoBehaviour
{
    
    public  GameObject SmallEnemy;

    public  bool Discovery_Main;
    public bool Lost_Main;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
     void Start()
    {
        Discovery_Main = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //�G�̃T�[�`�̈���G���G�ɒǏ]����悤�ɂ���
        transform.position = SmallEnemy.transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        //��l�����T�[�`�̈�ɓ�������G����l���Ɍ������M���𑗂�
        if(other.gameObject.name == "Main")
        {
           
          // Debug.Log("�q�b�g");
           Discovery_Main = true;
        }
       
    }

    void OnTriggerExit(Collider other)
    {
        //��l�����T�[�`�̈�ɓ�������G����l���Ɍ������M���𑗂�
        if (other.gameObject.name == "Main")
        {

            Debug.Log("�o��");
            Lost_Main = false;
        }

    }
}
