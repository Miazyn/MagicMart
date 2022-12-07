using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    [SerializeField] float beatTempo = 120f;

    public bool HasStarted;

    private void Start()
    {
        beatTempo /= 60f;
    }

    private void Update()
    {
        if (!HasStarted)
        {
            //if (Input.anyKeyDown)
            //{
            //    HasStarted = true;
            //}
        }
        else
        {
            //Start moving down
            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
        }
    }
}
