using UnityEngine;

public class BossSearchPlayer : MonoBehaviour
{
    public BossAction action;
    public GameObject Boss;

    private void Update()
    {
        this.gameObject.transform.position = Boss.transform.position;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            action.player_pos = other.transform.position;
            action.near_player = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            action.near_player = false;
        }
    }
}
