using System;

[System.Serializable]
public class WordPuzzleEnglishRef : IComparable<WordPuzzleEnglishRef>
{
    public int ID = 0;
    public string[] parsedString = null;
    public WordPuzzleEnglishRef(int p_id, string[] p_parsedString)
    {
        ID = p_id;
        parsedString = p_parsedString;
    }

    public int CompareTo(WordPuzzleEnglishRef other)
    {
        if (other == null)
        {
            return 1;
        }
        //Return the difference in power.
        return ID - other.ID;
    }
}
