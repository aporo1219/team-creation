using UnityEngine;
using UnityEngine.UI;

public class FreeEnemySpawnManager : MonoBehaviour
{
    public bool normal_enemy_flag = true;
    public bool wheel_enemy_flag = false;
    public bool fry_enemy_flag = false;

    int nspawn_num;
    int wspawn_num;
    int fspawn_num;

    int remove_player = 0;
    int wait_spawn = 600;

    int spawn_num;

    public float spawn_count = 0;
    public int spawn_max = 0;

    public float death_num;

    public Vector3 maxspawn_pos;
    public Vector3 minspawn_pos;

    [SerializeField] GameObject spawn_obj;

    [SerializeField] GameObject normal_enemy;
    [SerializeField] GameObject wheel_enemy;
    [SerializeField] GameObject fry_enemy;

    GameObject score_obj;
    ScoreManager scoremanager;

    private void OnDrawGizmos()
    {
        //ワイヤーフレームボックスの色
        Gizmos.color = Color.cyan;

        //ボックスの中心とサイズを計算
        Vector3 center = (minspawn_pos + maxspawn_pos) * 0.5f;
        Vector3 size = maxspawn_pos - minspawn_pos;

        //ワイヤーフレームボックスを描画
        Gizmos.DrawWireCube(center, size);
    }

    // Update is called once per frame
    void Update()
    {
        //spawn_maxまで敵を召喚する
        if (spawn_count < spawn_max)
        {
            spawn_num = Spawn_Num();
            switch (spawn_num)
            {
                case 0:
                    Normal_Spawn();
                    break;
                case 1:
                    Wheel_Spawn();
                    break;
                case 2:
                    Fry_Spawn();
                    break;
            }
            if (spawn_num >= 0)
            {
                spawn_count++;
                spawn_num = -1;
            }
        }
        //敵が倒されたらspawn_countを倒された分減らす
        if (death_num > 0)
        {
            score_obj = GameObject.Find("Score");
            if (score_obj != null)
                scoremanager = score_obj.GetComponent<ScoreManager>();

            if (scoremanager != null)
            {
                //キル数を増やしてdeath_numをkill_pulsで増やした分だけ減らす
                scoremanager.kill_puls++;
                death_num--;
                
            }
        }
    }

    void Normal_Spawn()
    {
        Vector3 pos = new Vector3(Random.Range(minspawn_pos.x, maxspawn_pos.x), Random.Range(minspawn_pos.y, maxspawn_pos.y), Random.Range(minspawn_pos.z, maxspawn_pos.z));

        Instantiate(normal_enemy, pos, Quaternion.identity, spawn_obj.transform);
        Debug.Log("通常敵召喚☆彡");

    }

    void Wheel_Spawn()
    {
        Vector3 pos = new Vector3(Random.Range(minspawn_pos.x, maxspawn_pos.x), Random.Range(minspawn_pos.y, maxspawn_pos.y), Random.Range(minspawn_pos.z, maxspawn_pos.z));

        Instantiate(wheel_enemy, pos, Quaternion.identity, spawn_obj.transform);
        Debug.Log("車輪敵召喚☆彡");

    }

    void Fry_Spawn()
    {
        Vector3 pos = new Vector3(Random.Range(minspawn_pos.x, maxspawn_pos.x), Random.Range(minspawn_pos.y, maxspawn_pos.y), Random.Range(minspawn_pos.z, maxspawn_pos.z));

        Instantiate(fry_enemy, pos, Quaternion.identity, spawn_obj.transform);
        Debug.Log("空中敵召喚☆彡");

    }

    int Spawn_Num()
    {
        if (normal_enemy_flag)
        {
            if (wheel_enemy_flag || fry_enemy_flag)
            {
                if (Random.Range(0, 2) == 0)
                    return 0;
            }
            else return 0;
        }
        if (wheel_enemy_flag)
        {
            if (fry_enemy_flag)
            {
                if (Random.Range(0, 2) == 0)
                    return 1;
            }
            else return 1;
        }
        if (fry_enemy_flag)
        {
            return 2;
        }

        return -1;
    }
}
