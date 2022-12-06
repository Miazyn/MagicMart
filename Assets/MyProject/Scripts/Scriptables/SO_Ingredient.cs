using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ingredient", menuName = "Cooking/Ingredient")]
public class SO_Ingredient : ScriptableObject
{
    public string ingredientName;
    public Sprite ingredientSprite;
    //[TextArea(5,10)]
    //public string ingredientDescription
}
