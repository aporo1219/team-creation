using UnityEngine;
using UnityEngine.Assertions.Must;

public class ViabilityChecker : MonoBehaviour
{
    bool do_puls = false;

    [SerializeField] GameObject viability_obj;
    [SerializeField] ShowTaskSystem tasksystem;

    // Update is called once per frame
    void Update()
    {
        if(viability_obj.activeSelf && !do_puls)
        {
            tasksystem.now_kill_num++;
            do_puls = true;
        }
    }
}
