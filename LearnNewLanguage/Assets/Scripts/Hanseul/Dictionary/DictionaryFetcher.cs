using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DictionaryFetcher : Singleton<DictionaryFetcher>
{
    public string tsvUrl;

    public List<DictionaryWords> DictionaryText;

    private void Start()
    {
        DictionaryText = new List<DictionaryWords>();
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
        ParseDictionaryWords(tsv);
    }
    private void ParseDictionaryWords(string tsv)
    {
        StringReader reader = new StringReader(tsv);

        int id = 0;
        reader.ReadLine();
        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();

            string[] lineContent = line.Split('\t');

            DictionaryWords words = new DictionaryWords(id, lineContent[0], lineContent[1]);
            DictionaryText.Add(words);
            ++id;
        }
    }

    public DictionaryWords fetchWordByWord(string p_word)
    {
        foreach (DictionaryWords dictionaryword in DictionaryText)
        {
            if (dictionaryword.searchID == p_word)
                return dictionaryword;
        }

        Debug.Log("Warning Word string Does not exist ");
        return null;
    }
}
