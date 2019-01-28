using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizedText : MonoBehaviour {

    public string key;
    Text text;
    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        text.text = LocalizationManager.instance.GetLocalizedValue(key);
    }
    public void ReloadText()
    {
        text.text = LocalizationManager.instance.GetLocalizedValue(key);
    }
}
