using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Cooking/Recipe")]
public class SO_Recipe : ScriptableObject
{
    public string recipeName;
    [TextArea(5,10)]
    public string description;
    public SO_Ingredient[] ingredients;
}
