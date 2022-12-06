using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SO_Recipe : MonoBehaviour
{
    public string recipeName;
    [TextArea(5,10)]
    public string description;
    public SO_Ingredient[] ingredients;
}
