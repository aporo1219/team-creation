using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Status : MonoBehaviour
{
    [SerializeField] GameObject Die_Enemy;

    private string Player_Combo;//�v���C���[�̃R���{�󋵂��m�F����X�N���v�g
    private int Count;
    private Enemy_Manager Enemy_manager;
    private Animator Anim;//�A�j���[�^�[�擾�̕ϐ�
    private int CoolTime;//�U���̃N�[���^�C��
    private int time = 0;

    [SerializeField] AttackErea Attack_Erea;
    //public static Enemy_Status Instance;
    public int Enemy_HP;
    //�G��ID
    public int Enemy_ID { get; private set; }

    private float nextShotTime = 0f; // ���Ɍ��Ă鎞��
    [SerializeField] float shotInterval = 2f; // ���ˊԊu�i�b�j
    Vector3 Distance;//�v���C���[�̋����̌v�Z�x�N�g��

    //SE
    [SerializeField] private AudioSource AS;
    [SerializeField] private AudioClip Attack_SE;
    private float Attack_v = 0.3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Anim = GetComponent<Animator>();
        Count = 5;
        Enemy_HP = 10;//�����l
        Enemy_ID = Enemy_Manager.Entry_Enemy_ID(this);//ID�̓o�^
        CoolTime = 5;
        Die_Enemy.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Enemy_HP < 0)
        {
            Die_Enemy.SetActive(true);
            Death_Before();
        }

        if (Time.time >= nextShotTime && Attack_Erea.Find)
        {
            nextShotTime = Time.time + shotInterval;

            // �U���A�j���[�V�����Đ�
            if (gameObject.tag == "Enemy")
            {
                Anim.SetBool("Attack", true);
            }
            if (gameObject.tag == "WheellEnemy")
            {
                Anim.SetBool("Attack_1", true);
            }

            if (gameObject.tag == "FlyEnemy")
            {
                Anim.SetBool("Attack_2", true);
            }

            // �e�̔���
            Bullet_Enemy EB = GetComponent<Bullet_Enemy>();
            if (EB != null)
            {
                Debug.Log("����");
                EB.Shot();
                //�e���ˎ���SE�𗬂�
                AS.PlayOneShot(Attack_SE);
                AS.volume = Attack_v;
            }

            //Debug.Log("�G���e�������܂���");
        }

    }

    //�G���G�̍U�����󂯂������̊֐�
    public void Be_Attack(int Combo)
    {
        //�R���{
        if (Combo ==  1)
        {
            Debug.Log($"�GID {Enemy_ID} �� {Enemy_HP} HP");
            Enemy_HP -= 3;
        }
        //�R���{�ŏI
        if (Combo == 2)
        {
            Debug.Log($"�GID {Enemy_ID} �� {Enemy_HP} HP");
            Enemy_HP -= 7;
        }


    }

�@�@//�G���G���X�L�����󂯂������̊֐�
    public void Be_Skill(string skill)
    {
        //�t�@�C���[�{�[��
        if(skill == "FireBall")
        {
            Enemy_HP -= 8;
        }
        //�T���_�[
        if (skill == "Thunder")
        {
            Enemy_HP -= 8;
        }
    }
    //�G���G�����񂾌�̏���
    void Death_Before()
    {
        //�A�j���[�V�����؂�ւ�
        if(gameObject.tag == "Enemy")
        { 
            Anim.SetBool("Walk", false);
            Anim.SetBool("Attack",false);
            Anim.CrossFade("Die", 0.0f);
            Enemy_Manager.Delite_ListEnemy(Enemy_ID);
            Debug.Log("���S");
            //�A�j���[�V�����Đ����Destroy
            Invoke(nameof(Die),0.30f);
        }
        if (gameObject.tag == "WheellEnemy")
        {
            Anim.SetBool("Walk", false);
            Anim.SetBool("Attack_1", false);
            Anim.CrossFade("Die_1", 0.0f);
            Enemy_Manager.Delite_ListEnemy(Enemy_ID);
            Debug.Log("���S");
            //�A�j���[�V�����Đ����Destroy
            Invoke(nameof(Die),0.30f);
        }
        if (gameObject.tag == "FlyEnemy")
        {
            Anim.SetBool("Walk", false);
            Anim.SetBool("Attack_2", false);
            Anim.CrossFade("Die_2", 0.0f);
            Enemy_Manager.Delite_ListEnemy(Enemy_ID);
            Debug.Log("���S");
            //�A�j���[�V�����Đ����Destroy
            Invoke(nameof(Die),0.35f);
        }
    }

    //���S���̊֐�
    void Die()
    {
        Destroy(gameObject);
    }

    //�U��
    //void OnCollisionStay(Collision other)
    //{
    //    if(other.gameObject.tag == "Player")
    //    {
    //        if (Time.time >= nextShotTime)
    //        {
    //            nextShotTime = Time.time + shotInterval;

    //            // �U���A�j���[�V�����Đ�
    //            if (gameObject.tag == "Enemy")
    //            {
    //                Anim.SetBool("Attack", true);
    //            }
    //            if (gameObject.tag == "WheellEnemy")
    //            {
    //                Anim.SetBool("Attack_1", true);
    //            }

    //            if (gameObject.tag == "FlyEnemy")
    //            {
    //                Anim.SetBool("Attack_2", true);
    //            }

    //            // �e�̔���
    //            Bullet_Enemy EB = GetComponent<Bullet_Enemy>();
    //            if (EB != null)
    //            {
    //                Debug.Log("����");
    //                EB.Shot();
    //                //�e���ˎ���SE�𗬂�
    //                AS.PlayOneShot(Attack_SE);
    //                AS.volume = Attack_v;
    //            }

    //            //Debug.Log("�G���e�������܂���");
    //        }

    //    }
    //}
}