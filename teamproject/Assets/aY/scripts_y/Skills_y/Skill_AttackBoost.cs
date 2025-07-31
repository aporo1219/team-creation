using System.Threading;
using UnityEngine;

public class Skill_AttackBoost : SkillManager
{
    PlayerController_y1 PlayerCont;

    public GameObject AttackBoost;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerCont = FindAnyObjectByType<PlayerController_y1>();

        CoolTime = 3600;
        TimeCount = CoolTime;
    }

    private void FixedUpdate()
    {
        if (TimeCount < CoolTime)
        {
            TimeCount++;
        }
    }

    public override void UseSkill()
    {
        if (TimeCount >= CoolTime)
        {
            Instantiate(AttackBoost, new Vector3(PlayerCont.transform.position.x, PlayerCont.transform.position.y - 1.0f, PlayerCont.transform.position.z), PlayerCont.transform.rotation);
            StartCoroutine(PlayerCont.Status.SetAttackRate(1.2f, 20));
            TimeCount = 0;
        }

    }
}
