using UnityEngine;

public class UIManager : MonoBehaviour
{
    PlayerController_y1 Player;
    [SerializeField]GameObject SkillList;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player = FindAnyObjectByType<PlayerController_y1>();    
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.SkillCont.EquipActiveSkills.Count > 0)
        {
            SkillList.SetActive(true);
        }
        else
        {
            SkillList.SetActive(false);
        }
    }
}
