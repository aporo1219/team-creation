using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public bool tutorial_start = true;
    public List<bool> tutorial_clear = new List<bool>();
    [SerializeField] List<GameObject> Tutorial_Obj = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //チュートリアルを開始する
        if(tutorial_start)
        {
            Now_Tutorial();
        }
    }

    void Now_Tutorial()
    {
        
    }
}
