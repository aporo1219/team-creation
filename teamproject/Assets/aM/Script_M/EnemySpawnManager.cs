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

        //�G���S�ē|���ꂽ�瓖���蔻��𕜊������Ă܂��G���X�|�[���ł���悤�ɂ���
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
