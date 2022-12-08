using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] NotesToSpawn;
    [SerializeField] Transform noteParent;
    int counter = 10;
    private void Start()
    {
        GameObject myNote = Instantiate(NotesToSpawn[0], noteParent);
        myNote.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y);

        StartCoroutine(CreateNotes());
    }

    public IEnumerator CreateNotes()
    {
        while (counter > 0)
        {
            Debug.Log("I Create Notes");

            yield return new WaitForSeconds(1.0f);
        }
    }
}
