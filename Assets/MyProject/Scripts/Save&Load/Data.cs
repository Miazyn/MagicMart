using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    public bool FirstTimePlayed;

    public int[] QuestID;

    public int money;
    public int[] IngredientAmount;
    public string[] IngredientName;

    public Data(Player player)
    {
        money = player.moneyAmount;
        IngredientAmount = new int[player.inventory.inventoryItems.Count];
        IngredientName = new string[player.inventory.inventoryItems.Count];

        int _counter = 0;
        foreach (var item in player.inventory.inventoryItems)
        {
            IngredientName[_counter] = item.item.ingredientName;
            IngredientAmount[_counter] = item.amount;
            _counter++;
        }
    }
}
