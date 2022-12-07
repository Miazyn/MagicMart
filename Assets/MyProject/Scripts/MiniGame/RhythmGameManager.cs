using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmGameManager : MonoBehaviour
{
    [Header("Rhythm Game")]
    public AudioSource rhythmMusic;
    public bool StartPlaying;
    public BeatScroller beatScroller;

    public static RhythmGameManager instance;

    public int currentScore;
    [SerializeField] int scorePerNote = 100;
    [SerializeField] int scorePerGoodNote = 125;
    [SerializeField] int scorePerPerfectNote = 150;

    [SerializeField] int currentMultiplier;
    [SerializeField] int multiplierTracker;
    public int[] multiplierThresholds;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentMultiplier = 1;
    }
    void Update()
    {
        if (!StartPlaying)
        {
            if (Input.anyKeyDown)
            {
                StartPlaying = true;
                beatScroller.HasStarted = true;
                rhythmMusic.Play();
            }
        }
    }

    public void NoteHit()
    {
        if ((int)currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;

            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }
        //currentScore += scorePerNote * currentMultiplier;
    }

    public void NormalHit()
    {
        Debug.Log("Normal");
        currentScore += scorePerNote * currentMultiplier; 
        NoteHit(); 
    }
    public void GoodHit() 
    {
        Debug.Log("Good");

        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit(); 
    }
    public void PerfectHit() 
    {
        Debug.Log("Perfect");

        currentScore += scorePerPerfectNote * currentMultiplier; 
        NoteHit(); 
    }


    public void NoteMiss()
    {
        Debug.Log("Note missed");
    }
}
