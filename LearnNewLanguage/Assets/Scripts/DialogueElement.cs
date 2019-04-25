using System;

[System.Serializable]
public class DialogueElement : IComparable<DialogueElement>
{
    public int id;
    public string sentence;
    public int displayTime;
    public string eventName;

    public DialogueElement(int p_id, string p_sentence, int p_displayTime,
                           string p_eventName)
    {
        id = p_id;
        sentence = p_sentence;
        displayTime = p_displayTime;
        eventName = p_eventName;
    }

    public int CompareTo(DialogueElement other)
    {
        if(other == null)
            return 1;
        
        //Return the difference in power
        return id - other.id;
    }
}
