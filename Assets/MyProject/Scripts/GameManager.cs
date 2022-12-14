using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    Player player;
    public int CustomerCounter = 0;

    public SO_NPC[] Customers;
    public SO_Recipe CurrentRecipe;

    public int day;
    public int ExpectedCustomerAmount;
    bool HasFinishedNPC;

    public int[] QuestID;
    public List<SO_Quest> storyQuests { get; private set; }

    [Header("Scores")]
    public float RhythmGameScore;
    public float CookingGameScore;
    public float OverallScore;
    [SerializeField] AudioSource moneySound;

    [Header("All Correlated Scripts")]
    [SerializeField] DialogueManager dialogueManager;

    AudioSource gameplayMusic;


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

    void Awake()
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
        HasFinishedNPC = false;
        day = 1;
        player = Player.instance;
        gameplayMusic = GetComponent<AudioSource>();
        dialogueManager = DialogueManager.instance;
        curState = GameState.DayStart;
        originalVolume = gameplayMusic.volume;
        storyQuests = new List<SO_Quest>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            SaveData();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (SaveSystem.HasSaveData())
            {
                LoadData();
            }
        }
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
            StartNextDay();
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
                ChangeGameState(GameState.DayStart);
            }
        }
        if (curState == GameState.EvaluationState)
        {
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
    void StartNextDay()
    {
        day++;
        CustomerCounter = 0;
        HasFinishedNPC = false;
        if (onDayChangedCallback != null)
        {
            onDayChangedCallback.Invoke();
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
        float maxScore = 200;

        float playerScore = RhythmGameScore + CookingGameScore;
        float overall = playerScore / (maxScore / 100);

        OverallScore = overall;


        if (IsAnNPC())
        {
            storyQuests.Add(Customers[CustomerCounter].quests[0]);
            HasFinishedNPC = true;
        }
    }
    public void AfterQuestDialog()
    {
        dialogueManager.SetUpDialog(Customers[CustomerCounter].quests[0].QuestDialogAfterCompletion[0], Customers[CustomerCounter], Customers[CustomerCounter].quests[0].ReqRecipe);
    }
    bool IsAnNPC()
    {
        switch (Customers[CustomerCounter].NpcName)
        {
            case "Claw":
                return true;
            case "Nyx":
                return true;
            case "Steve":
                return true;
            default:
                return false;
        }
    }
    public void OnSave()
    {
        QuestID = new int[storyQuests.Count];
        for(int i = 0; i < storyQuests.Count; i++)
        {
            QuestID[i] = storyQuests[i].QuestID;
        }
        storyQuests.Clear();
    }
    void SaveData()
    {
        OnSave();
        SaveSystem.SaveData(player, this);
    }
    public void LoadData()
    {
        SO_Quest[] _allQuests = Resources.LoadAll<SO_Quest>("Story Quest");
        Data _data = SaveSystem.LoadData();

        day = _data.InGameDay;
        storyQuests = new List<SO_Quest>();
        for(int i = 0; i < _data.QuestID.Length; i++)
        {
            foreach(var _quest in _allQuests)
            {
                if(_quest.QuestID == _data.QuestID[i])
                {
                    storyQuests.Add(_quest);
                    break;
                }
            }
        }

        player.LoadData();
    }

    void OnApplicationQuit()
    {
        SaveData();
    }
}
