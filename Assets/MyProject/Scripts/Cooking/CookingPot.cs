using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class CookingPot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] List<SO_Ingredient> ingredients = new List<SO_Ingredient>();
    SO_Ingredient lastIngredient;
    int counter = 0;

    [SerializeField] Animator ruehrstabAnimator;
    string ruehrAnim = "MixingAnimation";

    [SerializeField] RecipeBoard recipeBoard;
    SO_Recipe currentRecipe;

    bool animPlaying = false;

    float animTimer = 2;

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

        if (currentRecipe.ContainsRecipe(ingredients))
        {
            animTimer = 2;
            Debug.Log("Contains recipe");
            ruehrstabAnimator.Play(ruehrAnim);
            animPlaying = true;
            StartCoroutine(loadNextScene());
        }

        counter++;
        Destroy(eventData.pointerDrag);
    }

    IEnumerator loadNextScene()
    {
        while (animPlaying)
        {
            animTimer--;
            if(animTimer <= 0)
            {
                animPlaying = false;
            }
            yield return new WaitForSeconds(1.2f);
        }
        SceneManager.LoadScene("RhythmMiniGame", LoadSceneMode.Single);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (CheckForCookIngredientOnPot(eventData))
        {
            eventData.pointerDrag.GetComponent<CookIngredient>().SizeUp();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (CheckForCookIngredientOnPot(eventData))
        {
            eventData.pointerDrag.GetComponent<CookIngredient>().SizeDown();
        }
    }

    bool CheckForCookIngredientOnPot(PointerEventData _eventData)
    {
        if (_eventData.pointerDrag != null)
        {
            if (_eventData.pointerDrag.GetComponent<CookIngredient>() != null)
            {
                return true;
            }
            return false;
        }

        return false;
    }
}
