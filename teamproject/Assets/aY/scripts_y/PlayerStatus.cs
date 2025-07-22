using System;
using System.Collections;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus Instance;

    public int Level;
    private int Exp;
    public int MaxHP;
    public int HP;
    private int DefaultAttack;
    private int DefaultDefense;
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
}
