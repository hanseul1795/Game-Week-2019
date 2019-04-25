using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    public bool restart = false;
    public void AddToListner()
    {
        GetComponent<Button>().onClick.AddListener(RestartPuzzle);
    }

    private void RestartPuzzle()
    {
        restart = true;
    }
}