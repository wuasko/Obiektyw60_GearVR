using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalizationManager : MonoBehaviour {

    public static LocalizationManager instance;
    string result = "missing";

    private Dictionary<string, string> localizedText;
    // Enforce singleton pattern
    void Awake() {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        // Initial language
        if (PlayerPrefs.GetInt("lang") == 0)
        {

            LoadLocalizedText("localizedText_en.json");
        }
        else
        {
            LoadLocalizedText("localizedText_pl.json");
        }
        //LoadLocalizedText("localizedText_en.json");
    }

    public void LoadLocalizedText(string fileName)
    {
        localizedText = new Dictionary<string, string>();
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

            for (int i = 0; i < loadedData.items.Length; i++)
            {
                localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
            }
            Debug.Log("Data loaded, dictionary contains: " + localizedText.Count + " entries.");
        }
        else
        {
            Debug.LogError("Cannot find file");
        }
        ReloadScene();
    }

    public string GetLocalizedValue(string key)
    {
        if (localizedText.ContainsKey(key))
        {
            result = localizedText[key];
        }
        return result;
    }

    private void ReloadScene()
    {
        Debug.Log("Reloading scene");
        //Application.LoadLevel(Application.loadedLevel);
        GameObject[] buttonLabels;
        buttonLabels = GameObject.FindGameObjectsWithTag("Lang");
        foreach (GameObject button in buttonLabels)
        {
            try{
                button.GetComponent<LocalizedText>().ReloadText();
            }
            catch (System.Exception e1){
                try
                {
                    button.GetComponent<LocalizedImage>().ReloadImage();
                    print("Reloading image");
                }
                catch (System.Exception e)
                {
                    print("Localization error: " + button.transform.name);
                }
            }
        }
    }
}
