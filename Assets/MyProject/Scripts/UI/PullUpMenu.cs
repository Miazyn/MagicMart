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

    void Start()
    {
        HasAnimPlayed = false;
        ogButton = button;
        ogBG = background;
        ogLogic = logic;
    }

    public void MenuUp()
    {
        if (!HasAnimPlayed)
        {
            Debug.Log("Calling Anim");
            button.gameObject.GetComponent<Animator>().SetTrigger("MenuUp");
            background.gameObject.GetComponent<Animator>().SetTrigger("MenuUp");
            logic.gameObject.GetComponent<Animator>().SetTrigger("MenuUp");

            StartCoroutine(WaitForAnimToFinish());
        }
        else
        {
            MenuDown();
        }
    }
    public void MenuDown()
    {
        button.gameObject.GetComponent<Animator>().SetTrigger("MenuDown");
        background.gameObject.GetComponent<Animator>().SetTrigger("MenuDown");
        logic.gameObject.GetComponent<Animator>().SetTrigger("MenuDown");

        StartCoroutine(DisableAnim());

        HasAnimPlayed = false;
        button.anchoredPosition = ogButton.anchoredPosition;
        background.anchoredPosition = ogBG.anchoredPosition;
        logic.anchoredPosition = ogLogic.anchoredPosition;
    }

    IEnumerator DisableAnim()
    {
        yield return new WaitForSeconds(1f);
        button.gameObject.GetComponent<Animator>().Play("Idle");
        background.gameObject.GetComponent<Animator>().Play("Idle");
        logic.gameObject.GetComponent<Animator>().Play("Idle");
    }

    IEnumerator WaitForAnimToFinish()
    {
        yield return new WaitForSeconds(1f);
        HasAnimPlayed = true;

        button.anchoredPosition = newPosButton.anchoredPosition;
        background.anchoredPosition = newPosBackground.anchoredPosition;
        logic.anchoredPosition = newPosLogic.anchoredPosition;
    }
}
