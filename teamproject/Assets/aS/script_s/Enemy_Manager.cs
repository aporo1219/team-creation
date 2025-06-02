using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class Enemy_Manager : MonoBehaviour
{
   [SerializeField] GameObject Enemy;

    private int Enemy_HP;
    private int Enemy_Power;
    private bool Enemy_Die;

    public List<GameObject> Enemy_Date;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Enemy_HP = 10;//‰¼”’l
        Enemy_Die = false;//true‚ÅÁ‚¦‚é

        Enemy_Date = new List<GameObject>();
        Enemy_Date.Add(Enemy);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
            Debug.Log("Á‚¦‚½");
            Enemy_Die = true; 
            if(Enemy_Die)
            {
              After_Die();
            }
    }


    //G‹›“G‚ª€‚ñ‚¾Œã‚Ìˆ—
    void After_Die()
    {
        Destroy(Enemy_Date[0]);
    }  
}
