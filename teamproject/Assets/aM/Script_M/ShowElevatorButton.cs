using UnityEngine;

public class ShowElevatorButton : MonoBehaviour
{
    [SerializeField] GameObject Tutorial_Button;
    [SerializeField] GameObject Stage1_Button;
    [SerializeField] GameObject Stage2_Button;
    [SerializeField] GameObject Stage3_Button;

    GameObject player;
    StageClearChecker clearchecker;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        clearchecker = player.GetComponent<StageClearChecker>();
        Tutorial_Button.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(clearchecker != null)
        {
            if (clearchecker.clear_flag[0] == true)
            {
                Stage1_Button.SetActive(true);
            }
            if(clearchecker.clear_flag[1] == true)
            {
                Stage2_Button.SetActive(true);
            }
            if(clearchecker.clear_flag[2] == true)
            {
                Stage3_Button.SetActive(true);
            }
        }
    }
}
