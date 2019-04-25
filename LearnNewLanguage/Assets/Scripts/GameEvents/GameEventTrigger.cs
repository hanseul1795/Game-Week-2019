using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType
{
    Dialogue,
    MultiChoice,
    Puzzle
}

public class GameEventTrigger : MonoBehaviour
{
    [SerializeField] private EventType eventType;
    [SerializeField] private int eventID;
    [SerializeField] private Speakers speaker;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")    
        {
            switch(eventType)
            {
                case EventType.Dialogue:
                    DialogueManager.Instance.StartDialogue(eventID);
                    break;
                case EventType.MultiChoice:
                    MultipleChoiceGame.Instance.StartGame(eventID);
                    break;
                case EventType.Puzzle:
                    WordPuzzleGame.Instance.StartGame(eventID);
                    break;
            }

            FluffDialogueManager.Instance.ShowSpeaker(speaker);
            GameManager.Instance.InputEnabler(false);
            gameObject.SetActive(false);
        }
    }
}
