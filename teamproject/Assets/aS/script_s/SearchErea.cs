using UnityEngine;

public class SearchErea : MonoBehaviour
{
    //�G�̈ʒu�擾�̕ϐ�
    private Transform EnemyPos;

    public bool Find;
    public bool FirstTime;
  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //�ǐՑΏۂ̐ݒ�
        EnemyPos = transform.parent;

        FirstTime = false;
        Find = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
     
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
