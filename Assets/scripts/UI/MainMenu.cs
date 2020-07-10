using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public GameObject instructionMenu;
    public GameObject instructionButton, startButton, instructionBackButton, QuitButton;
    public GameObject difficultySelectionMenu, normalButton;
    private GameObject lastSelected;
    void Awake()
    {
       
        
    }
    private void Start()
    {
        instructionMenu.SetActive(false);
        difficultySelectionMenu.SetActive(false);
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
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (difficultySelectionMenu.activeInHierarchy) {
                CloseDifficultySelection();
            }
            else if (lastSelected == instructionBackButton)
            {
                CloseInstructionMenu();
            }
            else {
                SetToQuitButton();
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && difficultySelectionMenu.activeInHierarchy) {
            CloseDifficultySelection();
        }
    }

    public void SelectDifficulty() {
        difficultySelectionMenu.SetActive(true);
        Invoke("SelectDifficultyDelay", 0.01f);
    }
    public void SelectDifficultyDelay() {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(normalButton);
    }
    public void CloseDifficultySelection() {
        difficultySelectionMenu.SetActive(false);
        lastSelected.GetComponent<ButtonPointer>().OnDeselect(null);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(startButton);
    }
    public void SetToQuitButton() {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(QuitButton);
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
