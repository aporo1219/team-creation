using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffList_Manager : MonoBehaviour
{
    public GameObject BuffCard;
    public Sprite[] BuffIcon = new Sprite[4];

    public List<GameObject> BuffList = new List<GameObject>();

    public enum BuffCategory 
    { 
        AttackUP, AttackDOWN, DefenseUP, DefenseDOWN
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < BuffList.Count; i++)
        {
            RectTransform Rect = BuffList[i].GetComponent<RectTransform>();
            
            Rect.anchoredPosition = new Vector3(i * 30 + 15, 0, 0);

            BuffIconManager iconManager = BuffList[i].GetComponent<BuffIconManager>();

            iconManager.BuffNum = i;
        }
    }

    public void BuffInput(BuffCategory category, float second)
    {
        int i;
        for (i = 0; i < BuffList.Count; i++)
        {
            
            
        }

        GameObject Card = Instantiate(BuffCard, this.transform);

        Image image = Card.GetComponent<Image>();

        switch (category)
        {
            case BuffCategory.AttackUP:
                image.sprite = BuffIcon[0];
                break;
            case BuffCategory.AttackDOWN:
                image.sprite = BuffIcon[1];
                break;
            case BuffCategory.DefenseUP:
                image.sprite = BuffIcon[2];
                break;
            case BuffCategory.DefenseDOWN:
                image.sprite = BuffIcon[3];
                break;
        }

        BuffIconManager iconManager = Card.GetComponent<BuffIconManager>();
        iconManager.List_Manager = this;
        iconManager.BuffImage = image;

        StartCoroutine(iconManager.BuffTimer(second));

        BuffList.Add(Card);
    }



}
