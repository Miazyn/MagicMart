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
    public TextMeshProUGUI Mana;
    public TextMeshProUGUI Health;
    public TextMeshProUGUI Power;
    public TextMeshProUGUI Name;

    [Header("Positioning")]
    public Transform prefabParent;
    public Transform AfterThekeParent;
    public Canvas canvas;

    [SerializeField]public Image itemImage { get; private set; }
    CookIngredient ingredientScript;
    GameObject instantiatedObject;

    public Color highlightColor;
    Color defaultColor;
    Vector2 currentpos;


    void Awake()
    {
        itemImage = transform.Find("ItemImage").gameObject.GetComponent<Image>();
        ItemAmount = transform.Find("Amount").gameObject.GetComponent<TextMeshProUGUI>();
        defaultColor = GetComponent<Image>().color;

    }

    void Start()
    {
        itemImage.sprite = ingredientToSpawn.ingredientSprite;
        Mana.SetText("" + ingredientToSpawn.mana);
        Health.SetText("" + ingredientToSpawn.health);
        Power.SetText("" + ingredientToSpawn.power);
        Name.SetText(ingredientToSpawn.ingredientName);

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

        Vector3 mousePos = new Vector3(Input.mousePosition.x - 2, Input.mousePosition.y - 2, Input.mousePosition.z);

        instantiatedObject.transform.position = mousePos;


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
        if (GameObject.FindWithTag("Ingredient") == null)
        {
            GetComponent<Image>().color = highlightColor;
        }
        else
        {
            if (!GameObject.FindWithTag("Ingredient").GetComponent<CookIngredient>().IsCurrentlyDragged)
            {
                GetComponent<Image>().color = highlightColor;
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
