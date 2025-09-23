using Unity.Cinemachine;
using UnityEngine;

public class fallPlayer : MonoBehaviour
{
    public int stairs;

    GameObject player;
    GameObject cinemachineCamera;
    CinemachinePanTilt cinemachine;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            cinemachineCamera = GameObject.Find("CinemachineCamera");
            cinemachine = cinemachineCamera.GetComponent<CinemachinePanTilt>();

            player = GameObject.Find("Player");
            switch (stairs)
            {
                case 0:
                    break;
                case 1:
                    player.gameObject.transform.position = new Vector3(-52, 1.5f, -58);
                    player.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
                    cinemachine.PanAxis.Value = 0;
                    cinemachine.TiltAxis.Value = 10;
                    break;
                case 2:
                    player.gameObject.transform.position = new Vector3(0, 1.5f, 23);
                    player.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
                    cinemachine.PanAxis.Value = 0;
                    cinemachine.TiltAxis.Value = 10;
                    break;
            }
        }
    }
}
