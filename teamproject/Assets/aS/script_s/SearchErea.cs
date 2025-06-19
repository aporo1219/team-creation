using UnityEngine;

public class SearchErea : MonoBehaviour
{
    //�G�̈ʒu�擾�̕ϐ�
    private Transform EnemyPos;
    private Move_Enemy Move;//�X�N���v�g�擾

    public bool Find;
    public bool FirstTime;
  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //�ǐՑΏۂ̐ݒ�
        EnemyPos = transform.parent;

        FirstTime = false;
        Find = false;

        Move = transform.parent.GetComponent<Move_Enemy>();  
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //�G�̃T�[�`�̈���G���G�ɒǏ]����悤�ɂ���
        transform.position = EnemyPos.position;
    }

    void OnTriggerStay(Collider other)
    {
        //��l�����T�[�`�̈�ɓ�������G����l���Ɍ������M���𑗂�
        if(other.gameObject.tag == "Player")
        {
            Find = true;
            FirstTime = true;
            Move.Discovery();
        }
       
    }


    void OnTriggerExit(Collider other)
    {
        //��l�����T�[�`�̈�ɓ�������G����l���Ɍ������M���𑗂�
        if (other.gameObject.tag == "Player")
        { 
            Find = false;
            //Move.Lost();
        }

    }
}
