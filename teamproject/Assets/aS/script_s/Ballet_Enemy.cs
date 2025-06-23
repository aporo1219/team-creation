using Unity.Cinemachine;
using UnityEngine;

public class Ballet_Enemy : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private Transform  Pos;
    [SerializeField] GameObject Bullet_OBJ;

    private float Bullet_Speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Bullet_OBJ.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //�e�̔���
    public void Shot()
    {
        //�e�̃Q�[���I�u�W�F�N�g�ɑ��
        if(!Bullet_OBJ.activeSelf)
        {
            //�I�u�W�F�N�g�̏o��
            Bullet_OBJ.SetActive(true);
            var Bullet = Instantiate(Bullet_OBJ, Pos.position, Pos.rotation);
            //�e��Rigidbody���擾
            Rigidbody rd = Bullet.GetComponent<Rigidbody>();
            Bullet.SetActive(true);


            if(rd != null)
            {
                 rd.angularVelocity = Pos.forward * Bullet_Speed;
            }
        }

        else
        {
            Debug.Log("�ݒ�Ȃ�");
        }
    }
}
