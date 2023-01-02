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
        inventory = Resources.Load<SO_Inventory>("Inventory/Player Inventory");

        if (Resources.Load<SO_Inventory>("Inventory/SavedInventory").inventoryItems.Count > 0)
        {
            foreach(var item in Resources.Load<SO_Inventory>("Inventory/SavedInventory").inventoryItems)
            {
                inventory.AddItem(item.item, item.GetAmount());
            }
        }

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


        //TEMP
        if (Input.GetKeyDown(KeyCode.J))
        {
            SaveData();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadData();
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

    void LoadData()
    {
        Data _data = SaveSystem.LoadData();

        Debug.LogWarning("Player last had: " +  _data.money + " Coins.");
    }

    void SaveData()
    {
        SaveSystem.SaveData(this);
        Debug.Log("Saved Data");
    }

    void OnApplicationQuit()
    {
        SaveSystem.SaveData(this);
    }
}
