using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WristUI : MonoBehaviour
{
    public InputActionAsset inputAction;

    private Canvas wristUICanvas;
    private InputAction streetMainMenu;

    // Start is called before the first frame update
    private void Start()
    {
        wristUICanvas = GetComponent<Canvas>();
        streetMainMenu = inputAction.FindActionMap("XRI LeftHand").FindAction("streetMainMenu");
        streetMainMenu.Enable();
        streetMainMenu.performed += ToggleMainMenu;

    }

    private void OnDestroy()
    {
        streetMainMenu.performed -= ToggleMainMenu;
    }

    public void ToggleMainMenu(InputAction.CallbackContext context)
    {
        wristUICanvas.enabled = !wristUICanvas.enabled;

    }
}
