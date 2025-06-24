using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet_Enemy : MonoBehaviour
{ 
    [SerializeField] GameObject Bullet_OBJ;

    Vector3 Enemy_Pos;
    Vector3 Bullet_Pos;
 
    private float Bullet_Speed;
   


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Bullet_Speed = 10.0f;
       Bullet_OBJ.SetActive(false);
    }

    // Update is called once per frame
    

    //�e�̔���
    public void Shot()
    {
        Bullet_OBJ.SetActive(true);
        Debug.Log("����");
        //�ړ�
        Bullet_OBJ.transform.Translate(Vector3.left * Time.deltaTime * Bullet_Speed);
        //�P�O�b�o�߂ŏ�����
        Invoke(nameof(Delite), 1.0f);
    }

    //�e��������
    void Delite()
    {
        GameObject new_Bullet = Instantiate(Bullet_OBJ, Enemy_Pos, transform.rotation);
        Destroy(gameObject);
        
    }

    //�v���C���[�Ƃ��������������
    void OnTriggerEnter(Collider other)
    {
       if(gameObject.tag == ("Player"))
       {
            Delite();
       }
    }
        
}
