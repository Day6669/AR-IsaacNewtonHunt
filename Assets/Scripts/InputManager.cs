using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private TouchControls touchControls;
    private AppleDropper appleDropper;

    private void Awake()
    {
        touchControls = new TouchControls();
        appleDropper = GetComponent<AppleDropper>(); // Ensure this script is on the same GameObject as AppleDropper
    }

    private void OnEnable()
    {
        touchControls.Enable();
    }

    private void OnDisable()
    {
        touchControls.Disable();
    }

    void Start()
    {
        touchControls.Touch.TouchTap.started += ctx => StartTouch(ctx);
        touchControls.Touch.TouchTap.canceled += ctx => EndTouch(ctx);
    }

    private void StartTouch(InputAction.CallbackContext context)
    {
        // Call drop at the start of touch to register instantly 
        Debug.Log("Touch Started");
        appleDropper.DropNextApple(); // Call the DropNextApple method from AppleDropper

    }

    private void EndTouch(InputAction.CallbackContext context)
    {
        Debug.Log("Touch Ended");
    }
}

