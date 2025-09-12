using UnityEngine;

public class FPSSetting : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        Application.targetFrameRate = 60;
    }
}
