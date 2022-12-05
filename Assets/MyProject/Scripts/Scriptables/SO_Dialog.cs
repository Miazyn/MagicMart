using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName ="Scriptables/Dialogue")]
public class SO_Dialog : ScriptableObject
{
    public string nameOfSpeaker;

    public List<int> keyForCharacterDisplay;
    public List<Sprite> spriteForCharacterDisplay;

    [TextArea(5,20)]
    public List<string> lines;

    public List<SO_Choice> dialogueChoices;
}
