using System;
using System.Collections;
using UnityEngine;

public class E_Move_y : MonoBehaviour
{
    PlayerController_y1 PlayerCont;

    Rigidbody rb;

    bool move = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerCont = FindAnyObjectByType<PlayerController_y1>();

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 MoveVec = PlayerCont.transform.position - transform.position;

        MoveVec = new Vector3(MoveVec.x, 0, MoveVec.z);

        if(move)
        {
            if (MoveVec.magnitude > 7.0f)
            {
                MoveVec.Normalize();

                rb.linearVelocity = MoveVec * 5 + new Vector3(0, rb.linearVelocity.y, 0);

                if (MoveVec != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(new Vector3(MoveVec.z, 0, MoveVec.x * -1));
                }
            }
            else if (MoveVec.magnitude < 3.0f)
            {
                MoveVec.Normalize();

                rb.linearVelocity = MoveVec * -2 + new Vector3(0, rb.linearVelocity.y, 0);

                if (MoveVec != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(new Vector3(MoveVec.z, 0, MoveVec.x * -1));
                }
            }
            else
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x * 0.8f, rb.linearVelocity.y, rb.linearVelocity.z * 0.8f);

                if (MoveVec != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(new Vector3(MoveVec.z, 0, MoveVec.x * -1));
                }
            }
        }
        


    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "GC")
        {
            rb.linearVelocity = PlayerCont.transform.forward * 7;
            StartCoroutine(HitStop(0.3f));
            Debug.Log("hit,C");
        }

        if (collision.gameObject.name == "GF")
        {
            rb.linearVelocity = PlayerCont.transform.forward * 10 + new Vector3(0, 3, 0);
            StartCoroutine(HitStop(1.0f));
            Debug.Log("hit,F");
        }

        if(collision.gameObject.tag == "PlayerSkill")
        {
            rb.linearVelocity = PlayerCont.transform.forward * 30 + new Vector3(0, 10, 0);
            StartCoroutine(HitStop(1.0f));
            Debug.Log("hit,S");
        }
    }
    
    IEnumerator HitStop(float time = 1.0f)
    {
        move = false;

        for(float t = 0.0f; t < time; t += Time.deltaTime)
        {
            if (t < time * 0.8f)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, rb.linearVelocity.z);
            }
            else
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x * 0.8f, rb.linearVelocity.y, rb.linearVelocity.z * 0.8f);
            }
            yield return null;
        }

        move = true;

        yield return null;
    }
}
