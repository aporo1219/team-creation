using UnityEngine;

public class Skill_Thunder : SkillManager
{
    PlayerController_y1 PlayerCont;

    public GameObject Thunder;
    [SerializeField] AudioSource AS;
    [SerializeField] AudioClip ThunSE;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerCont = FindAnyObjectByType<PlayerController_y1>();

        AS = GetComponentInParent<AudioSource>();

        skillName = SkillName.Thunder;

        CoolTime = 600;

        TimeCount = CoolTime;
    }

    private void FixedUpdate()
    {
        //ダメージ計算
        //式：(5 + (5 * レベル)) + (攻撃力 * 1.2)
        Damage = (int)((10 + (5 * Level)) + (PlayerCont.Status.Attack * 2.0f));

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
            //SE
            AS.PlayOneShot(ThunSE);
            TimeCount = 0;
        }

    }
}
