using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DialogueFetcher : Singleton<DialogueFetcher>
{
    public string tsvUrl;

    public List<DialogueElement> dialoguesBank;

    private void Start()
    {
        dialoguesBank = new List<DialogueElement>();
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

        ParseDialogues(tsv);                                                  
    }

    private void ParseDialogues(string tsv)                                                 
    {
        StringReader reader = new StringReader(tsv);

        int id = 0;

        reader.ReadLine();                                                          //Burn the first line

        while(reader.Peek() > -1)
        {
            string line = reader.ReadLine();                                        //read a line
            string[] lineContent = line.Split('\t');                                //tsv a seperated with tab '\t'
            DialogueElement dialogueElement = new DialogueElement(id , lineContent[0], int.Parse(lineContent[1]),
                                                                  lineContent[2]);

            dialoguesBank.Add(dialogueElement);
            ++id;
        }
    }


    public DialogueElement FetchDialogueByID(int p_id)
    {
        foreach(DialogueElement dialogueElement in dialoguesBank)
        {
            if(dialogueElement.id == p_id)
                return dialogueElement;
        }

        Debug.Log("Warning Dialogue Element ID Does not exist");
        return null;
    }
}
