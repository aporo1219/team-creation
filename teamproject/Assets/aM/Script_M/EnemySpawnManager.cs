using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    bool in_player = false;
    public bool do_spawn = false;

    public int spawn_time = 0;

    public int spawn_count = 0;
    public int spawn_max = 0;
    public int spawn_min = 0;

    public int death_num;

    public Vector3 maxspawn_pos;
    public Vector3 minspawn_pos;

    [SerializeField] BoxCollider boxcol;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject spawn_obj;

    Vector3 spawn_obj_pos;

    private void Start()
    {
        spawn_obj_pos = spawn_obj.transform.position;
    }

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

    private void Update()
    {
        //プレイヤーが範囲に入ったら当たり判定を消して敵をスポーンさせる
        if (in_player)
        {
            in_player = false;
            if (!do_spawn)
            {
                spawn_count = Random.Range(spawn_min, spawn_max);
                for (int i = 0; i < spawn_count; i++)
                {
                    Spawn();
                }
                do_spawn = true;
                if (spawn_time > 0)
                    spawn_time--;
            }
            boxcol.enabled = false;
        }

        //敵が全て倒されたら当たり判定を復活させてまた敵がスポーンできるようにする
        if(death_num == spawn_count)
        {
            boxcol.enabled = true;
            do_spawn = false;
        }
    }

    void Spawn()
    {
        Vector3 pos = new Vector3(Random.Range(minspawn_pos.x, maxspawn_pos.x), Random.Range(minspawn_pos.y, maxspawn_pos.y), Random.Range(minspawn_pos.z, maxspawn_pos.z));

        if(spawn_time == -1)
        {
            Instantiate(enemy, pos, Quaternion.identity, spawn_obj.transform);
            Debug.Log("敵召喚☆彡");
        }
        else if(spawn_time > 0)
        {
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player" && !do_spawn)
        {
            in_player = true;
        }
    }
}
