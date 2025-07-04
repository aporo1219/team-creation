using UnityEngine;

public class SkillController_y : MonoBehaviour
{
    PlayerController_y1 PlayerCont;

    public GameObject[] EquipActiveSkills = new GameObject[3];
    public GameObject[] EquipPassiveSkills = new GameObject[3];

    public int SelectSkill;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerCont = GetComponent<PlayerController_y1>();

        SelectSkill = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
