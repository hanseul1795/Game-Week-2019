using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject fireLady;
    [SerializeField] private GameObject fireSystem;
    [SerializeField] private Animator macarenaDude;
    [SerializeField] private PlayerController playerControl;

    public void EndFireEvent()
    {
        fireLady.SetActive(false);
        fireSystem.SetActive(false);
    }

    public void InputEnabler(bool p_enable)
    {
        playerControl.InputEnable(p_enable);
    }
    public void Victory()
    {
        SceneManager.LoadScene("VictoryScene", LoadSceneMode.Single); 
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOverScene", LoadSceneMode.Single);
    }

    public void ResurectMacarenaDude()
    {
        macarenaDude.Play("Dance");
    }


}
