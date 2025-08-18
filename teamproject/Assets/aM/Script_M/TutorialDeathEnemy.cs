using UnityEngine;

public class TutorialDeathEnemy : MonoBehaviour
{
    [SerializeField] GameObject viability_obj;
    [SerializeField] TutorialShowTaskSystem tasksystem;

    bool num_puls = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(viability_obj != null)
        {
            if(viability_obj.activeSelf && !num_puls)
            {
                tasksystem.now_kill_num++;
                num_puls = true;
            }
        }
    }
}
