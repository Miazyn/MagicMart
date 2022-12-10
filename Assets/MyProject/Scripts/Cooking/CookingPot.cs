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

    [SerializeField] Animator ruehrstabAnimator;
    string ruehrAnim = "MixingAnimation";

    [SerializeField] RecipeBoard recipeBoard;
    SO_Recipe currentRecipe;

    private void Start()
    {
        currentRecipe = recipeBoard.recipe;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Dropped: " + eventData.pointerDrag);

        ingredients.Add(eventData.pointerDrag.GetComponent<CookIngredient>().ingredient);
        lastIngredient = eventData.pointerDrag.GetComponent<CookIngredient>().ingredient;

        if (!currentRecipe.IsValidIngredient(lastIngredient))
        {
            Debug.Log("invalid ingredient");
        }

        if (ingredients.Count == currentRecipe.ingredients.Length)
        {
            if (currentRecipe.ContainsRecipe(ingredients))
            {
                Debug.Log("Contains recipe");
                ruehrstabAnimator.Play(ruehrAnim);
            }
            else
            {
                Debug.Log("Not contains recipe");
            }
        }
        counter++;
        Destroy(eventData.pointerDrag);
    }

}
