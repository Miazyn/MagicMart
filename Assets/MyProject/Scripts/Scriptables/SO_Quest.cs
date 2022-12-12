using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quests/Create new quest")]
public class SO_Quest : ScriptableObject
{
    public string QuestName;
    public SO_Recipe ReqRecipe;

    public bool _IsFullFilled;
    public void SetQuestStatus(bool _isFullFilled)
    {
        _IsFullFilled = _isFullFilled;
    }
}
