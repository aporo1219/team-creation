using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms.Impl;

public class ClearScene : MonoBehaviour
{
    [SerializeField] Button Button;
    [SerializeField] GameObject GB;
    [SerializeField] Text Result;
    [SerializeField] AudioSource AS;
    [SerializeField] AudioClip Push_Button;

    private Gamepad Gp;
    private GameObject LB;

    private Image TargetImage;
    private Color Push;
    private Color Default;
    private bool IsHover = false;
    private int Isscore;//スコアの変数

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //コントローラとUIボタンの紐づけ
        EventSystem.current.SetSelectedGameObject(GB);
        LB = GB;
        Button.onClick.AddListener(() => OnButtonPressed());
        if (Button != null)
        {
            TargetImage = Button.targetGraphic as Image;
        }

        Display_TEXT();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        GameObject Selected = EventSystem.current.currentSelectedGameObject;
        GameObject CrossKey = EventSystem.current.currentSelectedGameObject;

        //押されたときに色変更
        //if()
        if (Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            Default = TargetImage.color;
            TargetImage.color = Color.yellow;
        }

        //デフォルト
        if (Gamepad.current.buttonSouth.wasReleasedThisFrame)
        {
            TargetImage.color = Default;
        }
        //ゲームパッドの入力
        Gp = Gamepad.current;
        if (Gp == null)
        {
            return;
        }

        // 現在の選択が無効 or null なら復帰
        if (Selected == null)
        {
            if (LB != null)
            {
                EventSystem.current.SetSelectedGameObject(LB);
            }
            else
            {
                // 最後の選択が未設定なら First_Button に戻す
                EventSystem.current.SetSelectedGameObject(GB);
            }
        }
        else
        {
            // 有効なUIボタンが選ばれていれば記録しておく
            LB = Selected;
        }
    }

    void OnButtonPressed()
    {
        AS.PlayOneShot(Push_Button);
        SceneChenger.instance.ChangeScene(0);
    }

    //スコア計算関数
    void CountScore()
    {

    }

    //テキスト表示関数
    void Display_TEXT()
    {
        Result.text = "スコアは :" + Isscore;
    }
}
