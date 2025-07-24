using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class stairs : MonoBehaviour
{
    public int Change_Scene;
    int playerpos_changetime = 0;
    Collider player_col;
    StageClearChecker ClearChecker;

    private void Update()
    {
        if (playerpos_changetime != 0)
            playerpos_changetime--;

        if (playerpos_changetime == 1)
        {
            if (Change_Scene == 1)
                player_col.gameObject.transform.position = new Vector3(-52, 1.5f, 165);
            if (Change_Scene == 2)
                player_col.gameObject.transform.position = new Vector3(156, 1.5f, 176);
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
                player_col = other;
                SceneChenger.instance.ChangeScene(Change_Scene + 1);
                playerpos_changetime = 34;
            }
        }
    }
}
