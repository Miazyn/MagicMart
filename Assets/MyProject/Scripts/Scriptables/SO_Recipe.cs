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

    public int health;
    public int mana;
    public (int totalHealth, int totalMana) CombineIngredients(SO_Ingredient[] ingredientsArray)
    {
        int fullHealth = 0;
        int fullMana = 0;
        for (int i = 0; i < ingredientsArray.Length - 1; i++)
        {
            ingredientsArray[i].health += fullHealth;
            ingredientsArray[i].mana += fullMana;
        }
        return (fullHealth, fullMana);
    }
    public (int healthReq, int manaReq) CheckRecipeRequirements() 
    {
        return (this.health, this.mana);
    }

    }
