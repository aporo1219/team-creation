using UnityEngine;

public class BossSearchPlayer : MonoBehaviour
{
    public BossAction action;

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
