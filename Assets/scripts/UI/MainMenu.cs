using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public GameObject instructionMenu;
    public GameObject instructionButton, startButton, instructionBackButton;

    private GameObject lastSelected;
    void Awake()
    {
       
        
    }
    private void Start()
    {
        instructionMenu.SetActive(false);
        Invoke("SetStart", 0.01f);
    }
    void SetStart() {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(startButton);
        lastSelected = startButton;
    }
    void SetBack() {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(instructionBackButton);
    }

    void Update()
    {

        GameObject current = EventSystem.current.currentSelectedGameObject;
        if (current != lastSelected && current != null)
        {
            lastSelected = current;
        }

        if (current == null && lastSelected&&lastSelected.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                EventSystem.current.SetSelectedGameObject(lastSelected);
            }
        }
    }

    public void OpenInstructionMenu() {
        instructionMenu.SetActive(true);
        Invoke("SetBack", 0.01f);

    }
    public void CloseInstructionMenu() {
        instructionMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(instructionButton);

    }
}
