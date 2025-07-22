using UnityEngine;

public class UI_SkillCoolTime : MonoBehaviour
{
    public RectTransform rectTransform;

    private UI_SkillManager UI_skillmanager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UI_skillmanager = FindAnyObjectByType<UI_SkillManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
