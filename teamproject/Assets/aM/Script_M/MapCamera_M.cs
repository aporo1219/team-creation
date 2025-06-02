using System.Drawing;
using UnityEngine;

public class MapCamera_M : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform camera;

    Vector3 pos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pos = player.transform.position;
        pos.y = pos.y + 30;
        camera.transform.position = pos;
    }
}
