using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;


public class CubeMove : MonoBehaviour
{
    private float InputH = 0.0f;    //â°
    private float InputV = 0.0f;    //èc

    private Rigidbody RB;

    private void Start()
    {
        RB = GetComponent<Rigidbody>();
    }
    void Update()
    {
        InputH = Input.GetAxisRaw("Horizontal");
        InputV = Input.GetAxisRaw("Vertical");

        RB.linearVelocity = new Vector3(InputH, 0.0f, InputV);
    }

    void OnTriggerEnter(Collider other)
    {
         //Debug.Log("ÉqÉbÉg");
    }

    
    
}