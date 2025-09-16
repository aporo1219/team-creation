using UnityEngine;

public class BossAction : MonoBehaviour
{
    [SerializeField] public BossStatus status;
    [SerializeField] BossAttack attack;
    [HideInInspector] public Animator bossanim;

    public Vector3 player_pos;

    //[Header("int�^�ϐ�")]
    int attack_time;

    //[Header("float�^�ϐ�")]
    float PB_length;//�v���C���[�ƃ{�X�̋���

    [Header("bool�^�ϐ�")]
    public bool near_player = false;                    //�v���C���[���{�X�̃T�[�`���ɂ��邩�ǂ���
    public bool can_move = true;                        //�ړ����Ă������ǂ���
    public bool now_attack = false;                     //�U�������ǂ���

    [Header("�萔")]
    [SerializeField] float MOVE_STOP_LENGTH;            //�v���C���[�܂ł̋߂Â��Ă悢����
    [SerializeField] float NEAR_ATTACK_LENGTH;          //�ߋ����U���������o���鋗��
    [SerializeField] public float MOVE_ROTATION_TIME;          //��]�ɂ����鎞��
    [SerializeField] int ATTACK_MIN_TIME;               //�U��������ŒZ�̎���
    [SerializeField] int ATTACK_MAX_TIME;               //�U��������ő�̎���

    //SE�֘A
    [SerializeField] AudioSource AS;
    [SerializeField] AudioClip FootSE;

    private bool CheckFoot = false;//������SE���d�Ȃ�Ȃ����߂̕ϐ�
    private int SEtimerFoot;//�����̃^�C��

    private void Start()
    {
        bossanim = GetComponentInChildren<Animator>();
        attack_time = Random.Range(ATTACK_MIN_TIME, ATTACK_MAX_TIME);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(now_attack);

        //�v���C���[���߂��ɂ���ꍇ
        if(near_player && !now_attack)
        {
            //�����]���ƈړ�
            {
                //�v���C���[�̕����։�]
                var direction = player_pos - gameObject.transform.position;
                direction.y = 0;

                var lookRotation = Quaternion.LookRotation(direction, Vector3.up);
                //gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, lookRotation, MOVE_ROTATION_TIME);

                //if(lookRotation <= 1)
                lookRotation = new Quaternion(lookRotation.x, lookRotation.y, lookRotation.z, lookRotation.w);

                gameObject.transform.rotation = lookRotation;

                //�v���C���[�̕����ֈړ�
                Vector3 velocity = gameObject.transform.forward * status.Speed;
                //SE
                if (!CheckFoot)
                {
                    AS.PlayOneShot(FootSE);
                    CheckFoot = true;
                    Invoke(nameof(FootCheckSE), 4);
                }

                //�v���C���[���牓���Ƌ߂Â�
                PB_length = Vector_Lnegth(gameObject.transform.position, player_pos);
                if (PB_length > MOVE_STOP_LENGTH)
                {
                    gameObject.transform.position += velocity * Time.deltaTime;
                    bossanim.SetFloat("MoveSpeed", 2.0f);
                }
                else
                {
                    bossanim.SetFloat("MoveSpeed", 0.0f);
                }
            }
            if (!now_attack)
            {
                //�U��
                {
                    if (attack_time != 0) attack_time--;

                    if (attack_time == 0)
                    {
                        //�v���C���[�ɋ߂���
                        if (PB_length <= NEAR_ATTACK_LENGTH)
                        {
                            //��]�U���Ɠːi�U��
                            Debug.Log("�ߋ����U��");
                            now_attack = true;
                            attack.Rush_Attack();
                            attack_time = Random.Range(ATTACK_MIN_TIME, ATTACK_MAX_TIME);
                        }
                        //�v���C���[���牓����
                        else
                        {
                            //�ːi�U���̂�
                            Debug.Log("�������U��");
                            now_attack = true;
                            attack.Rush_Attack();
                            attack_time = Random.Range(ATTACK_MIN_TIME, ATTACK_MAX_TIME);
                        }
                    }
                }
            }
        }
        else
        {
            bossanim.SetFloat("MoveSpeed", 0.0f);
        }
    }

    //�x�N�g���̒��������߂�֐�
    float Vector_Lnegth(Vector3 boss_pos, Vector3 player_pos)
    {
        Vector3 direction = player_pos - boss_pos;
        direction.y = 0;
        float length = direction.magnitude;
        return length;
    }

    //�����̃`�F�b�N
    private void FootCheckSE()
    {
        //���ԃ`�F�b�N
        CheckFoot = false;
    }
}
