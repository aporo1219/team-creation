using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class TreasureBox_y : MonoBehaviour
{
    Rigidbody rb;

    Animator animator;

    bool IsOpen = false;

    [SerializeField] private AudioSource AS;
    [SerializeField] private AudioClip Open;

    [SerializeField] TutorialShowTaskSystem tasksystem;
    [SerializeField] TutorialManager manager;

    [SerializeField] List<GameObject> Skills = new List<GameObject>();

    [SerializeField] bool IsContentFixed = false;

    [SerializeField] GameObject FixedContent;

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
        if ((collision.gameObject.name == "GC" || collision.gameObject.name == "GF") && !IsOpen)
        {
            rb.linearVelocity = new Vector3(0, 3, 0);
            animator.SetBool("Hit", true);
            AS.PlayOneShot(Open);
            if (tasksystem != null)
            {
                if (tasksystem.task == "•ó” ‚ðŠJ‚¯‚æ‚¤")
                {
                    manager.Tutorial_Clear(7);

                }
            }

            GameObject Card;

            if (IsContentFixed && FixedContent != null)
            {
                Card = Instantiate(FixedContent, transform.position, transform.rotation);
            }
            else
            {
                Card = Instantiate(Skills[UnityEngine.Random.RandomRange(0, Skills.Count)], transform.position, transform.rotation);
            }
            

            Rigidbody Cardrb = Card.GetComponent<Rigidbody>();

            Cardrb.linearVelocity= new Vector3(0, 10, 0);

            IsOpen = true;
        }
    }
}
