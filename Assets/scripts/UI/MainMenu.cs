
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System;
//This controls the main menu of the game
public class MainMenu : MonoBehaviour
{
    public GameObject instructionMenu;
    public GameObject instructionButton, startButton, instructionBackButton, QuitButton, settingsButton, practiceButton;
    public GameObject difficultySelectionMenu, normalButton;
    public GameObject settingsMenu;
    public GameObject lastSelected;
    public GameObject creditMenu, creditBackButton, creditButton;
    public MusicTrack mainMenuMusic;
    public GameObject practiceModeMenu;
    public List<Button> practicemodeStagesButtons;
    public List<Button> difficultyButtons;
    Difficulty chosenPracticeModeDifficulty;
    GameObject practiceModepreviouslySelected;
    public GameObject clearMenu;
    public List<TextMeshProUGUI> clearobjects;
    SaveData data;
    void Awake()
    {

        data = SaveManager.LoadData();

    }
    //Set up the action listeners
    private void Start()
    {

        AudioManager.current.music.PlayTrack(mainMenuMusic);

        for (int i = 0; i < difficultyButtons.Count; i++)
        {
            int j = i;

            difficultyButtons[i].onClick.AddListener(() => StartGame(j, difficultyButtons[j].gameObject));
        }
        practicemodeStagesButtons[0].onClick.AddListener(() => StartPracticeGame(1, 0));
        practicemodeStagesButtons[1].onClick.AddListener(() => StartPracticeGame(5, 1));
        practicemodeStagesButtons[2].onClick.AddListener(() => StartPracticeGame(10, 2));
        practicemodeStagesButtons[3].onClick.AddListener(() => StartPracticeGame(16, 3));
        practicemodeStagesButtons[4].onClick.AddListener(() => StartPracticeGame(23, 4));
        practicemodeStagesButtons[5].onClick.AddListener(() => StartPracticeGame(28, 5));
        clearMenu.SetActive(false);
        instructionMenu.SetActive(false);
        difficultySelectionMenu.SetActive(false);
        creditMenu.SetActive(false);
        practiceModeMenu.SetActive(false);
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
    void SetCredit()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(creditBackButton);
    }
    public void StartGame(int i, GameObject chosen) {
        if (!GameManager.practiceMode)
        {


            GameManager.practiceMode = false;
            GameManager.sceneLoader.LoadStartGame(i);

        }
        else {
            chosenPracticeModeDifficulty = (Difficulty)i;
            practiceModepreviouslySelected = chosen;
            SelectStage();
        }
    }
    public void SelectStage() {
        practiceModeMenu.SetActive(true);
        Invoke("SelectStageDelay", 0.01f);
    }
    public void SelectStageDelay()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(practicemodeStagesButtons[0].gameObject);
        practiceModepreviouslySelected.GetComponent<ButtonPointer>().OnSelect(null);
        for (int i = 0; i < 6; i++) {
            try
            {
                TextMeshProUGUI text = practicemodeStagesButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                Color z = text.color;
                if (data.acceessible[(int)chosenPracticeModeDifficulty, i])
                {
                    z.a = 1f;
                }
                else
                {
                    z.a = 0.3f;
                }
                text.color = z;
            }
            catch (Exception ex) {
                Debug.Log(ex);
            }
            }
    }
    public void StartPracticeGame(int level, int stage) {
        if (data.acceessible[(int)chosenPracticeModeDifficulty, stage])
        {
            GameManager.practiceMode = true;
            GameManager.difficultyLevel = chosenPracticeModeDifficulty;
            PlayerStats.LevelUpBeforeHand(level);
            PlayerStats.gold = 99999;
            GameManager.sceneLoader.LoadShopScene(GameManager.gameData.levels[stage]);
        }

    }
    public void CloseStageSelection() {
        practiceModeMenu.SetActive(false);
        lastSelected.GetComponent<ButtonPointer>().OnDeselect(null);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(practiceModepreviouslySelected);
    }
    public void SetPracticeMode(bool bo) {
        GameManager.practiceMode = bo;
        if (!bo) {
            clearMenu.SetActive(true);
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    Color c = clearobjects[i].color;
                    if (data.cleared[i])
                    {
                        c.a = 1f;
                    }
                    else
                    {
                        c.a = 0.1f;
                    }
                    clearobjects[i].color = c;
                }
                catch (Exception ex) {
                    Debug.Log(ex);
                }
                
            }
        }
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
                if (practiceModeMenu.activeInHierarchy)
                {
                    CloseStageSelection();
                }
                else
                {
                    CloseDifficultySelection();
                }
            }
            else if (lastSelected == instructionBackButton)
            {
                CloseInstructionMenu();
            }
            else if (lastSelected == creditBackButton)
            {
                CloseCreditMenu();
            }
            else {
                SetToQuitButton();
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && difficultySelectionMenu.activeInHierarchy) {
            if (practiceModeMenu.activeInHierarchy)
            {
                CloseStageSelection();
            }
            else
            {
                CloseDifficultySelection();
            }
        }
    }

    public void SelectDifficulty() {
        difficultySelectionMenu.SetActive(true);
        Invoke("SelectDifficultyDelay", 0.01f);
    }
    public void SelectDifficultyDelay() {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(difficultyButtons[2].gameObject);
    }
    public void CloseDifficultySelection() {
        clearMenu.SetActive(false);
        difficultySelectionMenu.SetActive(false);
        lastSelected.GetComponent<ButtonPointer>().OnDeselect(null);
        EventSystem.current.SetSelectedGameObject(null);
        if (GameManager.practiceMode)
        {
            EventSystem.current.SetSelectedGameObject(practiceButton);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(startButton);
        }
    }
    public void SetToQuitButton() {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(QuitButton);
    }

    public void OpenInstructionMenu() {
        instructionMenu.SetActive(true);
        Invoke("SetBack", 0.01f);

    }
    public void OpenCreditMenu() {
        creditMenu.SetActive(true);
        Invoke("SetCredit", 0.01f);
    }
    public void OpenSettingsMenu() {
        settingsMenu.SetActive(true);
    }
    public void CloseSettingsMenu() {
        settingsMenu.GetComponent<SettingMenu>().backButton.GetComponent<ButtonPointer>().OnDeselect(null);
        settingsMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(settingsButton);
    }
    public void CloseInstructionMenu() {
        instructionMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(instructionButton);

    }
    public void CloseCreditMenu()
    {
        creditMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(creditButton);

    }
}
