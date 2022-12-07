using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueManager))]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Dialog")]
    DialogueManager dialogManager;
    [SerializeField] SO_Dialog currentDialog;
    int counter = 0;

    public delegate void OnItemMenuToggle();
    public OnItemMenuToggle onItemMenuToggleCallback;


    private void Awake()
    {
        Instance = this;
        dialogManager = GetComponent<DialogueManager>();

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
            //InteractionDialogue();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Close menus
        }

        
    }

    void InteractionDialogue()
    {
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
