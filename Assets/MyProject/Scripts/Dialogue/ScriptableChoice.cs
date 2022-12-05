using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Choice", menuName = "Scriptables/Choice")]
public class ScriptableChoice : ScriptableObject
{
    public string choiceLine;
    public ScriptableDialogue followingDialogue;
}
