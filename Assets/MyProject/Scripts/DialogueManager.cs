using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [SerializeField] int maxCharPerLine = 10;

    [SerializeField] GameObject textBox;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] float typingSpeedInSeconds = 0.05f;

    Sprite savedSprite;
    [SerializeField] GameObject npc;

    Coroutine displayCoroutine;
    public bool typerRunning = false;

    Player player;
    GameManager manager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        player = Player.instance;
        manager = GameManager.Instance;
        WarnCheck();
    }
    private void WarnCheck()
    {
        if (!textBox)
        {
            Debug.LogWarning("No Default TextBox is defined.");
        }
        if (!nameText)
        {
            Debug.LogWarning("No Default NameBox is defined.");

        }
        if (!dialogueText)
        {
            Debug.LogWarning("No Default DialogueBox is defined.");
        }
        if (!npc)
        {
            Debug.LogWarning("No Default SpriteBox is defined.");
        }
    }

    public void TextReceived(SO_Dialog dialogue, int counter)
    {
        DisplayCharacterSprite(dialogue, counter);
        nameText.text = dialogue.nameOfSpeaker;
        if (!typerRunning)
        {
            if (dialogue != null || dialogue.lines[counter] != "")
            {
                //SET ACTIVE
            }
            if (counter > dialogue.lines.Count - 1 && dialogue.dialogueChoices.Count == 0)
            {
                EndDialog();
            }
            else if (counter > dialogue.lines.Count - 1 && dialogue.dialogueChoices.Count != 0)
            {
                Debug.Log("Here would be logic for a dialog");
                EndDialog();
                //Code how to handle choices.
               // dialogueBox.text = dialogue.dialogueChoices[0].choiceLine;
            }
            else
            {
                string line = "";
                if (dialogue.lines[counter].Contains("$playerName")) 
                {
                    line = dialogue.lines[counter].Replace("$playerName", player.PlayerName);
                }
                else
                {
                    line = dialogue.lines[counter];
                }

                line = CheckStringForLineBreak(line);

                if (displayCoroutine != null)
                {
                    StopCoroutine(TypeEffect(line));
                }

                displayCoroutine = StartCoroutine(TypeEffect(line));
            }
        }

    }

    void EndDialog()
    {
        //SET INACTIVE
    }

    public IEnumerator TypeEffect(string line)
    {
        typerRunning = true;
        dialogueText.text = "";
        for (int i = 0; i < line.ToCharArray().Length; i++)
        {

            dialogueText.text += line.ToCharArray()[i];
            yield return new WaitForSeconds(typingSpeedInSeconds);

        }
        typerRunning = false;
    }

    public void StopTypeEffect(SO_Dialog dialogue, int counter)
    {
        if (displayCoroutine != null)
        {
            StopCoroutine(displayCoroutine);
        }
        dialogueText.text = dialogue.lines[counter - 1];
        typerRunning = false;

    }

    public void DisplayCharacterSprite(SO_Dialog dialogue, int counter)
    {
        if (dialogue.keyForCharacterDisplay != null && dialogue.spriteForCharacterDisplay != null)
        {
            for (int i = 0; i < dialogue.keyForCharacterDisplay.Count; i++)
            {
                if (dialogue.keyForCharacterDisplay[i] == counter)
                {
                    savedSprite = dialogue.spriteForCharacterDisplay[i];
                    npc.GetComponent<Image>().sprite = savedSprite;
                    npc.SetActive(true);

                    break;
                }
                else
                {
                    if (savedSprite)
                    {
                        npc.GetComponent<Image>().sprite = savedSprite;
                    }
                    else
                    {
                        Debug.LogWarning("THERE IS NO SPRITE FOR THIS CHARACTER YET SET.");
                    }
                }
            }

        }
    }

    public string CheckStringForLineBreak(string line)
    {
        char[] a = line.ToCharArray();
        int counter = 0;
        int curCheck = 0;

        List<int> spaces = new List<int>();

        for (int i = 1; i <= a.Length / maxCharPerLine; i++)
        {
            curCheck = i * maxCharPerLine;
            Debug.Log(curCheck);
            bool addedSpace = false;
            for (int j = 0; j < curCheck; j++)
            {
                if (a[j] == ' ')
                {
                    if (addedSpace)
                    {
                        spaces[counter] = j;
                    }
                    else
                    {
                        spaces.Add(j);
                        addedSpace = true;
                    }
                }
            }
            counter++;
        }
        string newString = "";
        for (int y = 0; y < a.Length; y++)
        {
            bool addSpace = false;
            for (int x = 0; x < spaces.Count; x++)
            {
                if (y == spaces[x])
                {
                    addSpace = true;
                }
            }
            if (addSpace)
            {
                if (a[y] != ' ')
                {
                    newString += "\n" + a[y].ToString();

                }
                else
                {
                    newString += "\n";
                }
            }
            else
            {
                newString += a[y].ToString();
            }
        }
        return newString;
    }
}