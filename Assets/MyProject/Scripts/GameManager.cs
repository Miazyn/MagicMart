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
    [Range(0.1f, 10.0f)]
    [SerializeField] float TimeBtwCustomers = 2.0f;

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
            counter++;
            //DELAY HERE
            StartCoroutine(CustomerSpacingDelay());
        }
        
        if (curState == GameState.DialogState)
        {
            dialogueManager = DialogueManager.instance;
            if (counter < CustomerCount - 1)
            {
                dialogueManager.SetUpDialog(Customers[counter].quests[0].QuestDialogBeforeCompletion[0], Customers[0]);

            }
            else
            {
                //END OF THE DAY
            }
        }
        #region[Inactive Checks]
        if (curState == GameState.CookingState)
        {
        }
        if (curState == GameState.MiniRhythmGameState)
        {
        }
        if (curState == GameState.ShoppingState)
        {
        }
        if (curState == GameState.IdleState)
        {

        }
        #endregion
        if (curState == GameState.EvaluationState)
        {
            dialogueManager = DialogueManager.instance;
            Evaluation();
        }
        
    }

    IEnumerator CustomerSpacingDelay()
    {
        yield return new WaitForSeconds(TimeBtwCustomers);
        ChangeGameState(GameState.DialogState);
    }

    void Evaluation()
    {
        //Create Food For Player with stats for npc
        
        AfterQuestDialog();
    }

    public void AfterQuestDialog()
    {
        dialogueManager.SetUpDialog(Customers[counter].quests[0].QuestDialogAfterCompletion[0], Customers[0]);
    }

}
