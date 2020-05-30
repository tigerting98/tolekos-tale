using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public GameObject instructionMenu;
    public GameObject instructionButton, startButton, instructionBackButton;
    void Awake()
    {
        instructionMenu.SetActive(false);
        
        
    }
    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(startButton);
    }


    void Update()
    {
        
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
