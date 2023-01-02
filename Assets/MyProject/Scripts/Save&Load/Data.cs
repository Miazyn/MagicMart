using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    public bool FirstTimePlayed;
    public int money;
    public int[] IngredientAmount;
    public string[] IngredientName;

    public Data(Player player)
    {
        money = player.moneyAmount;
        IngredientAmount = new int[player.inventory.inventoryItems.Count];
        IngredientName = new string[player.inventory.inventoryItems.Count];
        int counter = 0;
        foreach (var item in player.inventory.inventoryItems)
        {
            IngredientName[counter] = item.item.ingredientName;
            IngredientAmount[counter] = item.amount;
            counter++;
        }
    }
}
