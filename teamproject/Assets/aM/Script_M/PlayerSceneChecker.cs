using UnityEngine;

public class PlayerSceneChecker : MonoBehaviour
{
    public Rigidbody rb;
    public PlayerController_y1 pc;
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
            if(NameChecker.scene == "Stage1" || NameChecker.scene == "Stage2" || NameChecker.scene == "Tutorial")
            {
                rb.useGravity = true;
                pc.canForce = true;
            }
            else if(NameChecker.scene == "Title" || NameChecker.scene == "GameOver" || NameChecker.scene == "Result" || NameChecker.scene == "StageSelect")
            {
                rb.useGravity = false;
                pc.canForce = false;
            }
        }
    }
}
