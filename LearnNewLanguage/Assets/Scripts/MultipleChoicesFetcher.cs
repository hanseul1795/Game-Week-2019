using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;



public class MultipleChoicesFetcher : Singleton<MultipleChoicesFetcher>
{
    public string tsvUrl;

    private List<MultipleChoiceQuestion> multipleChoicesBank;

    private void Start()
    {
        multipleChoicesBank = new List<MultipleChoiceQuestion>();
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

        ParseMultipleChoices(tsv);                                                  
    }

    private void ParseMultipleChoices(string tsv)                                                 
    {
        StringReader reader = new StringReader(tsv);

        int id = 0;

        reader.ReadLine();                                                          //Burn the first line

        while(reader.Peek() > -1)
        {
            string line = reader.ReadLine();                                        //read a line
            string[] lineContent = line.Split('\t');                                //tsv a seperated with tab '\t'
            MultipleChoiceQuestion question = new MultipleChoiceQuestion(id, lineContent[0],
                                                                         lineContent[1], lineContent[2],
                                                                         lineContent[3], lineContent[4],
                                                                         lineContent[5], lineContent[6],
                                                                         lineContent[7], lineContent[8]);

            multipleChoicesBank.Add(question);
            ++id;
        }
    }


    public MultipleChoiceQuestion FetchQuestionByID(int p_id)
    {
        foreach(MultipleChoiceQuestion question in multipleChoicesBank)
        {
            if(question.id == p_id)
            return question;
        }

        Debug.Log("Warning Multiple choice Question ID Does not exist ");
        return null;
    }
}
