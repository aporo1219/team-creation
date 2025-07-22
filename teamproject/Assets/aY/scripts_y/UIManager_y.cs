using UnityEngine;
using UnityEngine.UI;

public class UIManager_y : MonoBehaviour
{
    //ÉNÉâÉX
    PlayerStatus playerStatus;
    SkillController_y skillController;

    //
    public Slider HP_Ber;
    public Image BerContent;

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
        HP_Ber_Control();
    }

    void HP_Ber_Control()
    {
        HP_Ber.value = (float)playerStatus.HP / (float)playerStatus.MaxHP;

        if(HP_Ber.value > 0.2f )
        {
            BerContent.color = Color.green;
        }
        else
        {
            BerContent.color = Color.red;
        }
    }
}
