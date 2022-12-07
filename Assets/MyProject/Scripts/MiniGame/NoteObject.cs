using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    [SerializeField] float toleranceNormalHit = 0.25f;
    [SerializeField] float toleranceGoodHit = 0.15f;
    [SerializeField] float tolerancePerfectHit = 0.1f;
    bool CanBePressed;
    [SerializeField] KeyCode keyToPress;
    bool HasHitNote;
    [SerializeField] float keyYValue;

    [Header("Game SFX")]
    [SerializeField] GameObject hitEffect, goodHitEffect, perfectHitEffect, missEffect;
    private void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (CanBePressed)
            {
                //RhythmGameManager.instance.NoteHit();
                HasHitNote = true;
                gameObject.SetActive(false);
                if (Mathf.Abs(transform.position.y) > Mathf.Abs(keyYValue + tolerancePerfectHit))
                {
                    //Perfect
                    Instantiate(perfectHitEffect, transform.position, perfectHitEffect.transform.rotation);
                    RhythmGameManager.instance.PerfectHit();

                }
                else if (Mathf.Abs(transform.position.y) > Mathf.Abs(keyYValue + toleranceGoodHit))
                {
                    //Good
                    Instantiate(goodHitEffect, transform.position, goodHitEffect.transform.rotation);
                    RhythmGameManager.instance.GoodHit();

                }
                else if (Mathf.Abs(transform.position.y) > Mathf.Abs(keyYValue + toleranceNormalHit))
                {
                    //Normal
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                    RhythmGameManager.instance.NormalHit();
                }
                
                
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Activator")
        {
            CanBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Activator")
        {
            CanBePressed = false;
            if (!HasHitNote)
            {
                Instantiate(missEffect, transform.position, missEffect.transform.rotation);
                RhythmGameManager.instance.NoteMiss();
            }
        }
    }
}
