using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using System.Runtime.CompilerServices;

public class Button_s : MonoBehaviour
{
    private Image TargetImage;
    private Button Button;
    private Color Default = Color.gray;
    private Color Hover = Color.yellow;
    private Color Choice = Color.black;
    private bool IsHover = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Button= GetComponent<Button>();

        if(Button != null)
        {
            TargetImage = Button.targetGraphic as Image;
        }
    }

    private void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if(EventSystem.current.currentSelectedGameObject == gameObject && !IsHover)
        {
            TargetImage.color = Choice;
        }
        else if(!IsHover)
        {
            TargetImage.color = Default;
        }
    }

    //ボタンを選択しているとき
    public void PointerPush(PointerEventData EventData)
    {
        IsHover = true;
        if(TargetImage != null)
        {
            TargetImage.color = Hover;
        }
    }

    //ボタンから離れた時
    public void PointerSeparate(PointerEventData EventData)
    {
        IsHover = false;
    }

   
}
