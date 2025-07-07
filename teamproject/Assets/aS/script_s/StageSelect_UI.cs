using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;


public class StageSelectUI : MonoBehaviour
{
    private int Level_Floor = 1;
    private Gamepad GP;
    private int Button_Num;

    [SerializeField] Text Floor_Display;
    [SerializeField] GameObject Button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         FloorDisplay();

      
        Button_Num = 5;

        //Button.SetActive(false);
        //�R���g���[����UI�{�^���̕R�Â�
        EventSystem.current.SetSelectedGameObject(Button);
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }
    }

    private void FixedUpdate()
    {
        //�Q�[���p�b�h�̓���
        GP = Gamepad.current;
        if(GP == null )
        {
            return;
        }
        Vector2 Right_Stick = GP.rightStick.ReadValue();


    }

    public void FloorSet(int floor)
    {
        Level_Floor = floor;
        FloorDisplay();
    }

    void FloorDisplay()
    {
        Floor_Display.text =  "BF" + Level_Floor ; 
    }
}
