using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RhythmGameManager : MonoBehaviour
{
    [Header("Rhythm Game")]
    public AudioSource rhythmMusic;
    public bool StartPlaying;

    public static RhythmGameManager instance;
    GameManager manager;

    [SerializeField] int currentScore;
    float scoreDifference;

    [SerializeField] int scorePerNote = 100;
    [SerializeField] int scorePerGoodNote = 125;
    [SerializeField] int scorePerPerfectNote = 150;

    int NotesToBeMade;
    int NotesLeft;
    float perfectScore, onePercent;
    [SerializeField] NoteSpawner noteSpawn;

    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] SceneMana sceneMana;

    bool IsGameFinished = false;

    [SerializeField] GameObject Panel;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        manager = GameManager.Instance;
        NotesToBeMade = Random.Range(15,20);
        NotesLeft = NotesToBeMade;
        currentScore = 0;

        perfectScore = NotesToBeMade * scorePerPerfectNote;
        onePercent = perfectScore / 100;
        scoreText.text = 100 + "%";

        manager.ChangeGameState(GameManager.GameState.MiniRhythmGameState);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!StartPlaying)
            {
                Panel.SetActive(false);
                StartPlaying = true;
                rhythmMusic.Play();
                StartCoroutine(noteSpawn.CreateNotes(NotesToBeMade));

            }
        }

    }

    public void PercentageCalc()
    {
        NotesLeft--;
        float playerScore = NotesLeft * scorePerPerfectNote + currentScore;
        playerScore /= onePercent;
        scoreText.text = playerScore.ToString("F2") + "%";

        if(NotesLeft == 0)
        {
            IsGameFinished = true;

            for(int i = 0; i < noteSpawn.spawnedNotes.Count; i++)
            {
                Destroy(noteSpawn.spawnedNotes[i]);
            }

            manager.RhythymGameScore = playerScore;
            sceneMana.LoadNextScene("TransitionScene");
        }
    }
    public void NormalHit()
    {
        currentScore += scorePerNote; 
        PercentageCalc(); 
    }
    public void GoodHit() 
    {

        currentScore += scorePerGoodNote;
        PercentageCalc(); 
    }
    public void PerfectHit() 
    {

        currentScore += scorePerPerfectNote; 
        PercentageCalc(); 
    }
    public void NoteMiss()
    {
        PercentageCalc();
    }
}
