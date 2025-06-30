using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public bool tutorial_start_flag = true;
    public List<bool> tutorial_clear = new List<bool>();
    [SerializeField] List<GameObject> Tutorial_Obj = new List<GameObject>();

    [SerializeField] ShowTaskSystem tasksystem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Tutorial_Obj[0].SetActive(true);
        for(int i = 1;i<Tutorial_Obj.Count;i++)
        {
            Tutorial_Obj[i].SetActive(false);
        }

        Start_Tutorial();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Start_Tutorial()
    {
        tasksystem.change_task = "スティックを倒して前に進んでください";
    }

    void Now_Tutorial()
    {
        
    }
}
