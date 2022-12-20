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

    int npcOfTheDay = 0;
    void Awake()
    {
        sprites = Resources.LoadAll<Sprite>("GenericSprites");
        voices = Resources.LoadAll<SO_Voice>("Generic/Voices");
        allRecipes = Resources.LoadAll<SO_Recipe>("Recipes");
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
                _npc = CreateCustomer();
                gameManager.Customers[i] = _npc;
            }
        }
        yield return null;
        Debug.Log("Customers have been assigned" + "\nStart Dialog Status");
        gameManager.CustomerCount = customerAmount;
        gameManager.ChangeGameState(GameManager.GameState.DialogState);
    }

    SO_NPC CreateCustomer()
    {
        blueprint.voice = voices[Random.Range(0, voices.Length)];
        blueprint.quests[0].ReqRecipe = allRecipes[Random.Range(0, allRecipes.Length)];
        blueprint.quests[0].QuestDialogBeforeCompletion[0].spriteForCharacterDisplay[0] = sprites[Random.Range(0, sprites.Length)];
        blueprint.quests[0].QuestDialogAfterCompletion[0].spriteForCharacterDisplay[0] = sprites[Random.Range(0, sprites.Length)];

        return blueprint;
    }
}
