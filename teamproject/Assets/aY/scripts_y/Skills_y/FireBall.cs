using System.Collections;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public PlayerController_y1 PlayerCont;

    Rigidbody rb;
    Vector3 moveForward;

    public GameObject Ball;
    public GameObject Explosion;

    int LifeTimeLimit = 120;
    int LifeTime = 0;

    float MoveSpeed = 20.0f;
    float ExplosionTime = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        PlayerCont = FindAnyObjectByType<PlayerController_y1>();

        Explosion.SetActive(false);

        moveForward = PlayerCont.transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        ////カメラの方向からX-Z平面の単位ベクトルを取得
        //Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 1, 1)).normalized;
        ////カメラの向きから移動方向を決定
        //moveForward = cameraForward * 1 + Camera.main.transform.right * 0;
        ////
        moveForward.Normalize();

        rb.linearVelocity = moveForward * MoveSpeed;

        //キャラクターの向きを進行方向に
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
    }

    private void FixedUpdate()
    {
        LifeTime++;

        if(LifeTime == LifeTimeLimit)
            StartCoroutine(HitExplosion());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag!="Player"&&collision.gameObject.tag!="PlayerSkill")
        StartCoroutine(HitExplosion());
    }

    private IEnumerator HitExplosion()
    {
        Ball.SetActive(false);
        Explosion.SetActive(true);

        MoveSpeed = 0.0f;

        for(float time = 0.0f;time<ExplosionTime;time+=Time.deltaTime)
        {
            yield return null;
        }

        Destroy(gameObject);

        yield return null;
    }
}
