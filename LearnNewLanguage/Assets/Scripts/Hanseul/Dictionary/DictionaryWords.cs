using System;

[System.Serializable]
public class DictionaryWords : IComparable<DictionaryWords>
{
    public int ID = 0;
    public string searchID = null;
    public string english = null;
    public string korean = null;
    public int CompareTo(DictionaryWords other)
    {
        if (other == null)
        {
            return 1;
        }
        //Return the difference in power.
        return ID - other.ID;
    }
    public DictionaryWords(int p_id, string p_english, string p_korean)
    {
        searchID = p_english;
        english = p_english;
        korean = p_korean;
    }
}