using System;

[System.Serializable]
public class MultipleChoiceQuestion : IComparable<MultipleChoiceQuestion>
{
    public int id;
    public string question;
    public string[] choices = new string [4];
    public string[] choicesEvents = new string [4];

    public MultipleChoiceQuestion(int p_id, string p_question,
                                  string p_choice0, string p_choice1, string p_choice2, string p_choice3,
                                  string p_choice0Event, string p_choice1Event, string p_choice2Event, string p_choice3Event)
    {
        //choices = new string [3];
        //choicesEvents = new string [3];

        id = p_id;
        question = p_question;

        choices[0] = p_choice0;
        choices[1] = p_choice1;
        choices[2] = p_choice2;
        choices[3] = p_choice3;

        choicesEvents[0] = p_choice0Event;
        choicesEvents[1] = p_choice1Event;
        choicesEvents[2] = p_choice2Event;
        choicesEvents[3] = p_choice3Event;
    }

    public int CompareTo(MultipleChoiceQuestion other)
    {
        if(other == null)
            return 1;
        
        //Return the difference in power
        return id - other.id;
    }
}
