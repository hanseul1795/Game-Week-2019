using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MovableWord : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int wordID;
    

    //this will be used to show the text;
    public string text;
    public Vector3 startPosition;
    public bool readyToVerify = false;
    public Transform startParent = null;
    public MovableWord(int p_wordID,  string p_word)
    {
        wordID = p_wordID;
        text = p_word;
    }

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!readyToVerify)
        {
            startPosition = transform.position;
            startParent = transform.parent;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(!readyToVerify)
            transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!readyToVerify)
        {
            if (startParent == transform.parent)
                transform.position = startPosition;

            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        //this.transform.position = OriginalPosition.position + this.transform.parent.position;
    }
}