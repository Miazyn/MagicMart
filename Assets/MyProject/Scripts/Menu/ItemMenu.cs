using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMenu : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] GameObject itemMenu;
    [SerializeField] GameObject itemMenuBG;
    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    public void ToggleItemMenu()
    {
        itemMenu.SetActive(!itemMenu.activeSelf);
        itemMenuBG.SetActive(!itemMenuBG.activeSelf);
    }

}
