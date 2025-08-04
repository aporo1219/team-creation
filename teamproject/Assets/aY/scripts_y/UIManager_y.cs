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
    public Text HPValue_Text;
    public Text HP_Text;

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
        if (playerStatus == null)
        {
            playerStatus = FindAnyObjectByType<PlayerStatus>();
        }
        if(skillController == null)
        {
            skillController = FindAnyObjectByType<SkillController_y>();
        }

        HP_Ber_Control();
        HP_Text_Control();
    }

    void HP_Ber_Control()
    {
        HP_Percentage = (float)playerStatus.HP / (float)playerStatus.MaxHP;
        HP_Ber.value = HP_Percentage;

        if (HP_Ber.value > 0.2f )
        {
            BerContent.color = Color.green;
        }
        else
        {
            BerContent.color = Color.red;
        }
    }

    void HP_Text_Control()
    {
        HPValue_Text.text = playerStatus.HP.ToString() + " / " + playerStatus.MaxHP.ToString();

        if (HP_Percentage > 0.2f)
        {
            HP_Text.color = Color.green;
        }
        else
        {
            HP_Text.color = Color.red;
        }
    }
}
