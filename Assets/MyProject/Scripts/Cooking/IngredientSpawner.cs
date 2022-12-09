using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class IngredientSpawner : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IInitializePotentialDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] GameObject ingredientPrefab;
    [SerializeField] SO_Ingredient ingredientToSpawn;
    [SerializeField] Transform prefabParent;
    [SerializeField] Canvas canvas;

    [SerializeField] Image itemImage;
    CookIngredient ingredientScript;
    GameObject instantiatedObject;

    Color defaultColor;
    Vector2 currentpos;

    void Awake()
    {
        itemImage.sprite = ingredientToSpawn.ingredientSprite;
        defaultColor = GetComponent<Image>().color;
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        instantiatedObject = Instantiate(ingredientPrefab, prefabParent);
        ingredientScript = instantiatedObject.GetComponent<CookIngredient>();
        ingredientScript.ingredient = ingredientToSpawn;
        ingredientScript.canvas = canvas;

        currentpos = GetComponent<RectTransform>().anchoredPosition;
        RectTransform rt = instantiatedObject.GetComponent<RectTransform>();
        rt.anchoredPosition = currentpos;
        instantiatedObject.transform.position = transform.position;


        eventData.pointerDrag = instantiatedObject;
       
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Image alpa down
        GetComponent<Image>().color = Color.green;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Image alpha back up
        GetComponent<Image>().color = defaultColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }


}
