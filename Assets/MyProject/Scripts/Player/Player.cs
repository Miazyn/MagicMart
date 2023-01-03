using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    public string PlayerName = "Aubrey";
    public string StoreName = "Magic Mart";

    public SO_Inventory inventory;
    public int moneyAmount { get; private set; }

    public SO_Quest CurrentCookingQuest;
    GameManager manager;

    public SO_CookedFood CreatedFood;

    [Header("Cursor")]
    [SerializeField] Texture2D cursorNormal;
    [SerializeField] Texture2D cursorDrag;
    CursorMode cursorMode;
    Vector2 hotspot;

    public delegate void OnMoneyChanged();
    public OnMoneyChanged onMoneyChangedCallback;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public delegate void OnInventoryToggle();
    public OnItemChanged onInventoryToggleCallback;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        manager = GameManager.Instance;

        //CURSOR
        cursorMode = CursorMode.Auto;
        hotspot = Vector2.zero;

        Cursor.SetCursor(cursorNormal, hotspot, cursorMode);
        SetMoneyAmount(10000);
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (manager.curState == GameManager.GameState.DialogState)
            {
                manager.CheckGameStateAction();
            }
            if(manager.curState == GameManager.GameState.AfterDialog)
            {
                manager.AfterQuestDialog();
            }
        }
    }


    public bool CanCookRecipe(SO_Recipe _recipe)
    {
        int length = _recipe.ingredients.Length;
        int counter = 0;
        foreach (var ingredient in _recipe.ingredients)
        {
            if (!inventory.FindItemInList(ingredient).Item1)
            {
                return false;
            }
            if (inventory.inventoryItems[inventory.FindItemInList(ingredient).Item2].GetAmount() <= 0)
            {
                return false;
            }

            Debug.Log("Player has enough of ingredient.");
            counter++;

        }
        if(counter == length)
        {
            return true;
        }
        return false;
    }

    public bool CanBuy(int _buyPrice)
    {
        if (moneyAmount >= _buyPrice) return true;
        return false;
    }

    public void SetMoneyAmount(int _addedMoney)
    {
        moneyAmount += _addedMoney;
        if (onMoneyChangedCallback != null)
        {
            onMoneyChangedCallback.Invoke();
        }
    }

    public void LoadData()
    {
        Data _data = SaveSystem.LoadData();

        moneyAmount = _data.money;

        LoadInventory(_data);
    }
    void LoadInventory(Data _data)
    {
        SO_Ingredient[] _allIngredients = Resources.LoadAll<SO_Ingredient>("CookingIngredients");
        ClearInventory();

        for (int i = 0; i < _data.IngredientAmount.Length; i++)
        {
            foreach (var _ingredient in _allIngredients)
            {
                if (_data.IngredientName[i] == _ingredient.ingredientName)
                {
                    inventory.AddItem(_ingredient, _data.IngredientAmount[i]);
                    break;
                }
            }
        }
    }
    void ClearInventory()
    {
        inventory.inventoryItems.Clear();
    }
    void OnApplicationQuit()
    {
        if (SaveSystem.HasSaveData())
        {
            ClearInventory();
        }
    }
}
