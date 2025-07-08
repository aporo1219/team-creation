using UnityEngine;

public class TreasureBox_y : MonoBehaviour
{
    Rigidbody rb;

    Animator animator;

    [SerializeField] ShowTaskSystem tasksystem;
    [SerializeField] TutorialManager manager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "GC" || collision.gameObject.name == "GF")
        {
            rb.linearVelocity = new Vector3(0, 3, 0);
            animator.SetBool("Hit", true);
            if (tasksystem.task == "•ó” ‚ðŠJ‚¯‚æ‚¤")
            {
                manager.Tutorial_Clear(7);
            }
        }
    }
}
