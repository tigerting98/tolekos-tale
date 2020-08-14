using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this class is responsible for the dialogue system in the game
public class DialogueManager : MonoBehaviour
{
    public static int currentDialogueLine;
    public static Action OnNextLine;
     public static IEnumerator StartDialogue(Dialogue dialogue, Action OnFinishDialogue)
    {
        GameManager.dialogueUI.SetActive();
        currentDialogueLine = 0;
        OnNextLine = () => GetNextLine(dialogue, OnFinishDialogue);
        OnNextLine();
        yield return new WaitForSeconds(0.3f);
        if (GameManager.player)
        {
            GameManager.player.shootingEnabled = false;
        }
        GameManager.gameInput.OnPressZ += OnNextLine;
        GameManager.gameInput.OnPressEnter += OnNextLine;


    }

    static void GetNextLine(Dialogue dialogue, Action OnFinishDialogue)
    {
        if (GameManager.dialogueUI.typing)
        {
            GameManager.dialogueUI.FinishLine();
        }
        else
        {
            if (currentDialogueLine < dialogue.lines.Count)
            {
                GameManager.dialogueUI.ReceiveNewLine(dialogue.lines[currentDialogueLine]);
                currentDialogueLine++;
            }
            else
            {

                GameManager.dialogueUI.SetInactive();
                GameManager.gameInput.OnPressZ -= OnNextLine;
                GameManager.gameInput.OnPressEnter -= OnNextLine;
                if (GameManager.player)
                {
                    GameManager.player.shootingEnabled = true;
                }
                OnFinishDialogue();

            }
        }

    }

}
