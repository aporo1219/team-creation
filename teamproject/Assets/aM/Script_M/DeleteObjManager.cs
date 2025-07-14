using UnityEngine;

public class DeleteObjManager : MonoBehaviour
{
    [SerializeField] GameObject deleteobj;

    TaskSystem tasksystem;

    // Update is called once per frame
    void Update()
    {
        if (tasksystem == null)
        {
            deleteobj.SetActive(false);
        }
    }
}
