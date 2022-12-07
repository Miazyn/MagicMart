using UnityEngine;
using UnityEditor;
using System.IO;

public class CSVToSO
{
    //private static string CSVPath = "/Editor/CSV/MYCSV.csv";
    [MenuItem("Utilities/GenerateSO")]
    public static void GenerateSO()
    {
        Debug.Log("Generate SO");
        //string[] allLines = File.ReadAllLines(Application.dataPath + CSVPath);

        //foreach(string s in allLines)
        //{
        //    string[] splitData = s.Split(';');

        //    if(splitData.Length != 3)
        //    {
        //        Debug.Log("not 3 values");
        //        return;
        //    }

        //    SO_Tester tester = ScriptableObject.CreateInstance<SO_Tester>();
        //    tester.testerName = splitData[0];
        //    tester.damage = int.Parse(splitData[1]);
        //    tester.health = int.Parse(splitData[2]);

        //    //Knowledge of unity of all data //Path has alrdy to be exist
        //    AssetDatabase.CreateAsset(tester, $"Assets/MyProject/Scriptables/{tester.testerName}.asset");

        //}

        //AssetDatabase.SaveAssets();
    }
}
