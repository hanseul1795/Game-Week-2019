using System;

[System.Serializable]
public class WordPuzzle : IComparable<WordPuzzle>
{
    public int ID = 0;
    public int wordCount = 0;
    public int rightAnswerCount = 0;
    public string[] parsedString = null;
    public string[] answer = null;
    public string rightAnswerEvent = null;
    public string wrongAnswerEvent = null;
    public WordPuzzle(int p_id, string[] p_answer, string p_rightAnswerEvent, string p_wrongAnswerEvent, string[] p_parsedWordPuzzle, int p_rightAnswerCount, int p_wordCount)
    {
        ID = p_id;
        answer = p_answer;
        rightAnswerEvent = p_rightAnswerEvent;
        wrongAnswerEvent = p_wrongAnswerEvent;
        rightAnswerCount = p_rightAnswerCount;
        parsedString = p_parsedWordPuzzle;
        wordCount = p_wordCount;
    }

    public int CompareTo(WordPuzzle other)
    {
        if (other == null)
        {
            return 1;
        }
        //Return the difference in power.
        return ID - other.ID;
    }
}