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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        

        Instance = this; 
    }

    // Update is called once per frame
    void Update()
    {
        Attack  = (int)(DefaultAttack * AttackRate);
        Defense = (int)(DefaultDefense * DefenseRate);
    }
}
