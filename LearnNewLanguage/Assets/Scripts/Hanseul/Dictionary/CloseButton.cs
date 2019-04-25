using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    public bool isClose = true;
    public bool isCalled = false;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(CloseDictionary);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OpenDictionary()
    {
            isClose = false;
            isCalled = false;
    }

    public void CloseDictionary()
    {
            isClose = true;
            isCalled = true;
    }
}
