using UnityEngine;

public class Skill_FireBall : SkillManager
{
    PlayerController_y1 PlayerCont;

    public GameObject Fireball;
    [SerializeField] AudioSource AS;
    [SerializeField] AudioClip FireSE;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerCont = FindAnyObjectByType<PlayerController_y1>();

        AS = GetComponentInParent<AudioSource>();

        skillName = SkillName.FireBall;

        CoolTime = 300;
        TimeCount = CoolTime;
    }

    private void FixedUpdate()
    {
        //ダメージ計算
        //式：(5 + (5 * レベル)) + (攻撃力 * 1.2)
        Damage = (int)((5 + (5 * Level)) + (PlayerCont.Status.Attack * 1.2f));

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
            //SE
            AS.PlayOneShot(FireSE);
            TimeCount = 0;
        }
        
    }
}
