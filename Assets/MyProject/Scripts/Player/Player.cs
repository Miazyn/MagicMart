using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    public string PlayerName = "Default";

    public SO_Inventory inventory;
    public int moneyAmount { get; private set; }

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public delegate void OnHotbarScroll();
    public OnItemChanged onHotbarScrollCallback;

    public delegate void OnInventoryToggle();
    public OnItemChanged onInventoryToggleCallback;
    void Awake()
    {
        instance = this;
        moneyAmount = 0;
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
