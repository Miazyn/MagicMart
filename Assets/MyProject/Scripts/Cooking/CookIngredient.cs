using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CookIngredient : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] float delayedCheck = 0.2f;
    public RectTransform rect;
    //public Vector2 originalPosition;

    public SO_Ingredient ingredient;

    public Canvas canvas;
    public CanvasGroup canvasGroup;
    public float onDragAlpha;


    GameObject prefab;

    bool IsDragged = false;

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
        StartCoroutine(CheckIfDragged());
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = onDragAlpha;
        canvasGroup.blocksRaycasts = false;
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
        IsDragged = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        Destroy(gameObject);
    }


    IEnumerator CheckIfDragged()
    {
        yield return new WaitForSeconds(delayedCheck);
        if (!IsDragged)
        {
            Destroy(gameObject);
        }
    }
}
