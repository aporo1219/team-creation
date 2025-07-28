using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Transform door;

    private Vector3 Door_Pos;
    private Quaternion Door_Rotation;
    private int Door_Speed = 10;
    private Vector3 Goal_Pos_X;
    private Vector3 Goal_Pos_Z;

    private int X_OR_Z = 0;     // �J������
    private int CX_OR_Z = 0;    // �������
    private bool isOpening = false;
    private bool isClosing = false;


    [SerializeField] AudioSource AS;
    [SerializeField] AudioClip DoorSE;
    private void Start()
    {
        Door_Pos = door.position;
        Door_Rotation = door.rotation;

        Goal_Pos_X = new Vector3(door.position.x + 20, door.position.y, door.position.z);
        Goal_Pos_Z = new Vector3(door.position.x, door.position.y, door.position.z + 20);
    }

    private void Update()
    {
        if (Time.timeScale == 0) return;

        if (isOpening)
        {
            switch (X_OR_Z)
            {
                case 1:
                    MoveDoorTowards(Goal_Pos_X);
                    break;
                case 2:
                    MoveDoorTowards(Goal_Pos_Z);
                    break;
            }
        }
        else if (isClosing)
        {
            switch (CX_OR_Z)
            {
                case 1:
                case 2:
                    MoveDoorTowards(Door_Pos);
                    break;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!isOpening && !isClosing)
            {
                //Rotation��Y���̊p�x�e�F�b�N
                float yRot = Door_Rotation.eulerAngles.y;

                //SE�𗬂�
                AS.PlayOneShot(DoorSE);
                if (yRot == 0 || yRot == 180)
                {
                    X_OR_Z = 1;
                }
                else if (yRot == 90 || yRot == 270)
                {
                    X_OR_Z = 2;
                }

                Open(X_OR_Z);
            }
        }
    }

    //�h�A�̓���
    void MoveDoorTowards(Vector3 target)
    {
        //������
        Vector3 next = Vector3.MoveTowards(door.position, target, Door_Speed * Time.deltaTime);
        next.y = Door_Pos.y;
        door.position = next;

        if (Vector3.Distance(door.position, target) < 0.01f)
        {
            //�J���Ƃ�
            if (isOpening)
            {
                isOpening = false;
                Invoke(nameof(StartClosing), 2.0f);
            }
            //����Ƃ�
            else if (isClosing)
            {
                isClosing = false;
                CX_OR_Z = 0;
                X_OR_Z = 0;
            }
        }
    }

    //�J��
    void Open(int direction)
    {
        isOpening = true;
        isClosing = false;
        CX_OR_Z = direction; // ����Ƃ��Ɏg������
        Debug.Log("�J��");
    }

    //�߂�J�n
    void StartClosing()
    {
        Debug.Log("����");
        isOpening = false;
        isClosing = true;
    }
}
