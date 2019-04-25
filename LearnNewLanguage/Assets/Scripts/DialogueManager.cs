using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : Singleton<DialogueManager>
{
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private DialogueElement currentDialogue = null;

    private void Start()
    {
        dialogueCanvas.SetActive(false);
        StartCoroutine(StartDialogueCoroutine());
    }

    IEnumerator StartDialogueCoroutine()
    {
        GameManager.Instance.InputEnabler(false);
        yield return new WaitForSeconds(2);
        StartDialogue(0);
        yield return new WaitForSeconds(1);
        FluffDialogueManager.Instance.ShowPoliceMan();
        FluffDialogueManager.Instance.PoliceManPlayIdle();
    }

    public void EndDialogue()
    {
        dialogueCanvas.SetActive(false);
    }

    public void StartDialogue(int dialogueID = 0)
    {
        dialogueCanvas.SetActive(true);
        DisplayDialogue(dialogueID);
    }

    private void DisplayDialogue(int dialogueID)
    {
        DialogueElement dialogueElement = DialogueFetcher.Instance.FetchDialogueByID(dialogueID);
        if(dialogueElement != null)
        {
            currentDialogue = dialogueElement;
            UpdateDialogueUI(currentDialogue);
            StartCoroutine(DisplayTimer());
        }
    }

    private void UpdateDialogueUI(DialogueElement dialogueElement)
    {
        dialogueText.text = currentDialogue.sentence;
    }

    private IEnumerator DisplayTimer()
    {
        yield return new WaitForSeconds(currentDialogue.displayTime);
        GameEvents.Instance.LaunchEvent(currentDialogue.eventName);
    }
}
