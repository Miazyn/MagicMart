using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    [SerializeField] GameObject textBox;
    [SerializeField] TextMeshProUGUI nameBox;
    [SerializeField] TextMeshProUGUI dialogueBox;
    [SerializeField] float typingSpeedInSeconds = 0.05f;

    //Choices
    [SerializeField] GameObject choiceBottom;
    [SerializeField] GameObject choiceMiddle;
    [SerializeField] GameObject choiceLow;

    Coroutine displayCoroutine;
    public bool typerRunning = false;

    private void Awake()
    {
        Instance = this;
        WarnCheck();
    }

    private void WarnCheck()
    {
        if (!textBox)
        {
            Debug.LogWarning("No Default TextBox is defined.");
        }
        if (!nameBox)
        {
            Debug.LogWarning("No Default NameBox is defined.");

        }
        if (!dialogueBox)
        {
            Debug.LogWarning("No Default DialogueBox is defined.");
        }
    }

    public void TextReceived(ScriptableDialogue dialogue, int counter)
    {
        nameBox.text = dialogue.nameOfSpeaker;
        if (!typerRunning)
        {
            if (dialogue != null || dialogue.lines[counter] != "")
            {
                textBox.SetActive(true);
            }
            if (counter > dialogue.lines.Count - 1 && dialogue.dialogueChoices.Count == 0)
            {
                textBox.SetActive(false);
            }
            //else if (counter > dialogue.lines.Count - 1 && dialogue.dialogueChoices.Count != 0)
            //{
            //    //Code how to handle choices.
            //    dialogueBox.text = dialogue.dialogueChoices[0].choiceLine;
            //}
            else
            {
                if (displayCoroutine != null)
                {
                    StopCoroutine(TypeEffect(dialogue.lines[counter]));
                }
                
                displayCoroutine = StartCoroutine(TypeEffect(dialogue.lines[counter]));
            }
        }
        
    }

    public IEnumerator TypeEffect(string line)
    {
        typerRunning = true;
        dialogueBox.text = "";
        for (int i = 0; i < line.ToCharArray().Length; i++)
        {

            dialogueBox.text += line.ToCharArray()[i];
            yield return new WaitForSeconds(typingSpeedInSeconds);

        }
        typerRunning = false;
    }

    public void StopTypeEffect(ScriptableDialogue dialogue, int counter)
    {
        if (displayCoroutine != null)
        {
            StopCoroutine(displayCoroutine);
        }
        dialogueBox.text = dialogue.lines[counter - 1];
        typerRunning = false;

    }

    //public void SetUpChoices()
    //{
    //    Debug.Log("Setting up choices");
    //}

    //public void FindNextDialogue(ChoiceHolder choiceHolder)
    //{
    //    ScriptableChoice choice = choiceHolder.choiceHeld;
    //    if(choice.followingDialogue != null)
    //    {
    //        Debug.Log(choice.followingDialogue.lines[0]);
    //    }
    //}

    
}
