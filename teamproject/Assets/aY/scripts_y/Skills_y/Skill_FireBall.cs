using UnityEngine;

public class Skill_FireBall : SkillManager
{
    PlayerController_y1 PlayerCont;

    public GameObject Fireball;

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
            Instantiate(Fireball, PlayerCont.transform.position + PlayerCont.transform.forward * 2, PlayerCont.transform.rotation);
            Instantiate(Fireball, PlayerCont.transform.position + PlayerCont.transform.forward * 2 + PlayerCont.transform.right * 2, PlayerCont.transform.rotation);
            Instantiate(Fireball, PlayerCont.transform.position + PlayerCont.transform.forward * 2 + PlayerCont.transform.right * -2, PlayerCont.transform.rotation);
            TimeCount = 0;
        }
        
    }
}
