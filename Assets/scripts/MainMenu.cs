using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public GameObject instructionMenu;
    public GameObject instructionButton, startButton, instructionBackButton;
    public GameObject startPointer, instructionPointer, quitPointer;
    void Awake()
    {
        instructionMenu.SetActive(false);
        instructionPointer.SetActive(false);
        quitPointer.SetActive(false);
        
    }
    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        startPointer.SetActive(true);
        EventSystem.current.SetSelectedGameObject(startButton);
    }


    void Update()
    {
        GameObject currentSelection = EventSystem.current.currentSelectedGameObject;
        if (currentSelection == startButton) 
        {
            quitPointer.SetActive(false);
            instructionPointer.SetActive(false);
            startPointer.SetActive(true);
        } 
        else if (currentSelection == instructionButton || currentSelection == instructionBackButton)
        {
            quitPointer.SetActive(false);
            startPointer.SetActive(false);
            instructionPointer.SetActive(true);
        } 
        else
        {
            startPointer.SetActive(false);
            instructionPointer.SetActive(false);
            quitPointer.SetActive(true);
        }
        
    }
    public void OpenInstructionMenu() {
        instructionMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(instructionBackButton);

    }
    public void CloseInstructionMenu() {
        instructionMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(instructionButton);

    }
}
