using UnityEngine;

public class PlayerSceneChecker : MonoBehaviour
{
    public Rigidbody rb;
    public PlayerController_y1 pc;
    public PlayerStatus status;
    public string Now_Scene;
    GameObject scene;
    SceneNameChecker NameChecker;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scene = GameObject.Find("Scene");
        if (scene != null)
            NameChecker = scene.GetComponent<SceneNameChecker>();
        if (NameChecker != null)
        {
            if (NameChecker.scene == "Title")
            {
                rb.useGravity = false;
                pc.canForce = false;
                gameObject.transform.position = new Vector3(0, 0, 0);
            }
        }

        Now_Scene = "null";
    }

    // Update is called once per frame
    void Update()
    {
        //今どのシーンにいるかのチェック
        scene = GameObject.Find("Scene");
        if (scene != null)
            NameChecker = scene.GetComponent<SceneNameChecker>();
        if(NameChecker != null )
        {
            if(NameChecker.scene == "Stage1" || NameChecker.scene == "Stage2")
            {
                rb.useGravity = true;
                pc.canForce = true;
                Now_Scene = NameChecker.scene;
            }
            else if(NameChecker.scene == "Tutorial")
            {
                rb.useGravity = true;
                pc.canForce = true;
                status.HP = status.MaxHP;
            }
            else if(NameChecker.scene == "Title" || NameChecker.scene == "Result" || NameChecker.scene == "StageSelect")
            {
                rb.useGravity = false;
                pc.canForce = false;
                Now_Scene = "null";
            }
            else if(NameChecker.scene == "GameOver")
            {
                rb.useGravity = false;
                pc.canForce = false;
            }
        }
    }
}
