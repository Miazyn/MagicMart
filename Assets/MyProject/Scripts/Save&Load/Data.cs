using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    public int money;

    public Data(Player player)
    {
        money = player.moneyAmount;
    }
}
