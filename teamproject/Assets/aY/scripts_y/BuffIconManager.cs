using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffIconManager : MonoBehaviour
{

    public BuffList_Manager List_Manager;
    public Image BuffImage;

    public int BuffNum;

    private bool i;

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator BuffTimer(float second)
    {
        float a;
        a = BuffImage.color.a;

        for (;second > 0; second -= Time.deltaTime)
        {
            Debug.Log(second);

            if (second < 10.0f)
            {
                if (BuffImage.color.a < 0.5f)
                {
                    i = true;
                }
                else if(BuffImage.color.a > 1.0f)
                {
                    i = false;
                }

                if (i)
                {
                    a += Time.deltaTime;
                }
                else
                {
                    a -= Time.deltaTime;
                }

                Debug.Log(a);

                BuffImage.color = new Color(BuffImage.color.r, BuffImage.color.g, BuffImage.color.b, a);
            }

            yield return null;
        }


        List_Manager.BuffList.Remove(List_Manager.BuffList[BuffNum]);
        Destroy(gameObject);

        yield return null;
    }
}
