using UnityEngine;
using UnityEngine.UI;

public class UIManager_y : MonoBehaviour
{
    //ÉNÉâÉX
    PlayerStatus playerStatus;
    SkillController_y skillController;

    //
    public Image Health;

    //ïœêî
    float HP_Percentage;
    float HP_Ber_Range;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStatus = FindAnyObjectByType<PlayerStatus>();
        skillController = FindAnyObjectByType<SkillController_y>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HP_Ber_Control()
    {
        HP_Ber_Range = Health.flexibleWidth;

        HP_Percentage = playerStatus.HP / playerStatus.MaxHP;

        HP_Ber_Range *= HP_Percentage;

    }
}
