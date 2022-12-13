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
    int counter = 0;

    public SO_NPC[] Customers;
    public int day;
    public int CustomerCount;

    [Header("Scores")]
    public float RhythymGameScore;
    public float CookingGameScore;
    public float OverallScore;

    [Header("All Corelated Scripts")]
    DialogueManager dialogueManager;

    SO_CookedFood resultFood;

    public enum GameState
    {
        DialogState,
        CookingState,
        MiniRhythmGameState,
        ShoppingState,
        IdleState
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
        curState = GameState.IdleState;
    }


    public void ChangeGameState(GameState _newState)
    {
        curState = _newState;
        CheckGameStateAction();
    }

    public void CheckGameStateAction()
    {
        if(curState == GameState.DialogState)
        {
            Debug.Log("Time to start some dialog");
            if (counter < CustomerCount - 1)
            {
                dialogueManager.SetUpDialog(Customers[counter].quests[0].QuestDialogBeforeCompletion[0]);
            }
            else
            {
                Debug.Log("No more dialog, time to swap states");
            }
        }
        else if(curState == GameState.IdleState)
        {
            Debug.Log("Time to be idle");
        }
        else if(curState == GameState.CookingState)
        {
            Debug.Log("Time to start Cooking");
        }
        else if(curState == GameState.MiniRhythmGameState)
        {
            Debug.Log("Time to rhythm");
        }
        else if(curState == GameState.ShoppingState)
        {
            Debug.Log("Time to shop");
        }
    }
}
