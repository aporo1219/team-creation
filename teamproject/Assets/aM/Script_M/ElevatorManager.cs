using UnityEngine;
using UnityEngine.InputSystem;

public class ElevatorManager : MonoBehaviour
{
    [SerializeField] GameObject Show_Enter;
    Collider player;

    InputAction selectAction;

    int playerpos_changetime = 0;

    bool near_elevator = false;

    private void Start()
    {
        selectAction = InputSystem.actions.FindAction("Select");
    }

    private void Update()
    { 
        if (near_elevator && selectAction.WasPressedThisFrame())
        {
            SceneChenger.instance.ChangeScene(1);
            playerpos_changetime = 34;
        }

        if (playerpos_changetime != 0) playerpos_changetime--;
        if (playerpos_changetime == 1)
            player.gameObject.transform.position = new Vector3(0, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Show_Enter.SetActive(true);
            near_elevator = true;
            player = other;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Show_Enter.SetActive(false);
            near_elevator = false;
        }
    }
}
