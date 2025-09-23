using UnityEngine;

public class ChangeBGM : MonoBehaviour
{

    private int ChangeTime = 2;
    private bool BGM = false;

    [SerializeField] AudioSource ASBefore;
    [SerializeField] AudioSource ASAfter;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //一個のBGMをミュートにする
        ASAfter.mute = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag == "Player")
        {
            //プレイヤーが入ったら流れていたBGMをミュートにし、別のＢＧＭを流す
            Debug.Log("入った");
            ASBefore.mute = true;
            BGM = true;
            //2秒後にBGM切り替え   
            Invoke(nameof(Change), ChangeTime);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        //if (other.gameObject.tag == "Player")
        //{
        //    //プレイヤーが出たら流れていたBGMをミュートにし、別のＢＧＭを流す
        //    Debug.Log("出た");
        //    ASAfter.mute = true;
        //    BGM = false;
        //    //2秒後にBGM切り替え   
        //    Invoke(nameof(Change), ChangeTime);
        //}
    }

    private void Change()
    {
        //afterのBGMをtrueにする
        if (BGM)
        {
            ASAfter.mute = false;
            ASAfter.Play();
        }
        else
        {
            ASBefore.mute = false;
            ASBefore.Play();
        }
    }
}
