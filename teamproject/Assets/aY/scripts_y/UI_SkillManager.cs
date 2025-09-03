using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillManager : MonoBehaviour
{
    private RectTransform r;
    [SerializeField] private GameObject SkillCardObj;
    [SerializeField] private Image Cursor;
    [SerializeField] private Image[] CursorArrows = new Image[2];

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
        if(skillController == null)
        {
            skillController = FindAnyObjectByType<SkillController_y>();
        }

        GetCard();

        SetCard();

        SetCursor();
    }

    /// <summary>
    /// 
    /// </summary>
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

    /// <summary>
    /// �X�L���J�[�h�����X�g���ɋϓ��ɕ��ׂ�֐�
    /// </summary>
    void SetCard()
    {
        //�X�L���J�[�h�̖������J��Ԃ�
        for(int i=0;i< SkillCards.Count;i++)
        {
            //�J�[�h��RectTransform�擾
            RectTransform Rect = SkillCards[i].GetComponent<RectTransform>();
            //�I�𒆂̃J�[�h�͏����ʒu��������
            if (skillController.SelectSkill == i)
            {
                //���X�g�̉������J�[�h�̖���+1�Ŋ��������Ŕz�u����
                Rect.anchoredPosition = new Vector3((Width / (SkillCards.Count + 1)) * (i + 1), 38, 0);
            }
            else
            {
                //���X�g�̉������J�[�h�̖���+1�Ŋ��������Ŕz�u����
                Rect.anchoredPosition = new Vector3((Width / (SkillCards.Count + 1)) * (i + 1), 33, 0);
            }
                
            Text leveltext = SkillCards[i].GetComponentInChildren<Text>();

            leveltext.text = ("Lv." + Skills[i].Level);

            UI_skillcooltime[i].Skill = skillController.EquipActiveSkills[i].GetComponent<SkillManager>();
        }
        
    }

    /// <summary>
    /// �I��ł���X�L���J�[�h�ɃJ�[�\�������킹��֐�
    /// </summary>
    void SetCursor()
    {
        Cursor.rectTransform.anchoredPosition = new Vector3((Width / (SkillCards.Count + 1)) * (skillController.SelectSkill + 1), 38, 0);

        if(SkillCards.Count > 1)
        {
            CursorArrows[0].gameObject.SetActive(true);
            CursorArrows[1].gameObject.SetActive(true);
        }
        else
        {
            CursorArrows[0].gameObject.SetActive(false);
            CursorArrows[1].gameObject.SetActive(false);
        }
    }
}
