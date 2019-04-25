using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WordPuzzleRefFetcher : Singleton<WordPuzzleRefFetcher>
{
    public string tsvUrl;

    public List<WordPuzzleEnglishRef> WordPuzzleRefBank;

    private void Start()
    {
        WordPuzzleRefBank = new List<WordPuzzleEnglishRef>();
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
        ParseWordPuzzleRef(tsv);
    }
    private void ParseWordPuzzleRef(string tsv)
    {
        StringReader reader = new StringReader(tsv);

        int id = 0;
        reader.ReadLine();
        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();

            string[] lineContent = line.Split('\t');
            string answer = lineContent[0]; //tsv a seperated with tab '\t'
            string[] splitAnswer = answer.Split(' ');
        
            for (int i = 0; i < splitAnswer.Length; ++i)
            {
                string[] emptySpaceEraser = splitAnswer[i].Split(' ');
                if (emptySpaceEraser.Length > 1)
                    splitAnswer[i] = emptySpaceEraser[i];
            }

            WordPuzzleEnglishRef puzzleRef = new WordPuzzleEnglishRef(id, splitAnswer);
            WordPuzzleRefBank.Add(puzzleRef);
            ++id;
        }
    }

    public WordPuzzleEnglishRef FetchPuzzleByID(int p_id)
    {
        foreach (WordPuzzleEnglishRef wordPuzzleRef in WordPuzzleRefBank)
        {
            if (wordPuzzleRef.ID == p_id)
                return wordPuzzleRef;
        }

        Debug.Log("Warning Word Puzzle Ref ID Does not exist ");
        return null;
    }
}
