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
    [SerializeField]    SO_Inventory PlayerInventory;

    public void SetUpTile()
    {
        CurrentPlayer = Player.instance;
        PlayerInventory = CurrentPlayer.inventory;

        ItemImage.sprite = Ingredient.ingredientSprite;
        Name.SetText(Ingredient.ingredientName);
        Price.SetText(Ingredient.BuyPrice.ToString());
        int playerStockOfItem = 0;

        if (CurrentPlayer.inventory.inventoryItems.Count > 0)
        {
            playerStockOfItem =
                PlayerInventory.inventoryItems
                [PlayerInventory.FindItemInList(Ingredient).Item2].GetAmount();

            PlayerStock.SetText("Owned: " + playerStockOfItem.ToString());
        }
        else
        {
            Debug.LogWarning("NO Items in player");
            PlayerStock.SetText("Owned: " + 0);
        }

    }
}
