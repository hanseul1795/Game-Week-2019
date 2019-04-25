using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WordAnswerSpace : MonoBehaviour, IDropHandler
{
    public int answerID;
    public bool isWordAvailable = false;


    //this will be used to show the text;
    private TextMeshProUGUI text;
    public WordAnswerSpace(int p_answerID)
    {
        answerID = p_answerID;
    }
    /*
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
        {
            return;
        }
        MovableWord d = eventData.pointerDrag.GetComponent<MovableWord>();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("OnPointerExit");
        if (eventData.pointerDrag == null)
            return;

        MovableWord d = eventData.pointerDrag.GetComponent<MovableWord>();
    }*/

    public void OnDrop(PointerEventData eventData)
    {
        if (!isWordAvailable)
        {
            MovableWord d = eventData.pointerDrag.GetComponent<MovableWord>();
            if (d != null)
            {
                foreach (Transform child in this.transform)
                {
                    child.gameObject.SetActive(false);
                }
                d.transform.SetParent(this.transform);
                d.readyToVerify = true;
                d.GetComponent<CanvasGroup>().blocksRaycasts = true;
                isWordAvailable = true;
            }
        }
    }
}