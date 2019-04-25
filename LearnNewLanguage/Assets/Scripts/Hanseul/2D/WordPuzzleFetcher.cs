using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WordPuzzleFetcher : Singleton<WordPuzzleFetcher>
{
    public string tsvUrl;

    public List<WordPuzzle> WordPuzzleBank;

    private void Start()
    {
        WordPuzzleBank = new List<WordPuzzle>();
        StartCoroutine(FetchTSV(tsvUrl));
    }

    private IEnumerator FetchTSV(string url)                                            //Fetches the TSV from the url
    {
        string tsv;

        using (WWW www = new WWW(url))
        {
            yield return www;
            tsv = www.text;
        }
        ParseWordPuzzle(tsv);
    }
    private void ParseWordPuzzle(string tsv)
    {
        StringReader reader = new StringReader(tsv);

        int id = 0;
        reader.ReadLine();
        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();

            string[] lineContent = line.Split('\t');
            string answer = lineContent[0]; //tsv a seperated with tab '\t'
            string rightAnswer = lineContent[1];
            string wrongAnswer = lineContent[2];
            string[] splitAnswer = answer.Split(' ');

            string[] parsedString = null;
            if (string.IsNullOrEmpty(lineContent[lineContent.Length - 1]))
            {
                parsedString = new string[lineContent.Length - 4];
            }
            else
            {
                parsedString = new string[lineContent.Length - 3];
            }
            for(int i = 0; i < parsedString.Length; ++i)
            {
                parsedString[i] = lineContent[i + 3];
                string[] emptySpaceEraser = parsedString[i].Split(' ');
                if(emptySpaceEraser.Length > 1)
                    parsedString[i] = emptySpaceEraser[0];
            }
            
            WordPuzzle puzzle = new WordPuzzle(id, splitAnswer, rightAnswer, wrongAnswer, parsedString, splitAnswer.Length, parsedString.Length);
            WordPuzzleBank.Add(puzzle);
            ++id;
        }
    }
    public void NumberOfWordPuzzle()
    {
        Debug.Log(WordPuzzleBank.Count);
    }
    public WordPuzzle FetchPuzzleByID(int p_id)
    {
        foreach(WordPuzzle wordPuzzle in WordPuzzleBank)
        {
            if (wordPuzzle.ID == p_id)
           return wordPuzzle;
        }

        Debug.Log("Warning Word Puzzle ID Does not exist ");
        return null;
    }
}