using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenButton : MonoBehaviour
{
    public bool isOpen = false;
    public bool isCalled = false;// Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(DictionaryOpened);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DictionaryOpened()
    {
            isOpen = true;
            isCalled = true;
            gameObject.SetActive(false);
    }

    public void DictionaryClosed()
    {
            isOpen = false;
            isCalled = false;
            gameObject.SetActive(true);
    }
}