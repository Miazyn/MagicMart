using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarAssigner : MonoBehaviour
{
    [SerializeField] Animator star1;
    [SerializeField] Animator star2;
    [SerializeField] Animator star3;

    GameManager manager;
    float score;

    void Start()
    {
        manager = GameManager.Instance;
        score = manager.OverallScore;

        Judge();
    }

    void Judge()
    {
        if(score > 80)
        {
            star1.enabled = true;
            star2.enabled = true;
            star3.enabled = true;
        }
        else if(score > 50)
        {
            star1.enabled = true;
            star2.enabled = true;
        }
        else if(score > 20)
        {
            star1.enabled = true;
        }
    }
}
