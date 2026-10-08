using System.IO;
using UnityEngine;

public class JSONReader : MonoBehaviour
{
    public TextAsset jsonFile;

    void Start()
    {
        Kits kitsInJson = JsonUtility.FromJson<Kits>(jsonFile.text);

        foreach (KitData kit in kitsInJson.kits)
        {
            Debug.Log("Found kit: " + kit.kitNumber + " " + kit.available);
        }
    }
    private void WriteData(KitData kit)
    {
        string json = JsonUtility.ToJson(kit, true);
        File.WriteAllText("", json);
    }

    
}