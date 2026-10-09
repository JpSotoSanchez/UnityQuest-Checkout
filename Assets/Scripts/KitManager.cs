using System;
using System.IO;
using UnityEngine;

public class KitManager : MonoBehaviour
{
    public TextAsset defaultJson;
    public TextAsset ogJson;
    public KitButton[] buttons;

    private Kits data;
    private string path;

    void Awake()
    {
        path = Path.Combine(Application.persistentDataPath, "Kits.json");
    }

    void Start()
    {
        Load();
        RefreshAll();

        // Check expired dates every 60 seconds while the app is open
        InvokeRepeating("CheckExpired", 60f, 60f);
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
        // First run: copy the default JSON to the persistent path
        if (File.Exists(path) == false)
        {
            File.WriteAllText(path, defaultJson.text);
        }

        string json = File.ReadAllText(path);
        data = JsonUtility.FromJson<Kits>(json);

        CheckExpired();
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
    }

    // Returns the kit with the given number, or null if it does not exist
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

    // If a kit is in state 2 and its date has passed, move it to another state
    public void CheckExpired()
    {
        DateTime now = DateTime.Now;
        bool changed = false;

        for (int i = 0; i < data.kits.Length; i++)
        {
            KitData kit = data.kits[i];

            if (kit.available == 2 && kit.GetDate() <= now)
            {
                if (kit.complete == false)
                {
                    kit.available = 3;   // incomplete
                }
                else
                {
                    kit.available = 1;   // charging
                }
                changed = true;
            }
        }

        if (changed == true)
        {
            Save();
            RefreshAll();
        }
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

    // Rents a kit until the given date
    public void SetRental(int kitNumber, DateTime until)
    {
        KitData kit = FindKit(kitNumber);
        if (kit == null)
        {
            return;
        }

        kit.available = 2;
        kit.SetDate(until);
        Save();
        RefreshAll();
    }

    // Changes only the date (for example, to extend a rental)
    public void SetDate(int kitNumber, DateTime newDate)
    {
        KitData kit = FindKit(kitNumber);
        if (kit == null)
        {
            return;
        }

        kit.SetDate(newDate);
        Save();
        RefreshAll();
    }

    public void SetComplete(int kitNumber, bool complete)
    {
        KitData kit = FindKit(kitNumber);
        if (kit == null)
        {
            return;
        }

        kit.complete = complete;
        Save();
    }

    public void RefreshAll()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            KitData kit = FindKit(buttons[i].kitNumber);
            buttons[i].Apply(kit);
        }
    }

    // Overwrites the saved file with the original JSON and reloads everything
    public void ResetKits()
    {
        File.WriteAllText(path, ogJson.text);
        Load();
        RefreshAll();
    }
}