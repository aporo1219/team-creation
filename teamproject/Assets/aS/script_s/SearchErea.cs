using UnityEngine;

public class SearchErea : MonoBehaviour
{
    
    public  GameObject SmallEnemy;

    public static bool Discovery_Main;

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
           
           Debug.Log("�q�b�g");
            Discovery_Main = true;
        }
       
    }
}
