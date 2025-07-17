using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class kari : MonoBehaviour
{
    public Button button;

    private InputAction SkillAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SkillAction = InputSystem.actions.FindAction("Skill");
    }

    // Update is called once per frame
    void Update()
    {
        if(SkillAction.WasPressedThisFrame())
        {
            button.onClick.Invoke();
        }
    }
}
