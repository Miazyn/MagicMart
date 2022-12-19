using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeTransitions : MonoBehaviour
{
    [SerializeField] GameObject clickToContinue;
    [SerializeField] SceneMana sceneMana;
    GameManager manager;
    void Start()
    {
        manager = GameManager.Instance;
        StartCoroutine(LateStart());
    }

    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(2f);
        clickToContinue.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //ASSIGN WHAT IS NEEDED
            if (manager.curState == GameManager.GameState.CookingState)
            {
                sceneMana.LoadNextScene("Shopkeeper");
            }
        }
    }
}
