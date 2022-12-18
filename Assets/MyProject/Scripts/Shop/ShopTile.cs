using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ShopTile : MonoBehaviour
{
    public SO_Ingredient Ingredient;
    [SerializeField] Image ItemImage;
    [SerializeField] TextMeshProUGUI Name;
    [SerializeField] TextMeshProUGUI Price;
    [SerializeField] TextMeshProUGUI PlayerStock;

    Player CurrentPlayer;
    SO_Inventory PlayerInventory;
    private void Start()
    {
        CurrentPlayer = Player.instance;
        PlayerInventory = CurrentPlayer.inventory;
        SetUpTile();
    }

    void SetUpTile()
    {
        ItemImage.sprite = Ingredient.ingredientSprite;
        Name.SetText(Ingredient.ingredientName);
        Price.SetText(Ingredient.BuyPrice.ToString());
        int playerStockOfItem;
        if (PlayerInventory.FindItemInList(Ingredient).Item1) 
        {
            playerStockOfItem = 
                PlayerInventory.inventoryItems
                [PlayerInventory.FindItemInList(Ingredient).Item2].GetAmount();
            PlayerStock.SetText("Owned: " + playerStockOfItem.ToString());
        }
        else
        {
            PlayerStock.SetText("Owned: " + "invalid_item");
        }


    }
}
