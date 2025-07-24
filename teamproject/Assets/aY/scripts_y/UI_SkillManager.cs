using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillManager : MonoBehaviour
{
    private RectTransform r;
    [SerializeField] private GameObject SkillCardObj;

    public SkillController_y skillController;
    public List<UI_SkillCoolTime> UI_skillcooltime = new List<UI_SkillCoolTime>();

    public List<SkillManager> Skills = new List<SkillManager>();
    public List<GameObject> SkillCards = new List<GameObject>();

    public float Width;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        skillController = FindAnyObjectByType<SkillController_y>();
        r = GetComponent<RectTransform>();
        Width = r.rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        GetCard();

        SetCard();


    }

    void GetCard()
    {
        for(int i = 0; i < skillController.EquipActiveSkills.Count; i++)
        {
            if (!Skills.Contains(skillController.EquipActiveSkills[i]))
            {
                Skills.Add(skillController.EquipActiveSkills[i]);

                GameObject Card = Instantiate(SkillCardObj, this.transform);

                SkillCards.Add(Card);

                UI_skillcooltime.Add(SkillCards[i].GetComponent<UI_SkillCoolTime>());
            }
        }
    }

    void SetCard()
    {
        for(int i=0;i< SkillCards.Count;i++)
        {
            RectTransform Rect = SkillCards[i].GetComponent<RectTransform>();

            Rect.anchoredPosition = new Vector3((Width / (SkillCards.Count + 1)) * (i + 1), 50, 0);

            UI_skillcooltime[i].Skill = skillController.EquipActiveSkills[i].GetComponent<SkillManager>();
        }
        
    }
}
