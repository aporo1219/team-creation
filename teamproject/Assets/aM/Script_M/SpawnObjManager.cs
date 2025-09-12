using UnityEngine;

public class SpawnObjManager : MonoBehaviour
{
    public GameObject spawnobj;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            spawnobj.SetActive(true);
            Destroy(this);
        }
    }
}
