using UnityEngine;

public class TaskSystem : MonoBehaviour
{
    public string text;

    [SerializeField] ShowTaskSystem tasksystem;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            tasksystem.change_task = text;
            tasksystem.change_task_flag = true;
            Destroy(gameObject);
        }
    }
}
