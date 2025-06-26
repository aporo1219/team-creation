using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Status : MonoBehaviour
{
    [SerializeField] GameObject Player;//�v���C���[�̃^�O��������ϐ�
    [SerializeField] PlayerController_y Player_Script;//�v���C���[�̃X�N���v�g�擾�ϐ�

    private string Player_Combo;//�v���C���[�̃R���{�󋵂��m�F����X�N���v�g
    private int Count;
    private Enemy_Manager Enemy_manager;
    private Animator Anim;//�A�j���[�^�[�擾�̕ϐ�
    private int CoolTime;//�U���̃N�[���^�C��
    private int time = 0;

    //public static Enemy_Status Instance;
    public int Enemy_HP;
    public int Enemy_Power;
    //�G��ID
    public int Enemy_ID { get; private set; }

    private float nextShotTime = 0f; // ���Ɍ��Ă鎞��
    [SerializeField] float shotInterval = 2f; // ���ˊԊu�i�b�j
    Vector3 Distance;//�v���C���[�̋����̌v�Z�x�N�g��


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Anim = GetComponent<Animator>();
        Count = 5;
        Enemy_HP = 1;//�����l
        Enemy_Power = 5;//�����l
        Enemy_ID = Enemy_Manager.Entry_Enemy_ID(this);//ID�̓o�^
        CoolTime = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (Enemy_HP < 0)
        {
           Death_Before();
        }

    }

    //�G���G�̍U�����󂯂������̊֐�
    public void Be_Attack(string Combo)
    {
        //�R���{��i�K��
        if (Combo == "GroundFirst" || Combo == "AirFirst")
        {
            Debug.Log($"�GID {Enemy_ID} �� {Enemy_HP} HP");
            Enemy_HP -= 1;
        }
        //�R���{��i��
        if (Combo == "GroundSecond" || Combo == "AirSecond")
        {
            Debug.Log($"�GID {Enemy_ID} �� {Enemy_HP} HP");
            Enemy_HP -= 2;
        }
        //�R���{�O�i��
        if (Combo == "GroundThird" || Combo == "AirThird")
        {
            Debug.Log($"�GID {Enemy_ID} �� {Enemy_HP} HP");
            Enemy_HP -= 3;
        }
        //�R���{�l�i��
        if (Combo == "GroundFinish" || Combo == "AirFinish")
        {
            Debug.Log($"�GID {Enemy_ID} �� {Enemy_HP} HP");
            Enemy_HP -= 4;
        }


    }

    //�G���G�����񂾌�̏���
    void Death_Before()
    {
        //�A�j���[�V�����؂�ւ�
        Anim.SetBool("Die", true);
        Enemy_Manager.Delite_ListEnemy(Enemy_ID);
        Debug.Log("���S");
        //1�b���Destroy
        Invoke(nameof(Die), 1.0f);
        //Enemy_Manager.Destroy_Enemy_ID(1);
    }

    //���S���̊֐�
    void Die()
    {
        Destroy(gameObject);
    }

    //�U��
    private void OnCollisionStay(Collision collision)
    {
          
        if (collision.gameObject.tag == ("Player"))
        {
            if (Time.time >= nextShotTime)
            {
                nextShotTime = Time.time + shotInterval;

                // �U���A�j���[�V�����Đ�
                Anim.SetBool("Attack", true);

                // �e�̔���
                Bullet_Enemy EB = GetComponent<Bullet_Enemy>();
                if (EB != null)
                {
                    
                    EB.Shot();
                }

                //Debug.Log("�G���e�������܂���");
            }


        }
    }
}