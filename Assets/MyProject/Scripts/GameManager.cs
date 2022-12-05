using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] DialogueManager dialogManager;
    [SerializeField] SO_Dialog currentDialog;
    int counter = 0;

    private void Awake()
    {
        Instance = this;
        if (!dialogManager)
        {
            Debug.LogError("DialogSystem cannot function without DialogManager!!!");
        }
    }

    void Update()
    {
        //TRIGGER FOR DIALOGUE ++ get dialog
        if (Input.GetMouseButtonDown(0))
        {
            InteractionDialogue();
        }
    }

    void InteractionDialogue()
    {
        Debug.Log(counter);
        if (counter <= currentDialog.lines.Count && !dialogManager.typerRunning)
        {
            dialogManager.TextReceived(currentDialog, counter);
            counter++;
        }
        else if (dialogManager.typerRunning)
        {
            dialogManager.StopTypeEffect(currentDialog, counter);
        }

    }
}
