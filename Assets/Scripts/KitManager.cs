using System.IO;
using UnityEngine;

public class KitManager : MonoBehaviour
{
    public TextAsset defaultJson;
    public TextAsset ogJson;
    public KitButton[] buttons;

    private Kits data;
    private string path;
    private string ogpath;

    void Start()
    {
        path = Path.Combine(Application.persistentDataPath, "Kits.json");
        path = Path.Combine(Application.persistentDataPath, "KitsOg.json");
        Load();
        RefreshAll();
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus == true && data != null)
        {
            Load();
            RefreshAll();
        }
    }

    public void Load()
    {
        if (File.Exists(path) == false)
        {
            File.WriteAllText(path, defaultJson.text);
        }

        string json = File.ReadAllText(path);
        data = JsonUtility.FromJson<Kits>(json);
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
    }

    private KitData FindKit(int kitNumber)
    {
        for (int i = 0; i < data.kits.Length; i++)
        {
            if (data.kits[i].kitNumber == kitNumber)
            {
                return data.kits[i];
            }
        }
        return null;
    }

    public void SetAvailability(int kitNumber, int state)
    {
        KitData kit = FindKit(kitNumber);

        if (kit == null)
        {
            return;
        }

        kit.available = state;
        Save();
        RefreshAll();
    }

    public void RefreshAll()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            KitData kit = FindKit(buttons[i].kitNumber);
            buttons[i].Apply(kit);
        }
    }

    public void ResetKits()
    {
        if (File.Exists(ogpath) == false)
        {
            File.WriteAllText(ogpath, ogJson.text);
        }
        string json = File.ReadAllText(ogpath);
        data = JsonUtility.FromJson<Kits>(json);
        string jsonOg = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, jsonOg);
    }
}