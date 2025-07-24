using System.Collections.Generic;
using UnityEngine;

public class TutorialButtonSystem : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] GameObject Pusher;
    [SerializeField] List<GameObject> Move_Obj = new List<GameObject>();

    [SerializeField] ShowTaskSystem tasksystem;
    [SerializeField] TutorialManager manager;

    [SerializeField] AudioSource AS;
    [SerializeField] AudioClip Push;

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
        //�v���C���[�̍U��������������
        if (collision.gameObject.name == "GC" || collision.gameObject.name == "GF")
        {
            Debug.Log("�{�^���������ꂽ�恙");
            anim.SetBool("StartAnim", true);
            AS.PlayOneShot(Push);
            push_button = true;
            if (this.name == "Button")
                Pusher.GetComponent<Renderer>().material.color = Color.green;
            if (tasksystem.assist_text == "�U���𓖂Ă�Ƃǂ����̎d�|��������")
            {
                manager.Tutorial_Clear(8);
            }
        }
    }
}
