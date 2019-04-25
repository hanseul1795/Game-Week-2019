using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class GameEvents : Singleton<GameEvents>
{

    public void LaunchEvent(string eventName)
    {
        Type t = this.GetType();
        MethodInfo method = t.GetMethod(eventName);
        if(method != null)
            method.Invoke(this, null);
        else
            Debug.Log("Event " + eventName + " Doesnt Exist");
    }
    IEnumerator EndGameCoroutine(bool p_victory)
    {
        yield return new WaitForSeconds(5);
        if (p_victory)
            GameManager.Instance.Victory();
        else
            GameManager.Instance.GameOver();
    }
    public void Police1()
    {
        DialogueManager.Instance.EndDialogue();
        MultipleChoiceGame.Instance.StartGame(0);
    }

    public void Police2()
    {
        DialogueManager.Instance.EndDialogue();
        MultipleChoiceGame.Instance.StartGame(1);
    }

    public void LearnTravel()
    {
        Dictionary.Instance.AddWordsManually("Travel", "여행");
        MultipleChoiceGame.Instance.EndGame();
        WordPuzzleGame.Instance.StartGame(2);
    }

    public void Boss1()
    {
        EndDialogue();
        MultipleChoiceGame.Instance.StartGame(4);
    }

    public void Boss2()
    {
        EndDialogue();
        MultipleChoiceGame.Instance.StartGame(5);
    }

    public void LearnRight()
    {
        //Debug.Log("learn Right");
        FluffDialogueManager.Instance.Correct();
        Dictionary.Instance.AddWordsManually("Right", "오른쪽");
        EndMultipleGame(false);
        DialogueManager.Instance.StartDialogue(24);

    }

    public void LearnLeft()
    {
        Debug.Log("learn Left");
        FluffDialogueManager.Instance.Wrong();
        Dictionary.Instance.AddWordsManually("Left", "왼쪽");
        EndMultipleGame(false);
        DialogueManager.Instance.StartDialogue(25);
    }

    public void LearnUp()
    {
        Debug.Log("learn Up");
        FluffDialogueManager.Instance.Wrong();
        Dictionary.Instance.AddWordsManually("Up", "위쪽");
        EndMultipleGame(false);
        DialogueManager.Instance.StartDialogue(25);
    }

    public void LearnDown()
    {
        Debug.Log("learn Down");
        FluffDialogueManager.Instance.Wrong();
        Dictionary.Instance.AddWordsManually("Down", "아래쪽");
        EndMultipleGame(false);
        DialogueManager.Instance.StartDialogue(25);
    }

    public void LearnRed()
    {
        Debug.Log("learn Red");
        FluffDialogueManager.Instance.Correct();
        Dictionary.Instance.AddWordsManually("Red", "빨강색");
        EndMultipleGame();
        //StartCoroutine(ReAfficheBoss());
        DialogueManager.Instance.StartDialogue(26);
        FluffDialogueManager.Instance.HideCurrentSpeaker();
        GameManager.Instance.InputEnabler(true);
        StartCoroutine(EndGameCoroutine(true));
    }

    public IEnumerator ReAfficheBoss()
    {
        yield return new WaitForSeconds(3);
        FluffDialogueManager.Instance.ShowBoss();
    }

    public void LearnBlue()
    {
        Debug.Log("learn Blue");
        FluffDialogueManager.Instance.Wrong();
        Dictionary.Instance.AddWordsManually("Blue", "파랑색");
        EndMultipleGame();
        DialogueManager.Instance.StartDialogue(27);
        FluffDialogueManager.Instance.HideCurrentSpeaker();
        GameManager.Instance.InputEnabler(true);
        StartCoroutine(EndGameCoroutine(true));
    }

    public void LearnGreen()
    {
        Debug.Log("learn Green");
        FluffDialogueManager.Instance.Wrong();
        Dictionary.Instance.AddWordsManually("Green", "녹색");
        EndMultipleGame();
        DialogueManager.Instance.StartDialogue(27);
        FluffDialogueManager.Instance.HideCurrentSpeaker();
        GameManager.Instance.InputEnabler(true);
        StartCoroutine(EndGameCoroutine(true));
    }

    public void LearnBlack()
    {
        Debug.Log("learn Black");
        FluffDialogueManager.Instance.Wrong();
        Dictionary.Instance.AddWordsManually("Black", "검은색");
        EndMultipleGame();
        DialogueManager.Instance.StartDialogue(27);
        FluffDialogueManager.Instance.HideCurrentSpeaker();
        GameManager.Instance.InputEnabler(true);
        StartCoroutine(EndGameCoroutine(true));
    }

    public void Boss2bis()
    {
        EndDialogue();
        MultipleChoiceGame.Instance.StartGame(6);
    }

    public void LearnPlates()
    {
        Debug.Log("Learn Plate");
        FluffDialogueManager.Instance.Correct();
        Dictionary.Instance.AddWordsManually("Plate", "접시");
        EndMultipleGame();
        DialogueManager.Instance.StartDialogue(20);
        FluffDialogueManager.Instance.HideCurrentSpeaker();
        GameManager.Instance.InputEnabler(true);
    }

    public void LearnChop()
    {
        Debug.Log("Learn ChopSticks");
        FluffDialogueManager.Instance.Wrong();
        Dictionary.Instance.AddWordsManually("Chopsticks", "젓가락");
        EndMultipleGame();
        DialogueManager.Instance.StartDialogue(21);
        FluffDialogueManager.Instance.HideCurrentSpeaker();
        GameManager.Instance.InputEnabler(true);
    }

    public void LearnBreak()
    {
        Debug.Log("Learn Break");
        FluffDialogueManager.Instance.BossYell();
        Dictionary.Instance.AddWordsManually("Break", "휴식");
        EndMultipleGame();
        FluffDialogueManager.Instance.HideCurrentSpeaker();
        DialogueManager.Instance.StartDialogue(22);
        GameManager.Instance.InputEnabler(true);
    }

    public void LearnGlass()
    {
        Debug.Log("Learn Glass");
        FluffDialogueManager.Instance.Wrong();
        Dictionary.Instance.AddWordsManually("Glass", "잔");
        EndMultipleGame();
        DialogueManager.Instance.StartDialogue(23);
        FluffDialogueManager.Instance.HideCurrentSpeaker();
        GameManager.Instance.InputEnabler(true);
    }

    public void LearnWork()
    {
        Debug.Log("Add Work");
        Dictionary.Instance.AddWordsManually("Work", "일하다");
        MultipleChoiceGame.Instance.EndGame();
        WordPuzzleGame.Instance.StartGame(1);
    }
    
    public void LearnInsult1()
    {
        Debug.Log("Add Insult 1");
        FluffDialogueManager.Instance.Wrong();
        FluffDialogueManager.Instance.HideCurrentSpeaker();
        Dictionary.Instance.AddWordsManually("Dumbass", "멍청이");
        MultipleChoiceGame.Instance.EndGame();
        GameManager.Instance.InputEnabler(true);
    }

    public void RunAway()
    {
        Debug.Log("Add Word ID");
        FluffDialogueManager.Instance.Wrong();
        FluffDialogueManager.Instance.HideCurrentSpeaker();
        Dictionary.Instance.AddWordsManually("ID", "아이디");
        MultipleChoiceGame.Instance.EndGame();
        GameManager.Instance.InputEnabler(true);
    }

    public void ForgotID()
    {
        MultipleChoiceGame.Instance.EndGame();
        WordPuzzleGame.Instance.StartGame(4);
    }

    public void Fireman1()
    {
        DialogueManager.Instance.EndDialogue();
        MultipleChoiceGame.Instance.StartGame(2);
    }

    public void EndMultipleGame(bool miaw = true)
    {
        MultipleChoiceGame.Instance.EndGame();

        if(miaw)
        {
            var go = GameObject.Find("FireMan2");

            if(go)
                FluffDialogueManager.Instance.HideCurrentSpeaker();
        }
    }

    public void LearnDoctor()
    {
        Debug.Log("learn Doctor");
        FluffDialogueManager.Instance.Wrong();
        Dictionary.Instance.AddWordsManually("Doctor", "의사");
        EndMultipleGame();
        FluffDialogueManager.Instance.HideCurrentSpeaker();
        DialogueManager.Instance.StartDialogue(18);
        GameManager.Instance.InputEnabler(true);
    }

    public void LearnAlive()
    {
        Debug.Log("learn Alive");
        FluffDialogueManager.Instance.Wrong();
        Dictionary.Instance.AddWordsManually("Alive", "살아있다");
        EndMultipleGame();
        FluffDialogueManager.Instance.HideCurrentSpeaker();
        DialogueManager.Instance.StartDialogue(19);
        GameManager.Instance.InputEnabler(true);
    }

    public void LearnBaby()
    {
        Debug.Log("learn Baby");
        FluffDialogueManager.Instance.Correct();
        FluffDialogueManager.Instance.HideCurrentSpeaker();
        GameManager.Instance.EndFireEvent();
        Dictionary.Instance.AddWordsManually("Baby", "아기");
        EndMultipleGame();
        DialogueManager.Instance.StartDialogue(14);
        GameManager.Instance.InputEnabler(true);
    }

    public void Fireman2()
    {
        EndDialogue();
        MultipleChoiceGame.Instance.StartGame(3);
    }

    public void SomeoneGround()
    {
        EndMultipleGame();
        WordPuzzleGame.Instance.StartGame(5);
    }

    public void LearnFast()
    {
        Debug.Log("learn Fast");
        GameManager.Instance.ResurectMacarenaDude();
        FluffDialogueManager.Instance.HideCurrentSpeaker();
        Dictionary.Instance.AddWordsManually("Fast", "빨리");
        EndPuzzleGame();
        DialogueManager.Instance.StartDialogue(17);
        GameManager.Instance.InputEnabler(true);
    }

    public void EndPuzzleGame()
    {
        WordPuzzleGame.Instance.EndGame();
    }

    public void EndDialogue()
    {
        DialogueManager.Instance.EndDialogue();
    }

    public void LearnCat()
    {
        Debug.Log("learn Cat");
        FluffDialogueManager.Instance.Correct();
        FluffDialogueManager.Instance.HideCurrentSpeaker();
        GameManager.Instance.EndFireEvent();
        Dictionary.Instance.AddWordsManually("Cat", "고양이");
        EndMultipleGame();
        DialogueManager.Instance.StartDialogue(15);
        GameManager.Instance.InputEnabler(true);
    }

    public void LearnChair()
    {
        Debug.Log("learn Chair");
        FluffDialogueManager.Instance.Correct();
        FluffDialogueManager.Instance.HideCurrentSpeaker();
        GameManager.Instance.EndFireEvent();
        Dictionary.Instance.AddWordsManually("Chair", "의자");
        EndMultipleGame();
        DialogueManager.Instance.StartDialogue(16);
        GameManager.Instance.InputEnabler(true);
    }

    public void LEARNHELLO()
    {
        Debug.Log("Add Word Hello");
        Dictionary.Instance.AddWordsManually("Hello", "안녕하세요");
    }

    public void LearnRoom()
    {
        Debug.Log("Add Word Room");
        FluffDialogueManager.Instance.HideCurrentSpeaker();
        Dictionary.Instance.AddWordsManually("Room", "방");
        WordPuzzleGame.Instance.EndGame();
        GameManager.Instance.InputEnabler(true);
    }

    public void GameOver()
    {
        Debug.Log("GameOver");
        FluffDialogueManager.Instance.HideCurrentSpeaker();
        FluffDialogueManager.Instance.Wrong();
        MultipleChoiceGame.Instance.EndGame();
        GameManager.Instance.InputEnabler(true);
        StartCoroutine(EndGameCoroutine(false));
    }

    public void LearnCame()
    {
        Dictionary.Instance.AddWordsManually("Came", "왔어요");
        WordPuzzleGame.Instance.EndGame();
        FluffDialogueManager.Instance.HidePoliceMan();
        GameManager.Instance.InputEnabler(true);
    }
}