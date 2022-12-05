using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    

    public ScriptableDialogue myDialog;
    int counter = 0;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (counter <= myDialog.lines.Count && !DialogueManager.Instance.typerRunning)
            {
                DialogueManager.Instance.TextReceived(myDialog, counter);
                counter++;
            }
            else if (DialogueManager.Instance.typerRunning)
            {
                DialogueManager.Instance.StopTypeEffect(myDialog, counter);
            }

            //if(counter > myDialog.lines.Count && myDialog.dialogueChoices.Count != 0)
            //{
            //    Debug.Log("Need to recall my function with new dialog lines!");
            //    DialogueManager.Instance.SetUpChoices();
            //}
        }
    }
}
