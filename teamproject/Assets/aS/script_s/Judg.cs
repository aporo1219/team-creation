using UnityEngine;

public class Judg : MonoBehaviour
{
    [SerializeField] SceneChenger SC;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //�v���C���[���N���A�G���A�ɓ������烊�U���g��ʂɕς���
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            SceneChenger.instance.ChangeScene(5);
        }
    }
}
