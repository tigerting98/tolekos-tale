using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        GameManager.gameInput.OnPressZ += OnNextLine;
        GameManager.gameInput.OnPressEnter += OnNextLine;


    }

    static void GetNextLine(Dialogue dialogue, Action OnFinishDialogue)
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
            OnFinishDialogue();

        }

    }

}
