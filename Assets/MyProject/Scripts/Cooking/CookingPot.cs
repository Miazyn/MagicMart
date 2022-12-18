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

    public float OverallScore = 0;

    SO_Ingredient lastIngredient;

    [SerializeField] RecipeBoard recipeBoard;
    [SerializeField] SO_Ingredient[] recipeToDo;
    public SO_Recipe currentRecipe;
    //AFTER DEBUG BACK INTO PRIVATE SET

    Coroutine delayCoroutine, delayBubbleCoroutine;

    public delegate void OnIngredientsChanged();
    public OnIngredientsChanged onIngredientsChangedCallback;

    private void Start()
    {
        currentRecipe = recipeBoard.recipe;
        glowAnimator = glowEffect.GetComponent<Animator>();

        ingredients = new List<SO_Ingredient>();

        Debug.Log(ingredientsLimit + " this is the current max limit of ingredients");
        if (onIngredientsChangedCallback != null)
        {
            onIngredientsChangedCallback.Invoke();
        }
        
    }

    public void DebugUpdateRecipe()
    {
        //FOR TESTING

        if (onIngredientsChangedCallback != null)
        {
            onIngredientsChangedCallback.Invoke();
        }
        recipeToDo = null;
        ingredients = new List<SO_Ingredient>();

        curHealth = 0;
        curMana = 0;
        curPower = 0;

        OverallScore = 0.0f;

        foreach (var item in GameObject.FindGameObjectsWithTag("Ingredient"))
        {
            Destroy(item.gameObject);
        }
    }

    public float GiveScore()
    {
        float inBtwManaScore = curMana / (currentRecipe.mana / 100.0f);
        float inBtwHealthScore = curHealth / (currentRecipe.health / 100.0f);
        float inBtwPowerScore = curPower / (currentRecipe.power / 100.0f);

        Debug.Log("Mana" + inBtwManaScore + "\nHealth:" + inBtwHealthScore + "\nPower" + inBtwPowerScore);

        float result = inBtwHealthScore + inBtwManaScore + inBtwPowerScore / (300 / 100.0f);

        Debug.Log(result);

        return result;
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
            
            if (!currentIngredient.HasBeenOnTheke)
            {
                Debug.Log("ingredient, hasnt been on theke");
                ingredients.Add(currentIngredient.ingredient);

                UpdateUIText();
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
                            }
                        }
                        if (!found)
                        {
                            _tempArray = new SO_Ingredient[recipeToDo.Length + 1];

                            for (int i = 0; i < _tempArray.Length - 1; i++)
                            {
                                _tempArray[i] = recipeToDo[i];
                            }
                            _tempArray[recipeToDo.Length] = currentIngredient.ingredient;
                            recipeToDo = new SO_Ingredient[recipeToDo.Length+1];
                            recipeToDo = _tempArray;

                        }
                        else
                        {
                            AddToScore(currentIngredient);
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
        Debug.Log(ingredients.Count);
        currentIngredients.SetText((ingredients.Count) + "/" + ingredientsLimit);
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

        curHealth -= _ingredient.health;
        curMana -= _ingredient.mana;
        curPower -= _ingredient.power;

        ingredients = new List<SO_Ingredient>();
        ingredients = _tempList;
        UpdateUIText();
    }
    public void RemoveRecipeItem(SO_Ingredient _ingredient)
    {
        SO_Ingredient[] _tempArray = new SO_Ingredient[recipeToDo.Length - 1];
        bool itemDeleted = false;
        for(int i = 0; i < recipeToDo.Length; i++)
        {
            if (recipeToDo[i].CompareIngredient(_ingredient) && !itemDeleted)
            {
                itemDeleted = true;
            }
            else
            {
                _tempArray[i] = recipeToDo[i];
            }
        }
        recipeToDo = new SO_Ingredient[recipeToDo.Length + 1];
        recipeToDo = _tempArray;


        List<SO_Ingredient> _tempList = new List<SO_Ingredient>();
        bool itemDeletedList = false;
        foreach (var item in ingredients)
        {
            if (item.CompareIngredient(_ingredient) && !itemDeletedList)
            {
                itemDeletedList = true;
            }
            else
            {
                _tempList.Add(item);
            }
        }
        ingredients = new List<SO_Ingredient>();
        ingredients = _tempList;
    }
    public void StartCooking()
    {
        if (currentRecipe.ContainsRecipe(ingredients))
        { 
            Debug.Log("Cook the ingredient owo");
            //OverallScore = GiveScore();
        }
    }
}
