using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus Instance;

    //�v���C���[�̃X�e�[�^�X�ϐ�----------
    public int Level = 1;           //���x��
    private int Exp = 0;            //�l���o���l
    public int MaxHP = 50;          //�ő�̗�
    public int HP = 50;             //���ݑ̗�
    private int DefaultAttack = 5;  //��b�U����
    private int DefaultDefense = 5; //��b�h���
    public float AttackRate;        //�U���͔{��
    public float DefenseRate;       //�h��͔{��
    public int Attack;              //�ŏI�U����(�_���[�W�v�Z�ɂ͂����p����)
    public int Defense;             //�ŏI�h���(�_���[�W�v�Z�ɂ͂����p����)
    //------------------------------------

    private bool dead = false;

    PlayerController_y1 PlayerCont;

    [SerializeField] SceneChenger SC;
    //�����蔻��̎��
    [HideInInspector] public enum ColliderMode
    {
        Neutral,    //�ʏ�
        Guard,      //�K�[�h
        Invincible, //���G
    }

    //�v���C���[�̓����蔻��
    public ColliderMode ColliderStste;
    //UI�̃o�t���X�g
   [SerializeField] private BuffList_Manager BuffList;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerCont = GetComponent<PlayerController_y1>();

        //�����蔻�菉����
        ColliderStste = ColliderMode.Neutral;
        //�o�t���X�g�擾
        BuffList = FindAnyObjectByType<BuffList_Manager>();
        //�U���́E�h��͔{��������
        AttackRate = 1.0f;
        DefenseRate = 1.0f;
        //�C���X�^���X�ݒ�
        Instance = this; 
    }

    

    // Update is called once per frame
    void Update()
    {

        if(BuffList == null)
        {
            BuffList = FindAnyObjectByType<BuffList_Manager>();
        }

        //����HP���ő�HP�𒴂�����
        if(HP > MaxHP)
        {
            HP = MaxHP;
        }
        if (HP == MaxHP)
            dead = false;
        //HP��0�ȉ��ɂȂ�����
        else if (HP <= 0)
        {
            HP = 0;


            if (!dead)
                StartCoroutine(GameOver());

            dead = true;
        }

        Attack = (int)(DefaultAttack * AttackRate);
        Defense = (int)(DefaultDefense * DefenseRate);
    }

    /// <summary>
    /// �v���C���[�Ƀ_���[�W��^����֐�
    /// </summary>
    /// <param name="damage">�_���[�W��</param>
    public void PlayerDamage(int damage)
    {
        if (ColliderStste == ColliderMode.Neutral)
        {
            //�h��͂��������Čv�Z���A�_���[�W��0�ȉ��Ȃ�Œ��1�_���[�W�^����
            if ((damage - (int)(Defense / 4)) > 0)
                HP -= damage - (int)(Defense / 4);
            else
                HP -= 1;
        }
        else if (ColliderStste == ColliderMode.Guard)
        {
            //�h��͂ƃK�[�h�̃_���[�W�y�����������Čv�Z���A�_���[�W��0�ȉ��Ȃ�Œ��1�_���[�W�^����
            if ((int)((damage - (int)(Defense / 4)) * 0.2f) > 0)
                HP -= (int)((damage - (int)(Defense / 4)) * 0.2f);
            else
                HP -= 1;
        }
        else if (ColliderStste == ColliderMode.Invincible)
        {
            
        }

    }

    /// <summary>
    /// �v���C���[�̍U���͂̔{����ύX����֐�
    /// </summary>
    /// <param name="ratevalue">�ω��{��</param>
    /// <param name="second">���ʎ���(�b)</param>
    /// <returns></returns>
    public IEnumerator SetAttackRate(float ratevalue, float second)
    {
        if (ratevalue > 1.0f)
            BuffList.BuffInput(BuffList_Manager.BuffCategory.AttackUP, second);
        else
            BuffList.BuffInput(BuffList_Manager.BuffCategory.AttackDOWN, second);

        if (ratevalue >= 0)
        {
            AttackRate += ratevalue - 1.0f;

            for (float i = 0.0f; i < second; i += Time.deltaTime)
            {
                yield return null;
            }

            AttackRate -= ratevalue - 1.0f;
        }
        yield return null;
    }

    /// <summary>
    /// �v���C���[�̖h��͂̔{����ύX����֐�
    /// </summary>
    /// <param name="ratevalue">�ω��{��</param>
    /// <param name="second">���ʎ���(�b)</param>
    /// <returns></returns>
    public IEnumerator SetDefenseRate(float ratevalue, float second)
    {
        if (ratevalue > 1.0f)
            BuffList.BuffInput(BuffList_Manager.BuffCategory.DefenseUP, second);
        else
            BuffList.BuffInput(BuffList_Manager.BuffCategory.DefenseDOWN, second);

        if (ratevalue >= 0)
        {
            DefenseRate += ratevalue - 1.0f;

            for (float i = 0.0f; i < second; i += Time.deltaTime)
            {
                yield return null;
            }

            DefenseRate -= ratevalue - 1.0f;
        }
        yield return null;
    }

    private IEnumerator GameOver()
    {
        PlayerCont.AnimationPlay("aaaa");

        PlayerCont.canAction = false;
        PlayerCont.canMove = false;
        PlayerCont.canRotate = false;

        

        for(float i=0;i<2.0f;i+=Time.deltaTime)
        {
            PlayerCont.rb.linearVelocity = Vector3.zero;
            yield return null;
        }

        //�Q�[���I�[�o�[�V�[���ɐ؂�ւ�
        Debug.Log("�Q�[���I�[�o�[");
        SceneManager.LoadScene("GameOver");

        yield return null;
    }
}