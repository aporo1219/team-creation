using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    bool in_player = false;
    bool do_spawn = false;

    public int spawn_time = 0;

    public Vector3 maxspawn_pos;
    public Vector3 minspawn_pos;

    [SerializeField] BoxCollider boxcol;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject spawn_obj;

    Vector3 spawn_obj_pos;

    int spawn_num = 0;
    int spawn_max = 5;
    int spawn_min = 7;

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
            if (!do_spawn)
            {
                spawn_num = Random.Range(spawn_min, spawn_max);
                for (int i = 0; i < spawn_num; i++)
                {
                    Spawn();
                }
                do_spawn = true;
                spawn_time--;
            }
            boxcol.enabled = false;
        }

        //敵が全て倒されたら当たり判定を復活させてまた敵がスポーンできるようにする
        //boxcol.enabled = true;
        //do_spawn = false;
    }

    void Spawn()
    {
        if(spawn_time == -1)
        {

        }
        else if(spawn_time > 0)
        {
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            in_player = true;
        }
    }
}
