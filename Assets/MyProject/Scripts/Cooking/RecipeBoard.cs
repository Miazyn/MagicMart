using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeBoard : MonoBehaviour
{
    GameManager manager;
    public SO_Recipe recipe;

    [SerializeField] GameObject recipeBoard;
    [SerializeField] public GameObject recipeItemPrefab;


    void Start()
    {
        manager = GameManager.Instance;
        if (manager.Customers.Length > 0)
        {
            recipe = manager.Customers[manager.counter].quests[0].ReqRecipe;
        }
        else
        {
            Debug.Log("No Recipe here owo");
        }

        CreateRecipeBoard();
    }

    public void CreateRecipeBoard()
    {
        int count = recipe.ingredients.Length;
        for(int i = 0; i < count; i++)
        {
            GameObject curItem = Instantiate(recipeItemPrefab, recipeBoard.transform);
            curItem.GetComponent<RecipeIngredientSlot>().heldIngredient = recipe.ingredients[i];
        }
    }
}
