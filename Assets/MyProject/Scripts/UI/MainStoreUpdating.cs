using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainStoreUpdating : MonoBehaviour
{
    GameManager manager;
    Player player;

    [SerializeField] TextMeshProUGUI playerMoney;
    [SerializeField] TextMeshProUGUI customersleft;

    void Start()
    {
        player = Player.instance;
        manager = GameManager.Instance;

        player.onMoneyChangedCallback += UpdateMoney;
        manager.onNextCustomerCallback += UpdateUI;
        UpdateMoney();
        UpdateUI();
    }

    void UpdateUI()
    {
        customersleft.SetText("" + (manager.CustomerCount - manager.counter));
    }

    void UpdateMoney()
    {
        playerMoney.SetText("" + player.moneyAmount);
    }

    void OnDisable()
    {
        player.onMoneyChangedCallback -= UpdateMoney;
        manager.onNextCustomerCallback -= UpdateUI;
    }
}
