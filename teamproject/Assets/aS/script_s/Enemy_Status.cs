using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Status : MonoBehaviour
{
    [SerializeField] GameObject Player;//�v���C���[�̃^�O��������ϐ�
    [SerializeField] PlayerController_y Player_Script;//�v���C���[�̃X�N���v�g�擾�ϐ�

    private string Player_Combo;//�v���C���[�̃R���{�󋵂��m�F����X�N���v�g
    private int Count;
    private Enemy_Manager Enemy_manager;
   

    public static Enemy_Status Instance;
    public int Enemy_HP;
    public int Enemy_Power;
    //�G��ID
    public int Enemy_ID { get; private set; }

     void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
       Enemy_ID = Enemy_Manager.Entry_Enemy_ID(this);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Count = 5;
        Enemy_HP = 120;//�����l
        Enemy_Power = 5;//�����l
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Enemy_HP < 0)
        {
            Die();
        }

        if (Move_Enemy.Damede_Hit)
        {
            Debug.Log("�U�����󂯂�");
            Be_Attack(PlayerController_y.instance.AttackState);
        }
    }

    //�G���G�̍U�����󂯂������̊֐�
    void Be_Attack(string Combo)
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
    void OnDestroy()
    {
        Enemy_Manager.Delite_ListEnemy(Enemy_ID);
        Debug.Log("���S");
        //Enemy_Manager.Destroy_Enemy_ID(1);
    }

    //���S���̊֐�
    void Die()
    {
        Destroy(gameObject);
    }
}
