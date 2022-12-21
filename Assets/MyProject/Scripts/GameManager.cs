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

    AudioSource gameplayMusic;

    SO_CookedFood resultFood;

    public delegate void OnStateChanged();
    public OnStateChanged onStateChangedCallback;
    
    public delegate void OnNextCustomer();
    public OnNextCustomer onNextCustomerCallback;
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

    float originalVolume;

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
        gameplayMusic = GetComponent<AudioSource>();
        dialogueManager = DialogueManager.instance;
        curState = GameState.DayStart;
        originalVolume = gameplayMusic.volume;
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
            StartCoroutine(CustomerSpacingDelay());
        }
        
        if (curState == GameState.DialogState)
        {
            if(onNextCustomerCallback != null)
            {
                onNextCustomerCallback.Invoke();
            }

            dialogueManager = DialogueManager.instance;
            if (counter < CustomerCount)
            {
                dialogueManager.SetUpDialog(Customers[counter].quests[0].QuestDialogBeforeCompletion[0], Customers[counter], Customers[counter].quests[0].ReqRecipe);

            }
            else
            {
                //END OF THE DAY
                Debug.Log("End of the day");
            }
        }

        #region[Inactive Checks]
        if (curState == GameState.MiniRhythmGameState)
        {
            //StartCoroutine(TurnOffMusic());


        }
        if (curState == GameState.CookingState)
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
            gameplayMusic.volume = originalVolume;
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
        float maxScore = 200;

        float playerScore = RhythymGameScore + CookingGameScore;
        float overall = playerScore / (maxScore / 100);

        OverallScore = overall;
        Debug.Log("OVERALL SCORE: " + OverallScore);

        AfterQuestDialog();
    }

    public void AfterQuestDialog()
    {
        dialogueManager.SetUpDialog(Customers[counter].quests[0].QuestDialogAfterCompletion[0], Customers[counter], Customers[counter].quests[0].ReqRecipe);
    }

}
