using UnityEngine;

public class CameraController_y : MonoBehaviour
{
    public static CameraController_y instance;

    public PlayerController_y playerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        

        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
