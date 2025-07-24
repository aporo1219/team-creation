using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public int CoolTime;
    public int TimeCount;

    public Sprite image;

    public int Damage;
    public int HealValue;

    public virtual void UseSkill()
    {

    }
}
