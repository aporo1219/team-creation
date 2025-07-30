using UnityEngine;
using UnityEngine.UI;

public class DualEnemySpawnManager : MonoBehaviour
{
    public bool normal_enemy_flag = true;
    public bool wheel_enemy_flag = false;
    public bool fry_enemy_flag = false;

    int nspawn_num;
    int wspawn_num;
    int fspawn_num;

    public bool in_player = false;
    public bool dual_in_player = false;
    public bool do_spawn = false;

    int remove_player = 0;
    int wait_spawn = 600;

    public int spawn_time = 0;

    public float spawn_count = 0;
    public int spawn_max = 0;
    public int spawn_min = 0;

    public float death_num;

    public Vector3 maxspawn_pos;
    public Vector3 minspawn_pos;
    public Vector3 dualmaxspawn_pos;
    public Vector3 dualminspawn_pos;

    [SerializeField] GameObject spawn_obj;

    [SerializeField] GameObject normal_enemy;
    [SerializeField] GameObject wheel_enemy;
    [SerializeField] GameObject fry_enemy;

    [SerializeField] Slider Kill_Slider;

    [SerializeField] KillTaskSystem killtasksystem;

    private void OnDrawGizmos()
    {
        //ワイヤーフレームボックスの色
        Gizmos.color = Color.cyan;

        //ボックスの中心とサイズを計算
        Vector3 center = (minspawn_pos + maxspawn_pos) * 0.5f;
        Vector3 size = maxspawn_pos - minspawn_pos;
        Vector3 dualcenter = (dualminspawn_pos + dualmaxspawn_pos) * 0.5f;
        Vector3 dualsize = dualmaxspawn_pos - dualminspawn_pos;

        //ワイヤーフレームボックスを描画
        Gizmos.DrawWireCube(center, size);
        Gizmos.DrawWireCube(dualcenter, dualsize);
    }

    private void Update()
    {
        if (remove_player != 0) remove_player--;

        //プレイヤーが範囲に入ったら当たり判定を消して敵をスポーンさせる
        if (remove_player <= 0)
        {
            if (in_player)
            {
                if (!do_spawn)
                {
                    spawn_count = Random.Range(spawn_min, spawn_max);
                    if (killtasksystem != null)
                        killtasksystem.tasksystem.kill_enemy_num = spawn_count;
                    Spawn_Num();
                    for (int i = 0; i < nspawn_num; i++)
                    {
                        Normal_Spawn();
                    }
                    for (int i = 0; i < wspawn_num; i++)
                    {
                        Wheel_Spawn();
                    }
                    for (int i = 0; i < fspawn_num; i++)
                    {
                        Fry_Spawn();
                    }
                    do_spawn = true;
                    if (spawn_time > 0)
                        spawn_time--;
                }
            }
            if (dual_in_player)
            {
                if (!do_spawn)
                {
                    spawn_count = Random.Range(spawn_min, spawn_max);
                    Spawn_Num();
                    for (int i = 0; i < nspawn_num; i++)
                    {
                        Dual_Normal_Spawn();
                    }
                    for (int i = 0; i < wspawn_num; i++)
                    {
                        Dual_Wheel_Spawn();
                    }
                    for (int i = 0; i < fspawn_num; i++)
                    {
                        Dual_Fry_Spawn();
                    }
                    do_spawn = true;
                    if (spawn_time > 0)
                        spawn_time--;
                }
            }
            in_player = dual_in_player = false;
        }

        if (death_num != spawn_count && spawn_count != 0) remove_player = wait_spawn;

        //敵が全て倒されたら当たり判定を復活させてまた敵がスポーンできるようにする
        if (death_num == spawn_count && do_spawn)
        {
            do_spawn = false;
            spawn_count = death_num = 0;
            remove_player = wait_spawn;
            if (killtasksystem != null)
                killtasksystem.Next_Task();
        }

        if(spawn_count != 0 && killtasksystem != null)
        {
            killtasksystem.tasksystem.now_kill_num = death_num;
            Kill_Slider.value = (float)death_num / (float)spawn_count;
        }
    }

    void Normal_Spawn()
    {
        Vector3 pos = new Vector3(Random.Range(minspawn_pos.x, maxspawn_pos.x), Random.Range(minspawn_pos.y, maxspawn_pos.y), Random.Range(minspawn_pos.z, maxspawn_pos.z));

        if (spawn_time == -1 || spawn_time > 0)
        {
            Instantiate(normal_enemy, pos, Quaternion.identity, spawn_obj.transform);
            Debug.Log("通常敵召喚☆彡");
        }
    }

    void Wheel_Spawn()
    {
        Vector3 pos = new Vector3(Random.Range(minspawn_pos.x, maxspawn_pos.x), Random.Range(minspawn_pos.y, maxspawn_pos.y), Random.Range(minspawn_pos.z, maxspawn_pos.z));

        if (spawn_time == -1 || spawn_time > 0)
        {
            Instantiate(wheel_enemy, pos, Quaternion.identity, spawn_obj.transform);
            Debug.Log("車輪敵召喚☆彡");
        }
    }

    void Fry_Spawn()
    {
        Vector3 pos = new Vector3(Random.Range(minspawn_pos.x, maxspawn_pos.x), Random.Range(minspawn_pos.y, maxspawn_pos.y), Random.Range(minspawn_pos.z, maxspawn_pos.z));

        if (spawn_time == -1 || spawn_time > 0)
        {
            Instantiate(fry_enemy, pos, Quaternion.identity, spawn_obj.transform);
            Debug.Log("空中敵召喚☆彡");
        }
    }

    void Dual_Normal_Spawn()
    {
        Vector3 pos = new Vector3(Random.Range(dualminspawn_pos.x, dualmaxspawn_pos.x), Random.Range(dualminspawn_pos.y, dualmaxspawn_pos.y), Random.Range(dualminspawn_pos.z, dualmaxspawn_pos.z));

        if (spawn_time == -1 || spawn_time > 0)
        {
            Instantiate(normal_enemy, pos, Quaternion.identity, spawn_obj.transform);
            Debug.Log("通常敵召喚☆彡");
        }
    }

    void Dual_Wheel_Spawn()
    {
        Vector3 pos = new Vector3(Random.Range(dualminspawn_pos.x, dualmaxspawn_pos.x), Random.Range(dualminspawn_pos.y, dualmaxspawn_pos.y), Random.Range(dualminspawn_pos.z, dualmaxspawn_pos.z));

        if (spawn_time == -1 || spawn_time > 0)
        {
            Instantiate(wheel_enemy, pos, Quaternion.identity, spawn_obj.transform);
            Debug.Log("車輪敵召喚☆彡");
        }
    }

    void Dual_Fry_Spawn()
    {
        Vector3 pos = new Vector3(Random.Range(dualminspawn_pos.x, dualmaxspawn_pos.x), Random.Range(dualminspawn_pos.y, dualmaxspawn_pos.y), Random.Range(dualminspawn_pos.z, dualmaxspawn_pos.z));

        if (spawn_time == -1 || spawn_time > 0)
        {
            Instantiate(fry_enemy, pos, Quaternion.identity, spawn_obj.transform);
            Debug.Log("空中敵召喚☆彡");
        }
    }

    void Spawn_Num()
    {
        int max = (int)spawn_count;
        if (normal_enemy_flag)
        {
            if (wheel_enemy_flag || fry_enemy_flag)
            {
                nspawn_num = Random.Range(1, max);
                max -= nspawn_num;
            }
            else
            {
                nspawn_num = max;
            }
        }
        if (wheel_enemy_flag)
        {
            if (fry_enemy_flag)
            {
                wspawn_num = Random.Range(1, max);
                max -= wspawn_num;
            }
            else
            {
                wspawn_num = max;
            }
        }
        if (fry_enemy_flag)
        {
            fspawn_num = max;
        }
    }
}
