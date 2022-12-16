using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class CookingPot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] GameObject glowEffect;
    Animator glowAnimator;

    [SerializeField] List<SO_Ingredient> ingredients = new List<SO_Ingredient>();
    [SerializeField] int ingredientsLimit = 10;
    [SerializeField] TextMeshProUGUI currentIngredients;

    public int curMana = 0;
    public int curHealth = 0;
    public int curPower = 0;

    SO_Ingredient lastIngredient;

    [SerializeField] RecipeBoard recipeBoard;
    public SO_Recipe currentRecipe { get; private set; }


    Coroutine delayCoroutine, delayBubbleCoroutine;

    public delegate void OnIngredientsChanged();
    public OnIngredientsChanged onIngredientsChangedCallback;

    private void Start()
    {
        currentRecipe = recipeBoard.recipe;
        glowAnimator = glowEffect.GetComponent<Animator>();

        Debug.Log(ingredientsLimit + " this is the current max limit of ingredients");
        if (onIngredientsChangedCallback != null)
        {
            onIngredientsChangedCallback.Invoke();
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Dropped: " + eventData.pointerDrag);

        //SFX
        if (glowEffect != null)
        {
            glowAnimator.SetTrigger("EndGlow");
            delayCoroutine = StartCoroutine(DelayUntilDeactivation());
        }

        if (ingredients.Count < ingredientsLimit)
        {
            eventData.pointerDrag.gameObject.transform.SetParent(eventData.pointerDrag.GetComponent<CookIngredient>().AfterOnTheke);
            UpdateUIText();
            if (!eventData.pointerDrag.GetComponent<CookIngredient>().HasBeenOnTheke)
            {
                ingredients.Add(eventData.pointerDrag.GetComponent<CookIngredient>().ingredient);

                curHealth += eventData.pointerDrag.GetComponent<CookIngredient>().ingredient.health;
                curMana += eventData.pointerDrag.GetComponent<CookIngredient>().ingredient.mana;
                curPower += eventData.pointerDrag.GetComponent<CookIngredient>().ingredient.power;
                if (onIngredientsChangedCallback != null)
                {
                    onIngredientsChangedCallback.Invoke();
                }
            }
            lastIngredient = eventData.pointerDrag.GetComponent<CookIngredient>().ingredient;
        }
        else
        {
            Destroy(eventData.pointerDrag);
        }
        if (currentRecipe.ContainsRecipe(ingredients))
        {
            //animTimer = 2;
            Debug.Log("Contains recipe");
            //ruehrstabAnimator.Play(ruehrAnim);
            //animPlaying = true;
        }

        
        //eventData.pointerDrag.GetComponent<CookIngredient>().SizeDown();
        eventData.pointerDrag.GetComponent<CookIngredient>().HasBeenOnTheke = true;
        eventData.pointerDrag.GetComponent<CookIngredient>().IsOnTheke = true;
    }

    private void UpdateUIText()
    {
        currentIngredients.SetText((ingredients.Count + 1) + "/" + ingredientsLimit);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (CheckForCookIngredientOnPot(eventData))
        {
            if(glowEffect != null)
            {
                if(delayCoroutine != null)
                {
                    StopCoroutine(delayCoroutine);
                    glowAnimator.Play("GlowAnim");
                }
                
                glowEffect.SetActive(true);
            }
            //eventData.pointerDrag.GetComponent<CookIngredient>().SizeUp();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (CheckForCookIngredientOnPot(eventData))
        {
            if (glowEffect != null)
            {
                glowAnimator.SetTrigger("EndGlow");
                delayCoroutine = StartCoroutine(DelayUntilDeactivation());
            }
            
            //eventData.pointerDrag.GetComponent<CookIngredient>().SizeDown();
        }
    }

    IEnumerator DelayUntilDeactivation()
    {
        yield return new WaitForSeconds(1f);
        glowEffect.SetActive(false);
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

    bool IngredientHasBeenAddedBefore()
    {
        return true;
    }

    public void RemoveItem(SO_Ingredient _ingredient)
    {
        List<SO_Ingredient> _tempList = new List<SO_Ingredient>();
        bool itemDeleted = false;

        foreach(var item in ingredients)
        {
            if (item.CompareIngredient(_ingredient) && !itemDeleted)
            {
                itemDeleted = true;
            }
            else
            {
                _tempList.Add(item);
            }
        }

        ingredients = new List<SO_Ingredient>();
        ingredients = _tempList;
        UpdateUIText();
    }
}
