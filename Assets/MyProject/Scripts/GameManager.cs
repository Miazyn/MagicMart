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
        curState = GameState.IdleState;
    }


    public void ChangeGameState(GameState _newState)
    {
        curState = _newState;
    }

    void InteractionDialogue(SO_Dialog currentDialog)
    {
        //if (counter <= currentDialog.lines.Count && !dialogManager.typerRunning)
        //{
        //    dialogManager.TextReceived(currentDialog, counter);
        //    counter++;
        //}
        //else if (dialogManager.typerRunning)
        //{
        //    dialogManager.StopTypeEffect(currentDialog, counter);
        //}
    }
}
