using UnityEngine;
using UnityEngine.UI;

public class UI_SkillCoolTime : MonoBehaviour
{
    public RectTransform rectTransform;

    private UI_SkillManager UI_skillmanager;
    public SkillManager Skill;
    [SerializeField] private Slider Gauge;
    [SerializeField] private Image image;

    public float CoolTimeRaito = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UI_skillmanager = FindAnyObjectByType<UI_SkillManager>();
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Skill == null) return;

        if(image.sprite == null && Skill.image != null)
        {
            image.sprite = Skill.image;
        }

        if(Skill.CoolTime != 0)
        {
            CoolTimeRaito = 1.0f - ((float)Skill.TimeCount / (float)Skill.CoolTime);
        }
        
        Gauge.value = CoolTimeRaito;
    }
}
