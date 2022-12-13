using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeBoard : MonoBehaviour
{
    GameManager manager;
    public SO_Recipe recipe { get; private set; }

    [SerializeField] GameObject recipeBoard;
    [SerializeField] public GameObject recipeItemPrefab;

    void Start()
    {
        manager = GameManager.Instance;
        recipe = manager.Customers[manager.counter].quests[0].ReqRecipe;
        CreateRecipeBoard();
    }

    public void CreateRecipeBoard()
    {
        int count = recipe.ingredients.Length;
        Debug.Log(count);
        for(int i = 0; i < count; i++)
        {
            GameObject curItem = Instantiate(recipeItemPrefab, recipeBoard.transform);
            curItem.GetComponent<RecipeIngredientSlot>().heldIngredient = recipe.ingredients[i];
            Debug.Log("Made Ingredient");
        }
    }
}
