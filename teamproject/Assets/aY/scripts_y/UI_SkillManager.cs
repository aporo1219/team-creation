using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillManager : MonoBehaviour
{
    private RectTransform r;

    public SkillController_y skillController;

    public List<GameObject> Skills = new List<GameObject>();

    public float Width;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        r = GetComponent<RectTransform>();
        Width = r.rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        SetCard();


    }

    void SetCard()
    {
        for(int i=0;i<Skills.Count;i++)
        {
            RectTransform Rect = Skills[i].GetComponent<RectTransform>();

            Rect.anchoredPosition = new Vector3((Width / (Skills.Count + 1)) * (i + 1), 50, 0);
        }
        
    }
}
