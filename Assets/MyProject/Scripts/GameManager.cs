using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    Player player;
    //[Header("Dialog")]
    //DialogueManager dialogManager;
    //[SerializeField] SO_Dialog currentDialog;
    public int CustomerCounter = 0;

    public SO_NPC[] Customers;
    public SO_Recipe CurrentRecipe;
    public int day = 1;
    public int ExpectedCustomerAmount;

    [Header("Scores")]
    public float RhythymGameScore;
    public float CookingGameScore;
    public float OverallScore;
    [SerializeField] AudioSource moneySound;

    [Header("All Corelated Scripts")]
    [SerializeField] DialogueManager dialogueManager;

    AudioSource gameplayMusic;

    SO_CookedFood resultFood;

    public delegate void OnStateChanged();
    public OnStateChanged onStateChangedCallback;
    
    public delegate void OnNextCustomer();
    public OnNextCustomer onNextCustomerCallback;

    public delegate void OnDayChanged();
    public OnDayChanged onDayChangedCallback;
    public enum GameState
    {
        DayStart,
        StartState,
        DialogState,
        CookingState,
        MiniRhythmGameState,
        ShoppingState,
        IdleState,
        EvaluationState,
        AfterDialog
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
        player = Player.instance;
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
        if (curState == GameState.DayStart) 
        {
            CustomerCounter = 0;
            if (onDayChangedCallback != null)
            {
                onDayChangedCallback.Invoke();
            }
        }
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
            if (CustomerCounter < ExpectedCustomerAmount)
            {
                CurrentRecipe = Customers[CustomerCounter].quests[0].ReqRecipe;
                dialogueManager.SetUpDialog(Customers[CustomerCounter].quests[0].QuestDialogBeforeCompletion[0], Customers[CustomerCounter], Customers[CustomerCounter].quests[0].ReqRecipe);
            }
            else
            {
                //END OF THE DAY
                Debug.Log("End of the day");
                day++;
                ChangeGameState(GameState.DayStart);
            }
        }

        #region[Inactive Checks]
        //if (curState == GameState.MiniRhythmGameState)
        //{
        //    //StartCoroutine(TurnOffMusic());

        //}
        //if (curState == GameState.CookingState)
        //{
        //}
        //if (curState == GameState.ShoppingState)
        //{
        //}
        //if (curState == GameState.IdleState)
        //{

        //}
        #endregion
        if (curState == GameState.EvaluationState)
        {
            Debug.Log("Eval");
            ResetMusic();
            Evaluation();
        }
        if(curState == GameState.AfterDialog)
        {
            dialogueManager = DialogueManager.instance;

            moneySound = GameObject.Find("MoneySound").GetComponent<AudioSource>();
            player.SetMoneyAmount(MoneyForPlayer());
            moneySound.Play();
            AfterQuestDialog();
        }
        
    }

    int MoneyForPlayer()
    {
        if(OverallScore > 80)
        {
            return CurrentRecipe.perfectSellPrice;
        }

        if(OverallScore > 50)
        {
            return CurrentRecipe.goodSellPrice;

        }

        if(OverallScore > 20)
        {
            return CurrentRecipe.normalSellPrice;
        }

        return CurrentRecipe.terribleSellPrice;

    }

    public void ResetMusic()
    {
        gameplayMusic.volume = originalVolume;
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

        //AfterQuestDialog();
    }

    public void AfterQuestDialog()
    {
        dialogueManager.SetUpDialog(Customers[CustomerCounter].quests[0].QuestDialogAfterCompletion[0], Customers[CustomerCounter], Customers[CustomerCounter].quests[0].ReqRecipe);
    }

}
