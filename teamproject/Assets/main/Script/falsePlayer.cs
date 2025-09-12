using UnityEngine;

public class falsePlayer : MonoBehaviour
{
    GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        player.SetActive(false);
    }
}
