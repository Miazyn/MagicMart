using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerCreation : MonoBehaviour
{
    GameManager gameManager;

    int customerAmount;
    int dayCounter;

    [SerializeField] SO_NPC fillerCustomers;

    private void Start()
    {
        gameManager = GameManager.Instance;
        dayCounter = 1;
        if (gameManager.curState == GameManager.GameState.DayStart)
        {
            customerAmount = Random.Range(5, 7);
            StartCoroutine(CreatingCustomer());
        }
    }

    IEnumerator CreatingCustomer()
    {
        gameManager.Customers = new SO_NPC[customerAmount];
        for(int i = 0; i < customerAmount; i++)
        {
            gameManager.Customers[i] = fillerCustomers;
        }
        yield return null;
        Debug.Log("Customers have been assigned" + "\nStart Dialog Status");
        gameManager.CustomerCount = customerAmount;
        gameManager.ChangeGameState(GameManager.GameState.DialogState);
    }


}
