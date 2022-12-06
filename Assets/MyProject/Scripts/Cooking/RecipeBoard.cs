using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeBoard : MonoBehaviour
{

    public SO_Recipe recipe;

    [SerializeField] GameObject recipeBoard;
    [SerializeField] public GameObject recipeItemPrefab;

    private void Start()
    {
        CreateRecipeBoard();
    }

    void CreateRecipeBoard()
    {
        int count = recipe.ingredients.Length;

        for(int i = 0; i < count; i++)
        {
            GameObject curItem = Instantiate(recipeItemPrefab, recipeBoard.transform);
            curItem.GetComponent<RecipeIngredientSlot>().heldIngredient = recipe.ingredients[i];
        }
    }
}
