using UnityEngine;

public class SkillCardController_y : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]private GameObject SkillObj;

    public float BouncePower;
    public float RotateSpeed;

    //地上レイヤー
    LayerMask layerMask;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //レイヤーマスクにグラウンドレイヤー設定
        layerMask = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Physics.SphereCast(rb.position, 0.5f, Vector3.down, out RaycastHit h, transform.localScale.y / 2, layerMask))
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, BouncePower, rb.linearVelocity.z);
        }

        transform.Rotate(new Vector3(0.0f, RotateSpeed, 0.0f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            PlayerController_y1 player = other.gameObject.GetComponent<PlayerController_y1>();

            player.SkillCont.AddSkill(SkillObj);

            Destroy(gameObject);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(rb.position.x, rb.position.y - transform.localScale.y / 2, rb.position.z), 0.5f);
    }
}
