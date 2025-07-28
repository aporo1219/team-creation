using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffIconManager : MonoBehaviour
{
    public Image BuffImage;

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
            if (second < 10.0f)
            {
                if (BuffImage.color.a < 0.5f)
                {
                    i = true;
                }
                else if(BuffImage.color.a > 0.5f)
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

                BuffImage.color = new Color(BuffImage.color.r, BuffImage.color.g, BuffImage.color.b, a);
            }

            yield return null;
        }

        yield return null;
    }
}
