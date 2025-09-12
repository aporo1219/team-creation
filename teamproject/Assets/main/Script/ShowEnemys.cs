using UnityEngine;

public class ShowEnemys : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy" && !other.gameObject.transform.GetChild(0).gameObject.activeSelf)
        {
            other.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Enemy")
        {
            other.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
