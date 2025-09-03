using System.Collections;
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
        PlayerCont = GetComponentInParent<PlayerController_y1>();

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

        if (PlayerController_y1.GameState != "playing")
        {
            return;
        }

        if (SkillAction.WasPressedThisFrame() && PlayerCont.canAction)
        {
            if (EquipActiveSkills[SelectSkill].TimeCount >= EquipActiveSkills[SelectSkill].CoolTime)
            {
                StartCoroutine(UseSkill(SelectSkill));
            }
            
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

    public void AddSkill(GameObject skill)
    {
        bool none = true;

        for(int i=0;i<EquipActiveSkills.Count;i++)
        {
            if (EquipActiveSkills[i].skillName == (skill.GetComponent<SkillManager>()).skillName)
            {
                //int index = EquipActiveSkills.IndexOf(skill.GetComponent<SkillManager>());
                EquipActiveSkills[i].LevalUp();

                none = false;
            }
            
        }

        if (none)
        {
            GameObject value = Instantiate(skill, this.gameObject.transform);

            EquipActiveSkills.Add(value.GetComponent<SkillManager>());
        }
            
    }

    IEnumerator UseSkill(int select)
    {
        PlayerCont.canAction = false;
        PlayerCont.canMove = false;
        PlayerCont.canRotate = false;
        PlayerCont.canJump = false;

        PlayerCont.rb.linearVelocity = Vector3.zero;

        PlayerCont.Dash = false;

        PlayerCont.AnimationPlay("Skill");

        bool useSkill = false;

        for(float time = 0.0f;time < 0.9f;time += Time.deltaTime)
        {

            PlayerCont.rb.linearVelocity = Vector3.zero;
            if (time > 0.45f && !useSkill)
            {
                EquipActiveSkills[select].UseSkill();
                //
                useSkill = true;
            }

            yield return null;
        }

        PlayerCont.canAction = true;
        PlayerCont.canMove = true;
        PlayerCont.canRotate = true;
        PlayerCont.canJump = true;

        yield return null;
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
