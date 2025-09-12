using UnityEngine;

public class truePlayer : MonoBehaviour
{
    GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        player.SetActive(true);
    }
}
