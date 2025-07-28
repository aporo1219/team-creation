using UnityEngine;

public class AttackBoost : MonoBehaviour
{
    PlayerController_y1 PlayerCont;

    int LifeTimeLimit = 60;
    int LifeTime = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerCont = FindAnyObjectByType<PlayerController_y1>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        transform.position = new Vector3(PlayerCont.transform.position.x, PlayerCont.transform.position.y - 1.0f, PlayerCont.transform.position.z);

        LifeTime++;

        if (LifeTime == LifeTimeLimit)
            Destroy(gameObject);
    }
}
