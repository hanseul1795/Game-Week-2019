using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerifyButton : MonoBehaviour
{
    public bool isVerifying = false;
    public void AddToListner()
    {
        GetComponent<Button>().onClick.AddListener(verify);
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void ResetIsVerifying()
    {
        if (isVerifying)
            isVerifying = false;
    }
    private void verify()
    {
        isVerifying = true;
    }
}