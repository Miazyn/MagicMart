using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class IngredientSpawner : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IInitializePotentialDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("Ingredient")]
    public GameObject ingredientPrefab;
    public SO_Ingredient ingredientToSpawn;
    public TextMeshProUGUI ItemAmount;

    [Header("Positioning")]
    public Transform prefabParent;
    public Transform AfterThekeParent;
    public Canvas canvas;

    [SerializeField]public Image itemImage { get; private set; }
    CookIngredient ingredientScript;
    GameObject instantiatedObject;

    Color defaultColor;
    Vector2 currentpos;

    [SerializeField] PullUpMenu pullUpItemMenu;

    void Awake()
    {
        itemImage = transform.Find("ItemImage").gameObject.GetComponent<Image>();
        ItemAmount = transform.Find("ItemAmount").gameObject.GetComponent<TextMeshProUGUI>();
        defaultColor = GetComponent<Image>().color;

    }

    void Start()
    {
        pullUpItemMenu = GameObject.FindObjectOfType<PullUpMenu>();
        itemImage.sprite = ingredientToSpawn.ingredientSprite;
        AddItemAmount(1);
    }

    public void AddItemAmount(int _amount)
    {
        ItemAmount.SetText(_amount.ToString());
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        instantiatedObject = Instantiate(ingredientPrefab, prefabParent);
        ingredientScript = instantiatedObject.GetComponent<CookIngredient>();
        ingredientScript.ingredient = ingredientToSpawn;
        ingredientScript.canvas = canvas;
        instantiatedObject.GetComponent<CookIngredient>().AfterOnTheke = AfterThekeParent;

        currentpos = GetComponent<RectTransform>().anchoredPosition;
        RectTransform rt = instantiatedObject.GetComponent<RectTransform>();
        rt.anchoredPosition = currentpos;
        instantiatedObject.transform.position = transform.position;


        eventData.pointerDrag = instantiatedObject;
        pullUpItemMenu.MenuDown();
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
        if (GameObject.FindWithTag("Ingredient") == null)
        {
            GetComponent<Image>().color = Color.green;
        }
        else
        {
            if (!GameObject.FindWithTag("Ingredient").GetComponent<CookIngredient>().IsCurrentlyDragged)
            {
                GetComponent<Image>().color = Color.green;
            }
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Image alpha back up
        GetComponent<Image>().color = defaultColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }

    public SO_Ingredient GetIngredient()
    {
        return ingredientToSpawn;
    }
}
