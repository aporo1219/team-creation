using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkillController_y : MonoBehaviour
{
    PlayerController_y1 PlayerCont;

    public List<SkillManager> EquipActiveSkills = new List<SkillManager>();
    public List<GameObject> EquipPassiveSkills = new List<GameObject>();

    public int SelectSkill;

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
        EquipActiveSkills[SelectSkill].UseSkill();
    }

    void NextSkill()
    {
        SelectSkill++;
        if (SelectSkill > EquipActiveSkills.Count - 1)
        {
            SelectSkill = 0;
        }
    }

    void PreviousSkill()
    {
        SelectSkill--;
        if (SelectSkill < 0)
        {
            SelectSkill = EquipActiveSkills.Count - 1;
        }
    }
}
