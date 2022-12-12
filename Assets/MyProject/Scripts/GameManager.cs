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
        curState = GameState.IdleState;
        
    }

    public void ChangeGameState(string _newGameState)
    {
         curState = (GameState)System.Enum.Parse(typeof(GameState), _newGameState);
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
