using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class stairs : MonoBehaviour
{
    public int Change_Scene;
    public int Stair_num;
    int playerpos_changetime = 0;
    GameObject player;
    StageClearChecker ClearChecker;

    GameObject cinemachineCamera;
    CinemachinePanTilt cinemachine;
    Rigidbody rb;

    private void Start()
    {
        cinemachineCamera = GameObject.Find("CinemachineCamera");
        cinemachine = cinemachineCamera.GetComponent<CinemachinePanTilt>();
        player = GameObject.Find("Player");
        rb = player.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (playerpos_changetime != 0)
            playerpos_changetime--;

        if (playerpos_changetime == 1)
        {
            rb.isKinematic = true;
            if (Stair_num == 10)
            {
                player.transform.position = new Vector3(-52, 1.5f, 165);
                player.transform.rotation = new Quaternion(0, 180, 0, 0);
                cinemachine.PanAxis.Value = 180;
                cinemachine.TiltAxis.Value = 10;
            }
            if (Stair_num == 21)
            {
                player.transform.position = new Vector3(156, 1.5f, 176);
                player.transform.rotation = new Quaternion(0, 0, 0, 0);
                cinemachine.PanAxis.Value = 0;
                cinemachine.TiltAxis.Value = 10;
            }
            rb.isKinematic = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Change_Scene > 0 && Change_Scene < 4)
            {
                ClearChecker = other.gameObject.GetComponent<StageClearChecker>();
                if (ClearChecker != null)
                    if (ClearChecker.clear_flag[Change_Scene - 1] != true && Change_Scene == 2)
                        ClearChecker.clear_flag[Change_Scene - 1] = true;
                SceneChenger.instance.ChangeScene(Change_Scene + 2);
                playerpos_changetime = 34;
            }
        }
    }
}
