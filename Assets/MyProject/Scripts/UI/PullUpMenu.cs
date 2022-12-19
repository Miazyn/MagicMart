using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullUpMenu : MonoBehaviour
{
    [SerializeField] RectTransform button;
    [SerializeField] RectTransform background;
    [SerializeField] RectTransform logic;

    [SerializeField] RectTransform newPosButton;
    [SerializeField] RectTransform newPosBackground;
    [SerializeField] RectTransform newPosLogic;

    bool HasAnimPlayed;
    RectTransform ogButton, ogBG, ogLogic;

    Coroutine menuPullUp, menuPullDown;

    bool animPlaying = false;
    void Start()
    {
        HasAnimPlayed = false;
        ogButton = button;
        ogBG = background;
        ogLogic = logic;
    }

    public void MenuUp()
    {
        if (!animPlaying)
        {
            if (!HasAnimPlayed)
            {
                HasAnimPlayed = true;

                if (menuPullUp != null)
                {
                    StopCoroutine(menuPullUp);
                }
                menuPullUp = StartCoroutine(WaitForAnimToFinish());
                animPlaying = true;
            }
            else
            {
                MenuDown();
                animPlaying = true;
            }
        }

    }
    public void MenuDown()
    {
        HasAnimPlayed = false;
        if (menuPullDown != null)
        {
            StopCoroutine(menuPullDown);
        }
        menuPullDown = StartCoroutine(DisableAnim());
        
        button.anchoredPosition = ogButton.anchoredPosition;
        background.anchoredPosition = ogBG.anchoredPosition;
        logic.anchoredPosition = ogLogic.anchoredPosition;
    }

    IEnumerator DisableAnim()
    {
        button.gameObject.GetComponent<Animator>().SetTrigger("MenuDown");
        background.gameObject.GetComponent<Animator>().SetTrigger("MenuDown");
        logic.gameObject.GetComponent<Animator>().SetTrigger("MenuDown");

        yield return new WaitForSeconds(1.05f);

        animPlaying = false;
        SetIdle();
    }

    void SetIdle()
    {
        button.gameObject.GetComponent<Animator>().Play("Idle");
        background.gameObject.GetComponent<Animator>().Play("Idle");
        logic.gameObject.GetComponent<Animator>().Play("Idle");
    }

    IEnumerator WaitForAnimToFinish()
    {
        button.gameObject.GetComponent<Animator>().SetTrigger("MenuUp");
        background.gameObject.GetComponent<Animator>().SetTrigger("MenuUp");
        logic.gameObject.GetComponent<Animator>().SetTrigger("MenuUp");

        yield return new WaitForSeconds(1.05f);

        animPlaying = false;
        button.anchoredPosition = newPosButton.anchoredPosition;
        background.anchoredPosition = newPosBackground.anchoredPosition;
        logic.anchoredPosition = newPosLogic.anchoredPosition;
    }
}
