using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMenu : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] GameObject itemMenu;
    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    public void ToggleItemMenu()
    {
        itemMenu.SetActive(!itemMenu.activeSelf);
    }

}
