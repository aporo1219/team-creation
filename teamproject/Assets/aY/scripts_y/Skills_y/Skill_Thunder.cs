using UnityEngine;

public class Skill_Thunder : SkillManager
{
    PlayerController_y1 PlayerCont;

    public GameObject Thunder;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerCont = FindAnyObjectByType<PlayerController_y1>();

        CoolTime = 600;

        TimeCount = CoolTime;
    }

    private void FixedUpdate()
    {
        Damage = (int)(PlayerCont.Status.Attack * 6.0f);

        if (TimeCount < CoolTime)
        {
            TimeCount++;
        }
    }

    public override void UseSkill()
    {
        if (TimeCount >= CoolTime)
        {
            Instantiate(Thunder, PlayerCont.transform.position + PlayerCont.transform.forward * 10, PlayerCont.transform.rotation);
            TimeCount = 0;
        }

    }
}
