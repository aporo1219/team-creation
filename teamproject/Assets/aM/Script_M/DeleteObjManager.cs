using UnityEngine;

public class DeleteObjManager : MonoBehaviour
{
    [SerializeField] GameObject deleteobj;

    TaskSystem tasksystem;
    DualTaskSystem dualtasksystem;
    KillTaskSystem killtasksystem;

    private void Start()
    {
        tasksystem = GetComponent<TaskSystem>();
        dualtasksystem = GetComponent<DualTaskSystem>();
        killtasksystem = GetComponent<KillTaskSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tasksystem == null && dualtasksystem == null && killtasksystem == null)
        {
            deleteobj.SetActive(false);
        }
    }
}
