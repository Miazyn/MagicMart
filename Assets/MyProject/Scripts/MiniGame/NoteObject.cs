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

    private void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (CanBePressed)
            {
                //RhythmGameManager.instance.NoteHit();
                HasHitNote = true;
                gameObject.SetActive(false);

                if(Mathf.Abs(transform.position.y) > Mathf.Abs(keyYValue + toleranceNormalHit))
                {
                    //Normal
                    RhythmGameManager.instance.NormalHit();
                }
                else if(Mathf.Abs(transform.position.y) > Mathf.Abs(keyYValue + toleranceGoodHit))
                {
                    //Good
                    RhythmGameManager.instance.GoodHit();

                }
                else if (Mathf.Abs(transform.position.y) > Mathf.Abs(keyYValue + tolerancePerfectHit))
                {
                    //Perfect
                    RhythmGameManager.instance.PerfectHit();

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
                RhythmGameManager.instance.NoteMiss();
            }
        }
    }
}
