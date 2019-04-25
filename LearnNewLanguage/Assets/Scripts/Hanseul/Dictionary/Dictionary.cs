using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Dictionary : Singleton<Dictionary>
{
    
    public GameObject DictionaryCanvas;
    //TextSpace will be used to write more text into the dictionary
    public GameObject TextSpace;
    public Button CloseButton;
    public Button OpenButton;
    private int numberOfWords = 0;
    // Start is called before the first frame update
    void Start()
    {
       DictionaryCanvas.SetActive(false);
       StartCoroutine(StartGameCoroutine());
    }
    IEnumerator StartGameCoroutine()
    {
        yield return new WaitForSeconds(2f);
        TestAllCharacters();
    }
    private void TestAllCharacters()
    {
        for(int i = 0; i < DictionaryFetcher.Instance.DictionaryText.Count; ++i)
        {
            string english = DictionaryFetcher.Instance.DictionaryText[i].english;
            string korean = DictionaryFetcher.Instance.DictionaryText[i].korean;
            AddWordsManually(english, korean);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (OpenButton.GetComponent<OpenButton>().isCalled)
        {
            if (OpenButton.GetComponent<OpenButton>().isOpen)
            {
                DictionaryCanvas.SetActive(true);
                CloseButton.GetComponent<CloseButton>().OpenDictionary();
                OpenButton.GetComponent<OpenButton>().isCalled = false;
            }
        }
        if(CloseButton.GetComponent<CloseButton>().isCalled)
        { 
            if (CloseButton.GetComponent<CloseButton>().isClose)
            {
                DictionaryCanvas.SetActive(false);
                OpenButton.GetComponent<OpenButton>().DictionaryClosed();
                CloseButton.GetComponent<CloseButton>().isCalled = false;
            }
        }
    }
    public void AddTextToDictionary(string p_string)
    {
       DictionaryWords words =  DictionaryFetcher.Instance.fetchWordByWord(p_string);
       if(words != null)
       {
            AddWordsInDictionary(words);
       }
    }
    public void AddWordsManually(string p_english, string p_korean)
    {
        TextMeshProUGUI Text = TextSpace.GetComponentInChildren<TextMeshProUGUI>();
        int lineNumber = numberOfWords + 1;
        string toAdd = lineNumber + " -  " + p_english + "      " + p_korean;
        if (numberOfWords == 0)
        {
            Text.text = toAdd;
            ++numberOfWords;
        }
        else
        {
            TextMeshProUGUI NextLine = Instantiate(Text, TextSpace.GetComponent<Transform>());
            NextLine.text = toAdd;
            ++numberOfWords;
        }
    }
    private void AddWordsInDictionary(DictionaryWords p_words)
    {
        TextMeshProUGUI Text = TextSpace.GetComponentInChildren<TextMeshProUGUI>();
        int lineNumber = numberOfWords + 1;
        string toAdd = lineNumber + " -  " + p_words.english + "      " + p_words.korean;
        if (numberOfWords == 0)
        {           
            Text.text = toAdd;
        }
        else
        {
            TextMeshProUGUI NextLine = Instantiate(Text, TextSpace.GetComponent<Transform>());
            NextLine.text = toAdd;
        }     
    }
}