using System;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public int CoolTime;
    public int TimeCount;

    public Sprite image;

    public int Level = 1;

    public int Damage;
    public int HealValue;

    [HideInInspector] public enum SkillName
    { 
        FireBall,
        Thunder,
        Heal,
        AttackBoost,
    }

    public SkillName skillName;

    public virtual void UseSkill()
    {

    }

    public virtual void LevalUp()
    {
        Level++;
    }
}
