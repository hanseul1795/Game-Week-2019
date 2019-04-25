using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class WordPuzzleGame : Singleton<WordPuzzleGame>
{
    public GameObject AnswerSpace;
    public GameObject WordSpace;
    public GameObject ButtonSpace;

    public TMP_FontAsset KoreanFont;
    public Material KoreanFontMaterial;

    List<GameObject> movableWords = new List<GameObject>();
    List<GameObject> movableWordPanels = new List<GameObject>();
    List<GameObject> answerSpaces = new List<GameObject>();
    List<GameObject> buttons = new List<GameObject>();
    List<GameObject> answerSpaceCharacters = new List<GameObject>();
    List<Transform> movableInitialtransform = new List<Transform>();
    WordPuzzle CurrentPuzzle = null;
    WordPuzzleEnglishRef CurrentEnglishRef = null;
    void Start()
    {
        AnswerSpace.SetActive(false);
        WordSpace.SetActive(false);
        ButtonSpace.SetActive(false);
    }

    void CreateQuiz(int p_puzzleID)
    {
        WordPuzzle question = WordPuzzleFetcher.Instance.FetchPuzzleByID(p_puzzleID);
        WordPuzzleEnglishRef questionRef = WordPuzzleRefFetcher.Instance.FetchPuzzleByID(p_puzzleID);
        if (question != null && questionRef != null)
        {
            CurrentPuzzle = question;
            CurrentEnglishRef = questionRef;
        }
        if (CurrentPuzzle != null && CurrentEnglishRef != null)
        {
            if (buttons.Count == 0)
                CreateButtons();

            CreateAnswerSpacePanels(CurrentPuzzle.rightAnswerCount);
            CreateMovableWordPanels(CurrentPuzzle.wordCount);
            CreateMovableWords(CurrentPuzzle.wordCount, CurrentPuzzle.parsedString);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (buttons.Count > 0 && answerSpaces.Count > 0)
        {
            if (buttons[1].GetComponent<RestartButton>().restart)
                ResetPuzzle();

            if (buttons[0].GetComponent<VerifyButton>().isVerifying)
            {
                for (int i = 0; i < answerSpaces.Count; ++i)
                {
                    if (!answerSpaces[i].GetComponent<WordAnswerSpace>().isWordAvailable)
                    {
                        buttons[0].GetComponent<VerifyButton>().ResetIsVerifying();
                        break;
                    }
                    if (i == answerSpaces.Count - 1)
                    {
                        VerifyAnswer();
                        buttons[0].GetComponent<VerifyButton>().ResetIsVerifying();
                    }
                }
            }
        }
    }
  
    private void CreateWordPuzzle(int questionID)
    {
        WordPuzzle puzzle = WordPuzzleFetcher.Instance.FetchPuzzleByID(questionID);
        if (puzzle != null)
           CurrentPuzzle = puzzle;

    }

    private void CreateMovableWordPanels(int p_size)
    {
       for(int i =0; i < p_size; ++i)
       {
            GameObject wordspace = new GameObject();
            wordspace.AddComponent<CanvasRenderer>();
            wordspace.AddComponent<RectTransform>();
            wordspace.AddComponent<Image>();
            wordspace.AddComponent<GridLayoutGroup>();
            wordspace.GetComponent<GridLayoutGroup>().childAlignment = TextAnchor.MiddleCenter;
            wordspace.GetComponent<RectTransform>().SetParent(WordSpace.GetComponent<RectTransform>());
            wordspace.GetComponent<GridLayoutGroup>().cellSize = WordSpace.GetComponent<GridLayoutGroup>().cellSize - new Vector2(5, 5);
            movableWordPanels.Add(wordspace);
       }
    }

    private void CreateMovableWords(int p_size, string[] p_words)
    {
        for(int i = 0; i < p_size; ++i)
        {
            GameObject movableWord = new GameObject();
            movableWord.AddComponent<CanvasRenderer>();
            movableWord.AddComponent<RectTransform>();
            movableWord.AddComponent<Image>();
            movableWord.GetComponent<Image>().color = Color.red;
            movableWord.AddComponent<MovableWord>();
            movableWord.AddComponent<CanvasGroup>();
            movableWord.GetComponent<CanvasGroup>().blocksRaycasts = true;
            movableWord.GetComponent<CanvasGroup>().interactable = true;
            movableWord.GetComponent<RectTransform>().SetParent(movableWordPanels[i].GetComponent<RectTransform>());
            //movableWord.GetComponent<MovableWord>().wordID = i + 1;

            GameObject textObject = new GameObject();
            textObject.AddComponent<CanvasRenderer>();
            textObject.AddComponent<RectTransform>();
            TextMeshProUGUI temp = textObject.AddComponent<TextMeshProUGUI>();
            temp.font = KoreanFont;
            temp.fontMaterial = KoreanFontMaterial;
            temp.text = p_words[i];
            temp.color = Color.black;
            movableWord.GetComponent<MovableWord>().text = temp.text;
            textObject.GetComponent<TextMeshProUGUI>().fontSize = 25;
            textObject.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Midline;
            textObject.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
            textObject.AddComponent<AspectRatioFitter>();
            textObject.GetComponent<AspectRatioFitter>().aspectMode = AspectRatioFitter.AspectMode.FitInParent;
            textObject.GetComponent<AspectRatioFitter>().aspectRatio = 3f;
            textObject.GetComponent<Transform>().SetParent(movableWord.GetComponent<Transform>());
            answerSpaceCharacters.Add(textObject);
            movableInitialtransform.Add(movableWord.GetComponent<Transform>());
            movableWords.Add(movableWord);
        }
    }

    private void CreateAnswerSpacePanels(int p_size)
    {
        for (int i = 0; i < p_size; ++i)
        {
            GameObject answer = new GameObject();
            answer.AddComponent<CanvasRenderer>();
            answer.AddComponent<RectTransform>();
            answer.AddComponent<Image>();
            answer.AddComponent<WordAnswerSpace>();
            answer.AddComponent<GridLayoutGroup>();
            answer.GetComponent<GridLayoutGroup>().childAlignment = TextAnchor.MiddleCenter;
            answer.GetComponent<GridLayoutGroup>().cellSize = AnswerSpace.GetComponent<GridLayoutGroup>().cellSize - new Vector2(5, 5);

            GameObject textObject = new GameObject();
            textObject.AddComponent<CanvasRenderer>();
            textObject.AddComponent<RectTransform>();
            TextMeshProUGUI temp = textObject.AddComponent<TextMeshProUGUI>();
            temp.font = KoreanFont;
            temp.fontMaterial = KoreanFontMaterial;
            temp.text = CurrentEnglishRef.parsedString[i];
            temp.color = Color.black;

            textObject.GetComponent<TextMeshProUGUI>().fontSize = 25;
            textObject.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Top;
            textObject.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
            textObject.AddComponent<AspectRatioFitter>();
            textObject.GetComponent<Transform>().SetParent(answer.transform);
            answerSpaces.Add(answer);
            answer.GetComponent<RectTransform>().SetParent(AnswerSpace.GetComponent<RectTransform>());
        }
    }

    private void CreateVerifyButton()
    {
        GameObject VerifyButton = new GameObject();
        VerifyButton.AddComponent<CanvasRenderer>();
        VerifyButton.AddComponent<RectTransform>();
        VerifyButton.AddComponent<Image>();
        VerifyButton.AddComponent<Button>();
        VerifyButton.AddComponent<VerifyButton>();
        VerifyButton.GetComponent<VerifyButton>().AddToListner();

        GameObject textObject = new GameObject();
        textObject.AddComponent<CanvasRenderer>();
        textObject.AddComponent<RectTransform>();
        TextMeshProUGUI temp = textObject.AddComponent<TextMeshProUGUI>();
        temp.font = KoreanFont;
        temp.fontMaterial = KoreanFontMaterial;
        temp.text = "VERIFY";
        temp.color = Color.black;

        textObject.GetComponent<TextMeshProUGUI>().fontSize = 25;
        textObject.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Midline;
        textObject.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
        textObject.AddComponent<AspectRatioFitter>();
        textObject.GetComponent<AspectRatioFitter>().aspectMode = AspectRatioFitter.AspectMode.FitInParent;
        textObject.GetComponent<AspectRatioFitter>().aspectRatio = 3f;
        VerifyButton.GetComponent<RectTransform>().SetParent(ButtonSpace.GetComponent<RectTransform>());
        textObject.GetComponent<Transform>().SetParent(VerifyButton.GetComponent<Transform>());
        buttons.Add(VerifyButton);
    }

    private void CreateRestartButton()
    {
        GameObject restartButton = new GameObject();
        restartButton.AddComponent<CanvasRenderer>();
        restartButton.AddComponent<RectTransform>();
        restartButton.AddComponent<Image>();
        restartButton.AddComponent<Button>();
        restartButton.AddComponent<RestartButton>();
        restartButton.GetComponent<RestartButton>().AddToListner();

        GameObject textObject = new GameObject();
        textObject.AddComponent<CanvasRenderer>();
        textObject.AddComponent<RectTransform>();
        TextMeshProUGUI temp = textObject.AddComponent<TextMeshProUGUI>();
        temp.font = KoreanFont;
        temp.fontMaterial = KoreanFontMaterial;
        temp.text = "RESET";
        temp.color = Color.black;

        textObject.GetComponent<TextMeshProUGUI>().fontSize = 25;
        textObject.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Midline;
        textObject.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
        textObject.AddComponent<AspectRatioFitter>();
        textObject.GetComponent<AspectRatioFitter>().aspectMode = AspectRatioFitter.AspectMode.FitInParent;
        textObject.GetComponent<AspectRatioFitter>().aspectRatio = 3f;
        restartButton.GetComponent<RectTransform>().SetParent(ButtonSpace.GetComponent<RectTransform>());
        textObject.GetComponent<Transform>().SetParent(restartButton.GetComponent<Transform>());
        buttons.Add(restartButton);
    }
    
    private void CreateButtons()
    {
        CreateVerifyButton();
        CreateRestartButton();
    }

    private void ResetPuzzle()
    {
        RepositionMovableWords();
        ResetAnswerSpaces();
        ResetButtons();
    }

    private void VerifyAnswer()
    {
        int RightAnswer = 0;
        bool DontTrigger = false;
        for(int i = 0; i < answerSpaces.Count; ++i)
        {   
            if(answerSpaces[i].GetComponentInChildren<MovableWord>() == null)
            {
                DontTrigger = true;
                break;
            }
            if(answerSpaces[i].GetComponentInChildren<MovableWord>().text == CurrentPuzzle.answer[i])
            {               
                ++RightAnswer;
            }
        }
        if (!DontTrigger)
        {
            if (RightAnswer == CurrentPuzzle.rightAnswerCount)
            {
                //Debug.Log("CONGRATUATION, YOU GOT IT RIGHT");
                FluffDialogueManager.Instance.Correct();
                TriggerRightAnswerEvent();
            }
            else
            {
                FluffDialogueManager.Instance.Wrong();
                TriggerWrongAnswerEvent();
            }
        }
    }

    private void ResetAnswerSpaces()
    {
        for(int i = 0; i < answerSpaces.Count; ++i)
        {
            answerSpaces[i].GetComponent<WordAnswerSpace>().isWordAvailable = false;
            foreach (Transform child in answerSpaces[i].transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    private void RepositionMovableWords()
    {
        if (movableWordPanels.Count > 0 && movableWords.Count > 0)
        {
            for (int i = 0; i < movableWords.Count; ++i)
            {
                movableWords[i].GetComponent<Transform>().SetParent(movableWordPanels[i].GetComponent<Transform>());
                movableWords[i].GetComponent<Transform>().position = movableInitialtransform[i].position;
                movableWords[i].GetComponent<MovableWord>().readyToVerify = false;
            }
        }
    }
 
    private void ResetButtons()
    {
        buttons[0].GetComponent<VerifyButton>().isVerifying = false;
        buttons[1].GetComponent<RestartButton>().restart = false;
    }

    private void TriggerRightAnswerEvent()
    {
        if (CurrentPuzzle.rightAnswerEvent != "NULL")
        {
            GameEvents.Instance.LaunchEvent(CurrentPuzzle.rightAnswerEvent);
            CleanUpPuzzle();
        }
        else
        {
            CleanUpPuzzle();
        }
    }

    private void TriggerWrongAnswerEvent()
    {
        if (CurrentPuzzle.wrongAnswerEvent != "NULL")
        {
            GameEvents.Instance.LaunchEvent(CurrentPuzzle.wrongAnswerEvent);
            CleanUpPuzzle();
        }
        else
        {
            CleanUpPuzzle();
        }
    }

    public void StartGame(int p_puzzleID = 0)
    {
        AnswerSpace.SetActive(true);
        WordSpace.SetActive(true);
        ButtonSpace.SetActive(true);
        CreateQuiz(p_puzzleID);
    }

    public void EndGame()
    {
        AnswerSpace.SetActive(false);
        WordSpace.SetActive(false);
        ButtonSpace.SetActive(false);
    }
    private void CleanUpPuzzle()
    {
        if (answerSpaces.Count > 0 && movableWords.Count > 0)
        {
            //RepositionMovableWords();
            //ResetAnswerSpaces();
            foreach (Transform child in AnswerSpace.transform)
            {
                Destroy(child.gameObject);
            }
            foreach (Transform child in WordSpace.transform)
            {
                Destroy(child.gameObject);
            }
            answerSpaces.Clear();             
            movableWords.Clear();
            Debug.Log(movableWords.Count);
            movableWordPanels.Clear();
            ResetButtons();
        }
    } 
}