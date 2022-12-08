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
        if (HasStarted)
        {
            transform.position -= new Vector3(beatTempo * Time.deltaTime, 0f, 0f);
        }
    }
}
