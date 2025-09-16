using UnityEngine;

public class ShowEnemys : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] PlayerSceneChecker psc;

    private void Update()
    {
        transform.position = player.transform.position;
    }

    private void OnTriggerStay(Collider other)
    {
        if (psc.Now_Scene != "null")
        {
            if (other.gameObject.tag == "Enemy" && !other.gameObject.transform.GetChild(0).gameObject.activeSelf)
            {
                other.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
