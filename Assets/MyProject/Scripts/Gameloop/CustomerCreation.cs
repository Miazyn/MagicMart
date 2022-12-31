using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerCreation : MonoBehaviour
{
    GameManager gameManager;
    Player player;
    SO_Inventory playerInventory;


    [SerializeField] int minCustomers = 5;
    [SerializeField] int maxCustomers = 7;

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

    SO_NPC[] possibleNPC;
    void Awake()
    {
        allNPCs = Resources.LoadAll<SO_NPC>("GenericNPC");
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        dayCounter = 1;
        player = Player.instance;
        playerInventory = player.inventory;


        DayStart();
        if(gameManager.curState == GameManager.GameState.IdleState)
        {
            EnableItems();
        }

        gameManager.onDayChangedCallback += DayStart;
    }

    public void DayStart()
    {
        if (gameManager.curState == GameManager.GameState.DayStart)
        {
            customerAmount = Random.Range(minCustomers, maxCustomers);

            npcOfTheDay = Random.Range(0, customerAmount);

            StartCoroutine(CreatingCustomers());
        }
    }


    IEnumerator CreatingCustomers()
    {
        //possibleNPC = CheckForCompatibleNPCQuest();
        ////
        //foreach(var item in possibleNPC) Debug.Log(item);
        ////
        gameManager.Customers = new SO_NPC[customerAmount];
        for(int i = 0; i < customerAmount; i++)
        {
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
        gameManager.ExpectedCustomerAmount = customerAmount;
        gameManager.ChangeGameState(GameManager.GameState.StartState);
    }

    //SO_NPC[] CheckForCompatibleNPCQuest()
    //{
    //    SO_NPC[] _Npcs = new SO_NPC[allNPCs.Length];

    //    for(int i = 0; i < allNPCs.Length -1; i++)
    //    {
    //        if (player.CanCookRecipe(allNPCs[i].quests[0].ReqRecipe))
    //        {
    //            _Npcs[i] = allNPCs[i];
    //        }
    //    }

    //    return _Npcs;
    //}

    void EnableItems()
    {
        shopButton.SetActive(true);
        cookButton.SetActive(true);
        playerMoney.SetActive(true);
    }

    private void OnDisable()
    {
        gameManager.onDayChangedCallback -= DayStart;
    }
}
