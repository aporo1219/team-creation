using System.Collections.Generic;
using UnityEngine;

public class ButtonSystem : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] GameObject Pusher;
    [SerializeField] List<GameObject> Move_Obj = new List<GameObject>();

    int ChangeColor_Time = 60;
    bool push_button = false;

    private void FixedUpdate()
    {
        if (push_button)
            ChangeColor_Time--;
        if (ChangeColor_Time <= 0)
        {
            push_button = false;
        }
        if (ChangeColor_Time <= 0 && !push_button)
        {
            for (int i = 0; i < Move_Obj.Count; i++)
            {
                if (Move_Obj[i] != null)
                    Move_Obj[i].SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        //プレイヤーの攻撃が当たったら
        if (collision.gameObject.name == "GC" || collision.gameObject.name == "GF")
        {
            Debug.Log("ボタンが押されたよ☆");
            anim.SetBool("StartAnim", true);
            Pusher.GetComponent<Renderer>().material.color = Color.green;
            push_button = true;
        }
    }
}
