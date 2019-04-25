using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Reflection;

public class MultipleChoiceGame : Singleton<MultipleChoiceGame>
{
    [SerializeField] private GameObject gameCanvas;
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private TextMeshProUGUI choice0Text;
    [SerializeField] private TextMeshProUGUI choice1Text;
    [SerializeField] private TextMeshProUGUI choice2Text;
    [SerializeField] private TextMeshProUGUI choice3Text;

    private MultipleChoiceQuestion currentQuestion = null;

    private void Start()
    {
        gameCanvas.SetActive(false);
        //StartCoroutine(StartGameCoroutine());
    }

    public void StartGame(int questionID = 0)
    {
        gameCanvas.SetActive(true);
        AskQuestion(questionID);
    }

    public void EndGame()
    {
        gameCanvas.SetActive(false);
    }

    IEnumerator StartGameCoroutine()
    {
        yield return new WaitForSeconds(2);
        AskQuestion(0);
    }

    public void ValidateAnswer(int playerChoice)
    {
        if(currentQuestion != null)
            if(currentQuestion.choicesEvents[playerChoice] != "NULL")
                GameEvents.Instance.LaunchEvent(currentQuestion.choicesEvents[playerChoice]);
    }

    private void AskQuestion(int questionID)
    {
        gameCanvas.SetActive(true);
        MultipleChoiceQuestion question = MultipleChoicesFetcher.Instance.FetchQuestionByID(questionID);
        if(question != null)
        {
            currentQuestion = question;
            UpdateQuestionUI(question);
        }
    }

    private void UpdateQuestionUI(MultipleChoiceQuestion p_question)
    {
        questionText.text = p_question.question;
        choice0Text.text = p_question.choices[0];
        choice1Text.text = p_question.choices[1];
        choice2Text.text = p_question.choices[2];
        choice3Text.text = p_question.choices[3];
    }

    //This maps user choice# from UI (wrong choice) to the wrong choice cell in currentAnswer
    private int MapChoiceToBadAnswer(int p_correctAnswer, int p_choice)
    {
        if(p_correctAnswer == 0)
        {
            if(p_choice == 1)
                return 0;
            if(p_choice == 2)
                return 1;
            if(p_choice == 3)
                return 2;
        }
        else if(p_correctAnswer == 1)
        {
             if(p_choice == 0)
                return 0;
            if(p_choice == 2)
                return 1;
            if(p_choice == 3)
                return 2;
        }
        else if(p_correctAnswer == 2)
        {
             if(p_choice == 0)
                return 0;
            if(p_choice == 1)
                return 1;
            if(p_choice == 3)
                return 2;
        }
        else if(p_correctAnswer == 3)
        {
             if(p_choice == 0)
                return 0;
            if(p_choice == 1)
                return 1;
            if(p_choice == 2)
                return 2;
        }
        return -1;
    }
}
