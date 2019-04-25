using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDie : MonoBehaviour
{
    [SerializeField] private Animator deadMan;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")    
        {
            deadMan.Play("Die");
        }
    }
}
