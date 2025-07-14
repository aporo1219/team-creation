using UnityEngine;

public class Skill_Thunder : SkillManager
{
    PlayerController_y1 PlayerCont;

    public GameObject Thunder;
    int CoolTime = 0;
    int TimeCount = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerCont = FindAnyObjectByType<PlayerController_y1>();

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
            Instantiate(Thunder, PlayerCont.transform.position + PlayerCont.transform.forward * 10, PlayerCont.transform.rotation);
            TimeCount = 0;
        }

    }
}
