using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    public string PlayerName = "Aussie";

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
        moneyAmount = 0;
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
                Debug.Log("Dialog");
                manager.CheckGameStateAction();
            }
            if(manager.curState == GameManager.GameState.EvaluationState)
            {
                Debug.Log("After");
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
    }

    void OnApplicationQuit()
    {
        //B4 clear, save inventory
        if (inventory.inventoryItems.Count > 0)
        {
            inventory.inventoryItems.Clear();
        }
    }
}
