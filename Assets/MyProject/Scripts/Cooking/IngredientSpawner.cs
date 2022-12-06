using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class IngredientSpawner : MonoBehaviour, IInitializePotentialDragHandler, IDragHandler
{
    [SerializeField] GameObject ingredientPrefab;
    [SerializeField] SO_Ingredient ingredientToSpawn;
    [SerializeField] Transform prefabParent;
    [SerializeField] Canvas canvas;
    CookIngredient ingredientScript;
    GameObject instantiatedObject;

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        instantiatedObject = Instantiate(ingredientPrefab, prefabParent);
        ingredientScript = instantiatedObject.GetComponent<CookIngredient>();
        ingredientScript.ingredient = ingredientToSpawn;
        ingredientScript.canvas = canvas;

        ingredientPrefab.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

        eventData.pointerDrag = instantiatedObject;
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

}
