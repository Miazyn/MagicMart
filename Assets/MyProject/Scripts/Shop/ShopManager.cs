using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject ShopTilePrefab;
    public SO_Ingredient Ingredient;

    public Transform parentTransform;
    RectTransform parentRect;

    Player CurrentPlayer;
    SO_Inventory PlayerInventory;

    int shopSize = 10;

    void Awake()
    {
        CurrentPlayer = Player.instance;
        PlayerInventory = CurrentPlayer.inventory;

        for(int i =0; i < shopSize; i++)
        {
            GameObject _shopTile = Instantiate(ShopTilePrefab, parentTransform);

            _shopTile.GetComponent<RectTransform>().anchoredPosition = 
                parentTransform.GetComponent<RectTransform>().anchoredPosition;
            _shopTile.GetComponent<ShopTile>().Ingredient = this.Ingredient;

        }
    }
}
