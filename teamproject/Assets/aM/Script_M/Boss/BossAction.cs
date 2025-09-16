using UnityEngine;

public class BossAction : MonoBehaviour
{
    [SerializeField] public BossStatus status;
    [SerializeField] BossAttack attack;
    [HideInInspector] public Animator bossanim;

    public Vector3 player_pos;

    //[Header("int型変数")]
    int attack_time;

    //[Header("float型変数")]
    float PB_length;//プレイヤーとボスの距離

    [Header("bool型変数")]
    public bool near_player = false;                    //プレイヤーがボスのサーチ内にいるかどうか
    public bool can_move = true;                        //移動していいかどうか
    public bool now_attack = false;                     //攻撃中かどうか

    [Header("定数")]
    [SerializeField] float MOVE_STOP_LENGTH;            //プレイヤーまでの近づいてよい距離
    [SerializeField] float NEAR_ATTACK_LENGTH;          //近距離攻撃が発動出来る距離
    [SerializeField] public float MOVE_ROTATION_TIME;          //回転にかける時間
    [SerializeField] int ATTACK_MIN_TIME;               //攻撃をする最短の時間
    [SerializeField] int ATTACK_MAX_TIME;               //攻撃をする最大の時間

    //SE関連
    [SerializeField] AudioSource AS;
    [SerializeField] AudioClip FootSE;

    private bool CheckFoot = false;//足音のSEが重ならないための変数
    private int SEtimerFoot;//足音のタイム

    private void Start()
    {
        bossanim = GetComponentInChildren<Animator>();
        attack_time = Random.Range(ATTACK_MIN_TIME, ATTACK_MAX_TIME);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(now_attack);

        //プレイヤーが近くにいる場合
        if(near_player && !now_attack)
        {
            //方向転換と移動
            {
                //プレイヤーの方向へ回転
                var direction = player_pos - gameObject.transform.position;
                direction.y = 0;

                var lookRotation = Quaternion.LookRotation(direction, Vector3.up);
                //gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, lookRotation, MOVE_ROTATION_TIME);

                //if(lookRotation <= 1)
                lookRotation = new Quaternion(lookRotation.x, lookRotation.y, lookRotation.z, lookRotation.w);

                gameObject.transform.rotation = lookRotation;

                //プレイヤーの方向へ移動
                Vector3 velocity = gameObject.transform.forward * status.Speed;
                //SE
                if (!CheckFoot)
                {
                    AS.PlayOneShot(FootSE);
                    CheckFoot = true;
                    Invoke(nameof(FootCheckSE), 4);
                }

                //プレイヤーから遠いと近づく
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
                //攻撃
                {
                    if (attack_time != 0) attack_time--;

                    if (attack_time == 0)
                    {
                        //プレイヤーに近い時
                        if (PB_length <= NEAR_ATTACK_LENGTH)
                        {
                            //回転攻撃と突進攻撃
                            Debug.Log("近距離攻撃");
                            now_attack = true;
                            attack.Rush_Attack();
                            attack_time = Random.Range(ATTACK_MIN_TIME, ATTACK_MAX_TIME);
                        }
                        //プレイヤーから遠い時
                        else
                        {
                            //突進攻撃のみ
                            Debug.Log("遠距離攻撃");
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

    //ベクトルの長さを求める関数
    float Vector_Lnegth(Vector3 boss_pos, Vector3 player_pos)
    {
        Vector3 direction = player_pos - boss_pos;
        direction.y = 0;
        float length = direction.magnitude;
        return length;
    }

    //足音のチェック
    private void FootCheckSE()
    {
        //時間チェック
        CheckFoot = false;
    }
}
