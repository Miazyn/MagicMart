using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    public string PlayerName = "Aussie";
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

    public delegate void OnHotbarScroll();
    public OnItemChanged onHotbarScrollCallback;

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

        moneyAmount = 10000;
        manager = GameManager.Instance;

        //CURSOR
        cursorMode = CursorMode.Auto;
        hotspot = Vector2.zero;

        Cursor.SetCursor(cursorNormal, hotspot, cursorMode);
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (manager.curState == GameManager.GameState.DialogState)
            {
                manager.CheckGameStateAction();
            }
            if(manager.curState == GameManager.GameState.EvaluationState)
            {
                manager.AfterQuestDialog();
            }
        }    
    }

    public bool CanBuy(int _buyPrice)
    {
        if (moneyAmount > _buyPrice) return true;
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


    void OnApplicationQuit()
    {
        SO_Inventory saveInventory = Resources.Load<SO_Inventory>("Inventory/SavedInventory");

        saveInventory.inventoryItems.Clear();

        foreach(var item in inventory.inventoryItems)
        {
            saveInventory.AddItem(item.item, item.GetAmount());
        }

        Debug.Log("Saved the inventory");
        //inventory.inventoryItems.Clear();
    }
}
