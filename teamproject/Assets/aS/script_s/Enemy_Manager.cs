using System.ComponentModel;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
   
    [SerializeField] GameObject Enemy_Object;//�G�̃^�O��������ϐ�
    [SerializeField] Move_Enemy Enemy;//�G�̃X�N���v�g�擾�ϐ�
    [SerializeField] GameObject Player;//�v���C���[�̃^�O��������ϐ�
    [SerializeField] PlayerController_y Player_Script;//�v���C���[�̃X�N���v�g�擾�ϐ�

    private bool Enemy_Die;
    private int Enemy_Number;
    private string Player_Combo;//�v���C���[�̃R���{�󋵂��m�F����X�N���v�g
    

    public int Enemy_HP;
    public int Enemy_Power;
    public Enemy_Manager Instance;

    //�C���X�^���X�̑��
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Enemy_HP = 100;//�����l
        Enemy_Die = false;//true�ŏ�����
        Enemy_Number = 10;//���G�̐�
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
    public void Entry_Enemy(Enemy_Status Enemy)
    {

    }

    //ID�̓o�^����
    public void Delite_ListEnemy(Enemy_Status Enemy)
    {

    }
}
