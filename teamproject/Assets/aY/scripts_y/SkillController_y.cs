using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkillController_y : MonoBehaviour
{
    PlayerController_y1 PlayerCont;

    public List<GameObject> EquipActiveSkills = new List<GameObject>();
    public List<GameObject> EquipPassiveSkills = new List<GameObject>();

    public int SelectSkill;

    public Skill_FireBall Skill_FireBall;

    private InputAction SkillAction;
    private InputAction NextAction;
    private InputAction PreviousAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerCont = GetComponent<PlayerController_y1>();

        SkillAction = InputSystem.actions.FindAction("Skill");
        NextAction = InputSystem.actions.FindAction("NextSkill");
        PreviousAction = InputSystem.actions.FindAction("PreviousSkill");

        SelectSkill = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        if (SkillAction.WasPressedThisFrame())
        {
            UseSkill();
        }

        if (NextAction.WasPressedThisFrame())
        {
            NextSkill();
        }

        if (PreviousAction.WasPressedThisFrame())
        {
            PreviousSkill();
        }
    }

    void UseSkill()
    {
        Skill_FireBall.UseSkill();
    }

    void NextSkill()
    {

    }

    void PreviousSkill()
    {

    }
}
