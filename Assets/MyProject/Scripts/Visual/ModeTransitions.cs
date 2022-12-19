using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeTransitions : MonoBehaviour
{
    [SerializeField] GameObject clickToContinue;
    [SerializeField] SceneMana sceneMana;
    GameManager manager;

    public Animator Pot;
    public Animator Arrow;
    public Animator Star;
    void Start()
    {
        manager = GameManager.Instance;
        StartCoroutine(LateStart());

        //DISPLAY CORRECT ANIM
        if(manager.curState == GameManager.GameState.IdleState)
        {
            //Into Cooking
            Pot.enabled = true;
        }
        if(manager.curState == GameManager.GameState.CookingState)
        {
            //Into Rhythm
            Arrow.enabled = true;
        }
        if(manager.curState == GameManager.GameState.MiniRhythmGameState)
        {
            //Into Evaluation
            Star.enabled = true;
        }
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
            if (manager.curState == GameManager.GameState.IdleState)
            {
                sceneMana.LoadNextScene("CookingArea");
            }
            if (manager.curState == GameManager.GameState.CookingState)
            {
                //Into Rhythm
                sceneMana.LoadNextScene("RhythmMiniGame");
            }
            if (manager.curState == GameManager.GameState.MiniRhythmGameState)
            {
                //Into Evaluation
                sceneMana.LoadNextScene("MainStore");
            }
        }
    }
}
