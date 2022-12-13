using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerCreation : MonoBehaviour
{
    GameManager gameManager;

    int customerAmount;
    int dayCounter;

    SO_NPC fillerCustomers;

    private void Start()
    {
        gameManager = GameManager.Instance;
        dayCounter = 1;

        customerAmount = Random.Range(5, 7);
        StartCoroutine(CreatingCustomer());
    }

    IEnumerator CreatingCustomer()
    {
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
