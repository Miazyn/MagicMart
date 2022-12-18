using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    public GameObject ShopTilePrefab;

    SO_Ingredient[] allIngredients;

    public Transform parentTransform;


    void Awake()
    {
        allIngredients = Resources.LoadAll<SO_Ingredient>("CookingIngredients");
    }

    void Start()
    {
        
        for(int i =0; i < allIngredients.Length; i++)
        {
            GameObject _shopTile = Instantiate(ShopTilePrefab, parentTransform);

            _shopTile.GetComponent<RectTransform>().anchoredPosition = parentTransform.GetComponent<RectTransform>().anchoredPosition;
            _shopTile.GetComponent<ShopTile>().Ingredient = allIngredients[i];

            _shopTile.GetComponent<ShopTile>().SetUpTile();
        }
    }

    public void LeaveShop()
    {
        SceneManager.LoadScene("MainStore", LoadSceneMode.Single);
    }
}
