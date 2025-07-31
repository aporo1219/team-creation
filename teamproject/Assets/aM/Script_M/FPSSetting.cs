using UnityEngine;

public class FPSSetting : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
}
