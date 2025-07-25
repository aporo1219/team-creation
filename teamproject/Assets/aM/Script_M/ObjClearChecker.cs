using UnityEngine;

public class ObjClearChecker : MonoBehaviour
{
    GameObject player;
    StageClearChecker clearchecker;

    public int Stage_Num;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        clearchecker = player.GetComponent<StageClearChecker>();

        if(clearchecker.clear_flag[Stage_Num] == true)
        {
            Destroy(this.gameObject);
        }
    }
}
