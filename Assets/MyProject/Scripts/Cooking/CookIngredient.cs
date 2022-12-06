using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CookIngredient : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler, IDropHandler
{
    RectTransform rect;
    Vector2 originalPosition;
    [SerializeField] Canvas canvas;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float onDragAlpha;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalPosition = rect.anchoredPosition;
        if (!canvas)
        {
            Debug.LogWarning("NO CANVAS FOR SCALE DEFINED");
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        canvasGroup.alpha = onDragAlpha;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
        Debug.Log("Dragging");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
      //  Debug.Log("Pointer down");
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Item has been dropped");
        // Back to original position
       // rect.anchoredPosition = originalPosition;
    }
}
