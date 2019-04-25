using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Speakers
{
    None,
    PoliceMan,
    FireMan,
    Boss
}

public class FluffDialogueManager : Singleton<FluffDialogueManager>
{
    [SerializeField] private GameObject policeMan;
    [SerializeField] private GameObject fireMan;
    [SerializeField] private GameObject boss;

    private Animator policeManAnimator;
    private Animator fireManAnimator;
    private Animator bossAnimator;

    public Speakers currentSpeaker;

    private void Start()
    {
        policeManAnimator = policeMan.GetComponent<Animator>();
        fireManAnimator = fireMan.GetComponent<Animator>();
        bossAnimator = boss.GetComponent<Animator>();
    }

    public void HideCurrentSpeaker()
    {
        switch(currentSpeaker)
        {
            case Speakers.PoliceMan:
                StartCoroutine(HidePoliceManCoroutine());
            break;

            case Speakers.FireMan:
                StartCoroutine(HideFireManCoroutine());
            break;

            case Speakers.Boss:
                StartCoroutine(HideBossCoroutine());
            break;
        }
    }

    public void ShowSpeaker(Speakers p_speaker)
    {
        switch(p_speaker)
        {
            case Speakers.PoliceMan:
                ShowPoliceMan();
                break;

            case Speakers.FireMan:
                ShowFireMan();
                break;

            case Speakers.Boss:
                ShowBoss();
                break;
        }
    }

    public void ShowFireMan()
    {
        currentSpeaker = Speakers.FireMan;
        fireMan.SetActive(true);
    }

    public void ShowBoss()
    {
        currentSpeaker = Speakers.Boss;
        boss.SetActive(true);
    }

    public void Correct()
    {
        switch(currentSpeaker)
        {
            case Speakers.PoliceMan:
                PoliceManCorrect();
            break;

            case Speakers.FireMan:
                FireManCorrect();
            break;

            case Speakers.Boss:
                BossCorrect();
            break;
        }
    }

    public void BossYell()
    {
        bossAnimator.Play("Yell");
    }

    public void FireManCorrect()
    {
        fireManAnimator.Play("Correct");
    }

    public void BossCorrect()
    {
        bossAnimator.Play("Correct");
    }

    public void BossWrong()
    {
        bossAnimator.Play("Wrong");
    }

    public void FireManWrong()
    {
        fireManAnimator.Play("Wrong");
    }

    public void Wrong()
    {
        switch(currentSpeaker)
        {
            case Speakers.PoliceMan:
                PoliceManWrong();
            break;

            case Speakers.FireMan:
                FireManWrong();
            break;

            case Speakers.Boss:
                BossWrong();
            break;
        }
    }

    public void ShowPoliceMan()
    {
        currentSpeaker = Speakers.PoliceMan;
        policeMan.SetActive(true);
    }

    public void HidePoliceMan()
    {
        StartCoroutine(HidePoliceManCoroutine());
    }

    public void PoliceManPlayIdle()
    {
        policeManAnimator.Play("Policeman_Idle");
    }

    public void PoliceManWrong()
    {
        policeManAnimator.Play("Wrong");
    }

    public void PoliceManCorrect()
    {
        policeManAnimator.Play("Correct");
    }

    private IEnumerator HidePoliceManCoroutine()
    {
        yield return new WaitForSeconds(3);
        currentSpeaker = Speakers.None;
        policeMan.SetActive(false);
    }

    private IEnumerator HideFireManCoroutine()
    {
        yield return new WaitForSeconds(3);
        currentSpeaker = Speakers.None;
        fireMan.SetActive(false);
    }

    private IEnumerator HideBossCoroutine()
    {
        yield return new WaitForSeconds(3);
        currentSpeaker = Speakers.None;
        boss.SetActive(false);
    }

}
