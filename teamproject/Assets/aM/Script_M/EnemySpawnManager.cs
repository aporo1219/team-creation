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
        //���C���[�t���[���{�b�N�X�̐F
        Gizmos.color = Color.cyan;

        //�{�b�N�X�̒��S�ƃT�C�Y���v�Z
        Vector3 center = (minspawn_pos + maxspawn_pos) * 0.5f;
        Vector3 size = maxspawn_pos - minspawn_pos;

        //���C���[�t���[���{�b�N�X��`��
        Gizmos.DrawWireCube(center, size);
    }

    private void Update()
    {
        //�v���C���[���͈͂ɓ������瓖���蔻��������ēG���X�|�[��������
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

        //�G���S�ē|���ꂽ�瓖���蔻��𕜊������Ă܂��G���X�|�[���ł���悤�ɂ���
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
            Debug.Log("�G�������c");
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
