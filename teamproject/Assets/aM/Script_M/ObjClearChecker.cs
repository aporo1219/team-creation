using UnityEngine;

public class ObjClearChecker : MonoBehaviour
{
    GameObject player;

    public int Stage_Num;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
