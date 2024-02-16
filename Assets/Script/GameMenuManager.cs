using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameMenuManager : MonoBehaviour
{
    public Transform head;
    public float spawnDistance = 4;
    public GameObject menu;
    public Button closeMenuButton;
    public Button menuButton;
    //public InputActionProperty showButton;

    // Start is called before the first frame update
    void Start()
    {
        HideMenu();
        if (closeMenuButton != null)
        {
            closeMenuButton.onClick.AddListener(HideMenu);
        }
        else
        {
            Debug.LogError("CloseMenuButton is not assigned in GameMenuManager.");
        }
        menuButton.onClick.AddListener(ToggleMenuVisibility);

    }

    // Update is called once per frame
    //void Update()
    //{
    //if (showButton.action.WasPressedThisFrame()|| )
    //{
    //menu.SetActive(!menu.activeSelf);
    //menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;


    //}

    //menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
    //menu.transform.forward *= -1;
    //}
    public void ToggleMenuVisibility()
    {
        menu.SetActive(!menu.activeSelf);
        UpdateMenuPosition();
      
    }

    void UpdateMenuPosition()
    {
        menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
        menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
        //menu.transform.forward *= -1;
        menu.transform.Rotate(0, 180, 0);
    }

    //void Update()
    //{
    //if (showButton.action.WasPressedThisFrame())
    //{
    //ToggleMenuVisibility();
    //}
    //}

    public void HideMenu()
    {
       menu.SetActive(false);
    }
}
