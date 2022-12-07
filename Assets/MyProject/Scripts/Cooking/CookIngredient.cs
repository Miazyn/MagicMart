using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CookIngredient : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public RectTransform rect;
    //public Vector2 originalPosition;

    public SO_Ingredient ingredient;

    public Canvas canvas;
    public CanvasGroup canvasGroup;
    public float onDragAlpha;


    GameObject prefab;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        //originalPosition = rect.anchoredPosition;
        
        
    }
    private void Start()
    {
        GetComponent<Image>().sprite = ingredient.ingredientSprite;
        if (!canvas)
        {
            Debug.LogWarning("NO CANVAS FOR SCALE DEFINED");
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = onDragAlpha;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        Destroy(gameObject);
    }

}
