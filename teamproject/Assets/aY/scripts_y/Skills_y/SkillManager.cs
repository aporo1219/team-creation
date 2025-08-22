using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public int CoolTime;
    public int TimeCount;

    public Sprite image;

    private int Level = 1;

    public int Damage;
    public int HealValue;

    public virtual void UseSkill()
    {

    }

    public virtual void LevalUp()
    {
        Level++;
    }
}
