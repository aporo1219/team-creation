using UnityEngine;
using UnityEngine.UI;

public class StageSelectUI : MonoBehaviour
{
    private int Level_Foor = 1;

    [SerializeField] Text Floor_Display;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         FloorDisplay();
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
        //Floor_Display.text = "Å™BF";
    }

    public void FloorSet(int floor)
    {
        Level_Foor = floor;
        FloorDisplay();
    }

    void FloorDisplay()
    {
        Floor_Display.text =  "BF" + Level_Foor ;
    }
}
