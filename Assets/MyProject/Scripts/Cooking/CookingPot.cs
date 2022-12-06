using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CookingPot : MonoBehaviour, IDropHandler
{

    [SerializeField] List<SO_Ingredient> ingredients = new List<SO_Ingredient>();
    SO_Ingredient lastIngredient;
    int counter = 0;

    [SerializeField] RecipeBoard currentRecipe;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Dropped: " + eventData.pointerDrag);

        ingredients.Add(eventData.pointerDrag.GetComponent<CookIngredient>().ingredient);
        lastIngredient = eventData.pointerDrag.GetComponent<CookIngredient>().ingredient;
        if (!IngredientCheck())
        {
            Debug.Log("invalid ingredient");
        }

        if (ingredients.Count == currentRecipe.recipe.ingredients.Length)
        {
            if (CheckRecipe())
            {
                Debug.Log("Same recipe");
            }
            else
            {
                Debug.Log("Not same recipe");
            }
        }
        counter++;
        Destroy(eventData.pointerDrag);
    }

    bool IngredientCheck()
    {
        bool foundIngredient = false;
        foreach(var item in currentRecipe.recipe.ingredients)
        {
            if (lastIngredient.CompareIngredient(item))
            {
                foundIngredient = true;
            }
        }
        return foundIngredient;
    }

    bool CheckRecipe()
    {
        foreach (var item in currentRecipe.recipe.ingredients)
        {
            bool foundIngredient = false;
            for (int i = 0; i < ingredients.Count; i++)
            {
                if (ingredients[i].CompareIngredient(item))
                {
                    foundIngredient = true;
                }
            }
            if (!foundIngredient)
            {
                return false;
            }
        }
        return true;
    }
}
