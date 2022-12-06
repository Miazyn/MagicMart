using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CookIngredient : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    RectTransform rect;
    Vector2 originalPosition;

    [SerializeField] SO_Ingredient ingredient;

    public Canvas canvas;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float onDragAlpha;


    GameObject prefab;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalPosition = rect.anchoredPosition;
        if (!canvas)
        {
            Debug.LogWarning("NO CANVAS FOR SCALE DEFINED");
        }
        GetComponent<Image>().sprite = ingredient.ingredientSprite;
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

    }
}
