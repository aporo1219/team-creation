using Unity.VisualScripting;
using UnityEngine;

public class SearchErea : MonoBehaviour
{
    //�G�̈ʒu�擾�̕ϐ�
    [SerializeField] GameObject EnemyPos;
    //[SerializeField] Transform SE;

    public bool Find;
    public bool FirstTime;
  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //�ǐՑΏۂ̐ݒ�
        //SE = EnemyPos;

        FirstTime = false;
        Find = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //�G�̃T�[�`�̈���G���G�ɒǏ]����悤�ɂ���
        transform.position = EnemyPos.transform.position;
    }

   
    void OnTriggerStay(Collider other)
    {
        //��l�����T�[�`�̈�ɓ�������G����l���Ɍ������M���𑗂�
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("����");
            Find = true;
            FirstTime = true;
            //Move.Discovery();
        }
       
    }


    void OnTriggerExit(Collider other)
    {
        //��l�����T�[�`�̈�ɓ�������G����l���Ɍ������M���𑗂�
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("�ł�");
            Find = false;
            //Move.Lost();
        }

    }
}
