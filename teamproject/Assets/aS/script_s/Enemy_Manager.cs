using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
   
    [SerializeField] GameObject Enemy_Object;//�G�̃^�O��������ϐ�
    [SerializeField] Move_Enemy Enemy;//�G�̃X�N���v�g�擾�ϐ�
    [SerializeField] GameObject Player;//�v���C���[�̃^�O��������ϐ�
    [SerializeField] PlayerController_y Player_Script;//�v���C���[�̃X�N���v�g�擾�ϐ�

    private bool Enemy_Die;
    private static int Enemy_Number;//ID���蓖��
    private static Dictionary<int, Enemy_Status> Enemys = new();
   
    public static Enemy_Manager Instance;
    

    //�C���X�^���X�̑��
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        Enemy_Die = false;//true�ŏ�����
        Enemy_Number = 0;//���G�̐�
        Enemy_Object = GameObject.FindWithTag("Enemy");
        Enemy = Enemy_Object.GetComponent<Move_Enemy>();
        Player = GameObject.FindWithTag("Player");
        Player_Script = Player.GetComponent<PlayerController_y>();
    }
    


    // Update is called once per frame
    void FixedUpdate()
    {
        
       
    }

   
    //ID�̓o�^
    public static int Entry_Enemy_ID(Enemy_Status Enemy)
    {
        int Enemy_Tag = Enemy_Number++;
        Enemys[Enemy_Tag] = Enemy;
        return Enemy_Tag ;
    }

    //ID�̓o�^����(�j�󂳂ꂽ�Ƃ��j
    public static void Delite_ListEnemy(int Enemy_Tag)
    {
        Enemys.Remove(Enemy_Tag);
       
    }

    //ID�w��ł̓G�̔j��
    public static void Destroy_Enemy_ID(int Enemy_Tag)
    {
        if(Enemys.TryGetValue(Enemy_Tag, out Enemy_Status Enemy))
        {
            if(Enemy != null)
            {
                Object.Destroy(Enemy.gameObject);
            }
            else
            {
                Debug.Log($"�GID{Enemy_Tag}�̃I�u�W�F�N�g��null�ł�");
            }
        }
        else
        {
            Debug.Log($"�GID{Enemy_Tag}�͑��݂��Ȃ�");
        }
    }
}
