using UnityEngine;
using UnityEditor;
using System.IO;

public class CSVToSO
{
    private static string CSVItems = "/Editor/CSV/ItemsTabelle.csv";
    [MenuItem("Utilities/GenerateItems")]
    public static void GenerateSO()
    {
        Debug.Log("Generate Items");
        string[] allLines = File.ReadAllLines(Application.dataPath + CSVItems);

        foreach (string s in allLines)
        {
            string[] splitData = s.Split(',');

            SO_Ingredient tester = ScriptableObject.CreateInstance<SO_Ingredient>();
            tester.ingredientName = splitData[0];

            //Knowledge of unity of all data //Path has alrdy to be exist
            AssetDatabase.CreateAsset(tester, $"Assets/MyProject/Scriptables/Cooking/{tester.ingredientName}.asset");

        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    [MenuItem("Utilities/GenerateRecipes")]
    public static void GenerateRecipes()
    {
        Debug.Log("Generate SO");
        string[] allLines = File.ReadAllLines(Application.dataPath + CSVItems);

        foreach (string s in allLines)
        {
            string[] splitData = s.Split(';');

            SO_Ingredient tester = ScriptableObject.CreateInstance<SO_Ingredient>();
            tester.ingredientName = splitData[0];

            //Knowledge of unity of all data //Path has alrdy to be exist
            AssetDatabase.CreateAsset(tester, $"Assets/MyProject/Scriptables/{tester.ingredientName}.asset");

        }

        AssetDatabase.SaveAssets();
    }
}
