using UnityEngine;
using UnityEngine.SceneManagement;

public class Clear : MonoBehaviour
{
    [SerializeField] SceneChenger SC;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            SceneChenger.instance.ChangeScene(5);
        }
    }
}
