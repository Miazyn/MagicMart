using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RhythmGameManager : MonoBehaviour
{
    [Header("Rhythm Game")]
    public AudioSource rhythmMusic;
    public bool StartPlaying;

    public static RhythmGameManager instance;

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
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        NotesToBeMade = Random.Range(7, 15);
        NotesLeft = NotesToBeMade;
        Debug.Log(NotesToBeMade);
        currentScore = 0;

        perfectScore = NotesToBeMade * scorePerPerfectNote;
        onePercent = perfectScore / 100;
        scoreText.text = 100 + "%";
    }
    void Update()
    {
        if (!StartPlaying)
        {
            if (Input.anyKeyDown)
            {
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
        //Debug.Log("Mac" + perfectScore + "mine:" + playerScore);
        //Debug.Log("Score:" + playerScore / onePercent + "%");
    }

    public void NormalHit()
    {
        Debug.Log("Hit");
        currentScore += scorePerNote; 
        PercentageCalc(); 
    }
    public void GoodHit() 
    {
        Debug.Log("Good Hit");

        currentScore += scorePerGoodNote;
        PercentageCalc(); 
    }
    public void PerfectHit() 
    {
        Debug.Log(" Perfect Hit");

        currentScore += scorePerPerfectNote; 
        PercentageCalc(); 
    }


    public void NoteMiss()
    {
        PercentageCalc();
    }
}
