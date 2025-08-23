using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [Header("定数")]
    [SerializeField] int CHARGE_TIME;
    [SerializeField] int RUSH_BOOST_SPEED;
    [SerializeField] int RUSH_TIME;
    [SerializeField] int FREEZE_TIME;

    BossAction action;
    [SerializeField] BossHitCheck hitcheck;
    [SerializeField] GameObject hitobj;
    Rigidbody rb;

    public int charge_time = 0;
    public int rush_time = 0;
    public int freeze_time = 0;

    float rush_length;

    bool now_charge = false;
    bool now_rush = false;
    bool now_freeze = false;

    Vector3 rush_direction;


    //SE
    [SerializeField] AudioSource AS;
    [SerializeField] AudioClip ChargeSE;//突進中SE
    [SerializeField] AudioClip AccumulateSE;//突進チャージSE

    private void Start()
    {
        action = GetComponent<BossAction>();

        rb = GetComponentInChildren<Rigidbody>();
    }

    private void Update()
    {
        if (!action.now_attack) hitcheck.wall_hit = false;
        //突進攻撃================================================
        //突進チャージ
        if (now_charge)
        {
            charge_time++;
            //溜め中にプレイヤーの方向へ身体を向ける
            var direction = action.player_pos - gameObject.transform.position;
            direction.y = 0;
            var lookRotation = Quaternion.LookRotation(direction, Vector3.up);
            gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, lookRotation, action.MOVE_ROTATION_TIME);
            //SEを流す
            AS.PlayOneShot(AccumulateSE);
        }
        //突進開始
        if (charge_time == CHARGE_TIME)
        {
            //突進アニメーション再生
            action.bossanim.SetFloat("MoveSpeed", 5.0f);
            //チャージ系変数の初期化
            charge_time = 0;
            now_charge = false;
            //突進する速度を保存
            rush_length = Speed_Lnegth(action.player_pos, gameObject.transform.position);
            hitobj.SetActive(true); 
            now_rush = true;
            //SEを流す
            AS.PlayOneShot(ChargeSE);
        }
        //突進中
        if (now_rush)
        {
            rush_time--;
            Vector3 velocity = gameObject.transform.forward * rush_length * RUSH_TIME;
            Vector3 target = gameObject.transform.position + velocity * RUSH_BOOST_SPEED;

            rb.linearVelocity = velocity;
            //if (!hitcheck.wall_hit)
            //{
            //    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target, RUSH_BOOST_SPEED);
            //}
            //else
            //{
            //    gameObject.transform.position -= velocity * RUSH_BOOST_SPEED * 2;
            //    hitcheck.wall_hit = false;
            //}
        }
        //突進硬直開始
        if(rush_time ==  0 && now_rush)
        {
            hitobj.SetActive(false);
            action.bossanim.SetFloat("MoveSpeed", 0.0f);
            now_freeze = true;
            now_rush = false;
        }
        //突進硬直中
        if (now_freeze) freeze_time++;
        //硬直終了
        if(freeze_time == FREEZE_TIME)
        {
            now_freeze = false;
            freeze_time = 0;
            action.now_attack = false;
        }

        //========================================================
    }

    //突進攻撃
    public void Rush_Attack()
    {
        //攻撃前の溜めを発生
        action.bossanim.SetFloat("MoveSpeed", -1.0f);
        rush_time = RUSH_TIME / RUSH_BOOST_SPEED;
        now_charge = true;
    }

    //突撃スピード
    float Speed_Lnegth(Vector3 boss_pos, Vector3 player_pos)
    {
        Vector3 direction = player_pos - boss_pos;
        direction.y = 0;
        float l = direction.magnitude / rush_time;
        return l;
    }

    //回転攻撃
    public void Roll_Attack()
    {

    }
}
