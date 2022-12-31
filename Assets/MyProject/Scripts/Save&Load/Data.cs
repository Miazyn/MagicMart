using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data
{
    int money;

    public Data(Player player)
    {
        money = player.moneyAmount;
    }
}
