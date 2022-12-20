using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //[Header("Dialog")]
    //DialogueManager dialogManager;
    //[SerializeField] SO_Dialog currentDialog;
    public int counter = 0;

    public SO_NPC[] Customers;
    public SO_Recipe CurrentRecipe;
    public int day = 1;
    public int CustomerCount;

    [Header("Scores")]
    public float RhythymGameScore;
    public float CookingGameScore;
    public float OverallScore;

    [Header("All Corelated Scripts")]
    [SerializeField] DialogueManager dialogueManager;

    SO_CookedFood resultFood;

    bool delayStartDone = false;
    bool delayEndDone = false;

    public delegate void OnStateChanged();
    public OnStateChanged onStateChangedCallback;
    public enum GameState
    {
        DayStart,
        StartState,
        DialogState,
        CookingState,
        MiniRhythmGameState,
        ShoppingState,
        IdleState,
        EvaluationState
    }

    [SerializeField] public GameState curState { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        dialogueManager = DialogueManager.instance;
        curState = GameState.DayStart;
    }
    public void ChangeGameState(GameState _newState)
    {
        curState = _newState;
        if(onStateChangedCallback != null) 
        {
            onStateChangedCallback.Invoke();
        }
        CheckGameStateAction();
    }

    public void CheckGameStateAction()
    {
        if (curState == GameState.StartState)
        {
            Debug.Log("Time to start again");
            counter++;
            ChangeGameState(GameState.DialogState);
        }
        if (curState == GameState.IdleState)
        {
            Debug.Log("Time to be idle");
            
        }
        if (curState == GameState.DialogState)
        {
            Debug.Log("Time to start some dialog");
            Debug.Log(counter);
            dialogueManager = DialogueManager.instance;
            if (counter < CustomerCount - 1)
            {
                if (!delayStartDone)
                {
                    StartCoroutine(DelayBeforeDialog());
                }
                else
                {
                    Debug.Log("Customer:" + Customers[counter]);
                    dialogueManager.SetUpDialog(Customers[counter].quests[0].QuestDialogBeforeCompletion[0], Customers[0]);
                }
            }
            else
            {
                Debug.Log("No more dialog, Done with the day");
            }
        }
        if(curState == GameState.CookingState)
        {
            Debug.Log("Time to start Cooking");
        }
        if(curState == GameState.MiniRhythmGameState)
        {
            Debug.Log("Time to rhythm");
        }
        if(curState == GameState.ShoppingState)
        {
            Debug.Log("Time to shop");
        }
        if(curState == GameState.EvaluationState)
        {
            dialogueManager = DialogueManager.instance;
            Debug.Log("Time to Evaluate");
            Evaluation();
            
        }
    }

    IEnumerator DelayBeforeDialog()
    {
        yield return new WaitForSeconds(1.5f);
        delayStartDone = true;
        delayEndDone = false;
        dialogueManager.SetUpDialog(Customers[counter].quests[0].QuestDialogBeforeCompletion[0], Customers[counter]);
    }

    void Evaluation()
    {
        //Create Food For Player with stats for npc
        
        AfterQuestDialog();
    }

    public void AfterQuestDialog()
    {
        if (!delayEndDone)
        {
            StartCoroutine(DelayAfterDialog());
        }
        else
        {
            dialogueManager.SetUpDialog(Customers[counter].quests[0].QuestDialogAfterCompletion[0], Customers[counter]);
        }
    }

    IEnumerator DelayAfterDialog()
    {
        yield return new WaitForSeconds(1.5f);
        delayStartDone = false;
        delayEndDone = true;
        dialogueManager.SetUpDialog(Customers[counter].quests[0].QuestDialogAfterCompletion[0], Customers[counter]);
    }
}
