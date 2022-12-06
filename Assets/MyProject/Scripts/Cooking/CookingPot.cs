using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CookingPot : MonoBehaviour, IDropHandler
{

    [SerializeField] List<SO_Ingredient> ingredients = new List<SO_Ingredient>();
    int counter = 0;

    [SerializeField] RecipeBoard currentRecipe;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Dropped: " + eventData.pointerDrag);

        ingredients.Add(eventData.pointerDrag.GetComponent<CookIngredient>().ingredient);
        if (ingredients.Count == currentRecipe.recipe.ingredients.Length)
        {
            CheckRecipe();
        }
        counter++;
        Destroy(eventData.pointerDrag);
    }

    void CheckRecipe()
    {
        foreach(var item in currentRecipe.recipe.ingredients)
        {
            for (int i = 0; i < ingredients.Count; i++)
            {
                if (ingredients[i].ingredientName != item.ingredientName)
                {
                    Debug.Log("Not same ingredient: " + ingredients[i] + "recipe was: " + item);
                }
                Debug.Log("Same ingredient: " + ingredients[i] + "recipe is: " + item);

            }
        }
       // Debug.Log("Same recipe");
    }
}
