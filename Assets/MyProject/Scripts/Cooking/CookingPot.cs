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
    SO_Ingredient[] recipeToDo;
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

        CookIngredient currentIngredient = eventData.pointerDrag.GetComponent<CookIngredient>();
        //SFX
        if (glowEffect != null)
        {
            glowAnimator.SetTrigger("EndGlow");
            delayCoroutine = StartCoroutine(DelayUntilDeactivation());
        }

        if (ingredients.Count < ingredientsLimit)
        {
            eventData.pointerDrag.gameObject.transform.SetParent(currentIngredient.AfterOnTheke);
            UpdateUIText();
            if (!currentIngredient.HasBeenOnTheke)
            {
                ingredients.Add(currentIngredient.ingredient);

                if (currentRecipe.IsValidIngredient(currentIngredient.ingredient))
                {
                    SO_Ingredient[] _tempArray;
                    if(recipeToDo == null)
                    {
                        recipeToDo = new SO_Ingredient[1];
                        recipeToDo[0] = currentIngredient.ingredient;
                    }
                    else
                    {
                        bool found = false;
                        for(int i = 0; i < recipeToDo.Length; i++)
                        {
                            if (recipeToDo[i].CompareIngredient(currentIngredient.ingredient))
                            {
                                found = true;
                                Debug.Log("Ingredient alrdy here");
                            }
                        }
                        if (!found)
                        {
                            _tempArray = new SO_Ingredient[recipeToDo.Length + 1];
                            Debug.Log("Code to do with new ingredient" + _tempArray.Length);

                            for (int i = 0; i < _tempArray.Length - 1; i++)
                            {
                                _tempArray[i] = recipeToDo[i];
                            }
                            _tempArray[recipeToDo.Length] = currentIngredient.ingredient;
                        }
                    }
                }
                else
                {
                    AddToScore(currentIngredient);
                }


                if (onIngredientsChangedCallback != null)
                {
                    onIngredientsChangedCallback.Invoke();
                }
            }
            lastIngredient = currentIngredient.ingredient;
        }
        else
        {
            Destroy(eventData.pointerDrag);
        }
        //if (currentRecipe.ContainsRecipe(ingredients))
        //{
        //    Debug.Log("Contains recipe");
        //}

        
        //eventData.pointerDrag.GetComponent<CookIngredient>().SizeDown();
        currentIngredient.HasBeenOnTheke = true;
        currentIngredient.IsOnTheke = true;
    }

    private void AddToScore(CookIngredient currentIngredient)
    {
        curHealth += currentIngredient.ingredient.health;
        curMana += currentIngredient.ingredient.mana;
        curPower += currentIngredient.ingredient.power;
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
