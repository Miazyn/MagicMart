using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    [SerializeField] float beatTempo = 120f;

    [SerializeField] bool HasStarted;
    RectTransform noteRect;

    private void Start()
    {
        beatTempo /= 60f;
        noteRect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (!HasStarted)
        {
            if (Input.anyKeyDown)
            {
                HasStarted = true;
            }
        }
        else
        {
            //Start moving down
            noteRect.anchoredPosition -= new Vector2(0f, beatTempo * Time.deltaTime);
        }
    }
}
