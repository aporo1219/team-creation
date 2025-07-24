using UnityEngine;

public class Skill_Heal : SkillManager
{
    PlayerController_y1 PlayerCont;

    public GameObject Heal;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerCont = FindAnyObjectByType<PlayerController_y1>();

        CoolTime = 1800;
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
            Instantiate(Heal, new Vector3(PlayerCont.transform.position.x, PlayerCont.transform.position.y - 1.0f, PlayerCont.transform.position.z), PlayerCont.transform.rotation);
            PlayerCont.Status.HP += (int)(PlayerCont.Status.MaxHP * 0.4f);
            TimeCount = 0;
        }

    }
}
