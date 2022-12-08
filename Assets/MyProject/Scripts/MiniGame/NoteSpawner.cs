using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] NotesToSpawn;
    [SerializeField] int distanceBetweenNotes;
    public IEnumerator CreateNotes(int counter)
    {
        while (counter > 0)
        {
            GameObject myNote = Instantiate(NotesToSpawn[Random.Range(0, NotesToSpawn.Length -1)]);
            myNote.transform.position = new Vector3(transform.position.x, transform.position.y);

            counter--;
            if(counter == 0)
            {
                Debug.Log("This is the last note");
            }
            yield return new WaitForSeconds(1.0f);
        }
    }
}
