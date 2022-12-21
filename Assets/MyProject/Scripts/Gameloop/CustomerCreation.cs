using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerCreation : MonoBehaviour
{
    GameManager gameManager;

    int customerAmount;
    int dayCounter;

    [SerializeField] SO_NPC blueprint;
    [SerializeField] SO_NPC[] mainChars;

    Sprite[] sprites;
    SO_Voice[] voices;
    SO_Recipe[] allRecipes;

    [SerializeField] SO_NPC[] allNPCs;

    [SerializeField] GameObject cookButton;
    [SerializeField] GameObject shopButton;
    [SerializeField] GameObject playerMoney;

    int npcOfTheDay = 0;
    void Awake()
    {
        //sprites = Resources.LoadAll<Sprite>("GenericSprites");
        //voices = Resources.LoadAll<SO_Voice>("Generic/Voices");
        //allRecipes = Resources.LoadAll<SO_Recipe>("Recipes");
        allNPCs = Resources.LoadAll<SO_NPC>("GenericNPC");
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        dayCounter = 1;


        if (gameManager.curState == GameManager.GameState.DayStart)
        {
            customerAmount = Random.Range(5, 7);

            npcOfTheDay = Random.Range(0, customerAmount);

            StartCoroutine(CreatingCustomers());
        }
        if(gameManager.curState == GameManager.GameState.IdleState)
        {
            EnableItems();
        }
    }

    public void DayStart()
    {
        if (gameManager.curState == GameManager.GameState.DayStart)
        {
            customerAmount = Random.Range(5, 7);

            npcOfTheDay = Random.Range(0, customerAmount);

            StartCoroutine(CreatingCustomers());
        }
    }

    IEnumerator CreatingCustomers()
    {
        gameManager.Customers = new SO_NPC[customerAmount];
        for(int i = 0; i < customerAmount; i++)
        {
            SO_NPC _npc = new SO_NPC();
            if(i == npcOfTheDay)
            {
                gameManager.Customers[i] = mainChars[Random.Range(0, mainChars.Length)];
            }
            else
            {
                gameManager.Customers[i] = allNPCs[Random.Range(0, allNPCs.Length)];
            }
        }
        yield return null;
        Debug.Log("Customers have been assigned" + "\nStart Dialog Status");
        gameManager.CustomerCount = customerAmount;
        gameManager.ChangeGameState(GameManager.GameState.StartState);
    }

    void EnableItems()
    {
        shopButton.SetActive(true);
        cookButton.SetActive(true);
        playerMoney.SetActive(true);
    }
}
