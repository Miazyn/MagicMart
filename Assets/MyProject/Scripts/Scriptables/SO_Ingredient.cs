using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ingredient", menuName = "Cooking/Ingredient")]
public class SO_Ingredient : ScriptableObject
{
    public string ingredientName;
    public Sprite ingredientSprite;
    public int health;
    public int mana;

    public int BuyPrice;

    [TextArea(5, 10)]
    public string ingredientDescription;

    public bool CompareIngredient(SO_Ingredient compareTo)
    {
        if (ingredientName == compareTo.ingredientName)
        {
            return true;
        }
        return false;
    }

   
}
