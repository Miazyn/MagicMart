using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMana : MonoBehaviour
{
    [SerializeField] Animator FadePanel;
    GameManager manager;

    void Start()
    {
        manager = GameManager.Instance;
        StartCoroutine(WaitForFadeIn());
    }

    public void LoadNextScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName, LoadSceneMode.Single);

        //Debug.Log("Load next scene " + _sceneName);
        //FadePanel.gameObject.SetActive(true);
        //FadePanel.SetTrigger("FadeOut");
        //StartCoroutine(WaitForFade(_sceneName));
    }

    IEnumerator WaitForFade(string _sceneName)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(_sceneName, LoadSceneMode.Single);
    }

    IEnumerator WaitForFadeIn()
    {
        yield return new WaitForSeconds(1f);
        FadePanel.gameObject.SetActive(false);
    }
}
