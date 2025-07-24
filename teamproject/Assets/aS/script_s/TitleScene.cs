using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TitleScene : MonoBehaviour
{
    [SerializeField] Button Button;
    [SerializeField] GameObject GB;

    private Gamepad Gp;
    private GameObject LB;

    private Image TargetImage;
    private Color Push = Color.yellow;
    private Color Default = Color.white;
    private bool IsHover = false;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        GameObject Selected = EventSystem.current.currentSelectedGameObject;
        GameObject CrossKey = EventSystem.current.currentSelectedGameObject;

        if (EventSystem.current.currentSelectedGameObject == gameObject && !IsHover)
        {
            TargetImage.color = Push;
        }
        else if (!IsHover)
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
            if (LB!= null)
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
        SceneChenger.instance.ChangeScene(1);
    }
}
