using System;
using System.Collections;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus Instance;

    public int Level = 1;
    private int Exp = 0;
    public int MaxHP = 50;
    public int HP = 50;
    private int DefaultAttack = 5;
    private int DefaultDefense = 5;
    public float AttackRate;
    public float DefenseRate;
    public int Attack;
    public int Defense;

    [HideInInspector]
    public enum ColliderMode
    {
        Neutral,Guard,Invincible,
    }

    public ColliderMode ColliderStste;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ColliderStste = ColliderMode.Neutral;

        AttackRate = 1.0f;
        DefenseRate = 1.0f;

        Instance = this; 
    }

    // Update is called once per frame
    void Update()
    {
        if(HP > MaxHP)
        {
            HP = MaxHP;
        }
        else if(HP < 0)
        {
            HP = 0;
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
        if (ratevalue >= 0)
        {
            AttackRate += ratevalue - 1.0f;

            for (float i = 0.0f; i < second; i += Time.deltaTime) ;

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
        if (ratevalue >= 0)
        {
            DefenseRate += ratevalue - 1.0f;

            for (float i = 0.0f; i < second; i += Time.deltaTime) ;

            DefenseRate -= ratevalue - 1.0f;
        }
        yield return null;
    }
}
