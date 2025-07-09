using UnityEngine;

public class Skill_FireBall : MonoBehaviour
{
    PlayerController_y1 PlayerCont;

    public GameObject Fireball;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerCont = FindAnyObjectByType<PlayerController_y1>();
    }

    public void UseSkill()
    {
        Instantiate(Fireball, PlayerCont.transform.position + PlayerCont.transform.forward * 2, PlayerCont.transform.rotation);
        Instantiate(Fireball, PlayerCont.transform.position + PlayerCont.transform.forward * 2 + PlayerCont.transform.right * 2, PlayerCont.transform.rotation);
        Instantiate(Fireball, PlayerCont.transform.position + PlayerCont.transform.forward * 2 + PlayerCont.transform.right *-2, PlayerCont.transform.rotation);
    }
}
