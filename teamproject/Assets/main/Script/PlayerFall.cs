using Unity.Cinemachine;
using UnityEngine;

public class PlayerFall : MonoBehaviour
{
    GameObject player;
    GameObject cinemachineCamera;
    CinemachinePanTilt cinemachine;

    [SerializeField] SceneNameChecker snc;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player = GameObject.Find("Player");
            cinemachineCamera = GameObject.Find("CinemachineCamera");
            cinemachine = cinemachineCamera.GetComponent<CinemachinePanTilt>();

            if(snc.scene == "Tutorial")
            {
                player.transform.position = new Vector3(0, 1.5f, 0);
                player.transform.rotation = new Quaternion(0, 0, 0, 0);
                cinemachine.PanAxis.Value = 0;
                cinemachine.TiltAxis.Value = 10;
            }
            if(snc.scene == "Stage1")
            {
                player.gameObject.transform.position = new Vector3(-52, 1.5f, -58);
                player.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
                cinemachine.PanAxis.Value = 0;
                cinemachine.TiltAxis.Value = 10;
            }
            if(snc.scene == "Stage2")
            {
                player.gameObject.transform.position = new Vector3(0, 1.5f, 23);
                player.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
                cinemachine.PanAxis.Value = 0;
                cinemachine.TiltAxis.Value = 10;
            }
        }
    }
}
