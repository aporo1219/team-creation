using UnityEngine;
using UnityEngine.Assertions.Must;

public class ViabilityChecker : MonoBehaviour
{
    public bool do_puls = false;

    [SerializeField] GameObject viability_obj;
    GameObject Camera;
    ShowTaskSystem tasksystem;

    private void Start()
    {
        Camera = GameObject.Find("Main Camera");
        tasksystem = Camera.GetComponent<ShowTaskSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (viability_obj.activeSelf && !do_puls)
        {
            do_puls = true;
        }
    }
}
