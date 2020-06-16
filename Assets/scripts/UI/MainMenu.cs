using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public GameObject instructionMenu;
    public GameObject instructionButton, startButton, instructionBackButton;
    public GameObject startPointer, instructionPointer, instructionBackPointer, quitPointer;

    private GameObject previousSelection;
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
        previousSelection = startButton;
    }


    void Update()
    {
        GameObject currentSelection = EventSystem.current.currentSelectedGameObject;
        if (currentSelection != previousSelection && currentSelection != null) { 
            if (currentSelection == startButton) 
            {
                instructionBackPointer.SetActive(false);
                quitPointer.SetActive(false);
                instructionPointer.SetActive(false);
                startPointer.SetActive(true);
            } 
            else if (currentSelection == instructionButton)
            {
                instructionBackPointer.SetActive(false);
                quitPointer.SetActive(false);
                startPointer.SetActive(false);
                instructionPointer.SetActive(true);
            } 
            else if (currentSelection == instructionBackButton) 
            {
                quitPointer.SetActive(false);
                startPointer.SetActive(false);
                instructionPointer.SetActive(false);
                instructionBackPointer.SetActive(true);
            }
            else
            {
                instructionBackPointer.SetActive(false);
                startPointer.SetActive(false);
                instructionPointer.SetActive(false);
                quitPointer.SetActive(true);
            }
            previousSelection = currentSelection;
        } else if (currentSelection == null) {
            quitPointer.SetActive(false);
            startPointer.SetActive(false);
            instructionPointer.SetActive(false);
            instructionBackPointer.SetActive(false);
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)) 
            {
                if (instructionMenu.activeInHierarchy) 
                {
                    EventSystem.current.SetSelectedGameObject(instructionBackButton);
                    instructionBackPointer.SetActive(true);
                } 
                else 
                {
                EventSystem.current.SetSelectedGameObject(startButton);
                startPointer.SetActive(true);
                }
            }    
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
