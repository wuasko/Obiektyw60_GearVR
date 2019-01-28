using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using Assets.Scripts.EyeEffectsPostprocessing;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    //public GameObject playerController;
    public GameObject eyeEffectCamera;
    public GameObject player;

    public GameObject languageManager;

    public GameObject mainCanvas;
    public GameObject mainPanel;
    public GameObject gameModePanel;
    public GameObject storyModePanel;
    public GameObject designerModePanel;
    public GameObject settingsPanel;
    public GameObject aboutPanel;
    public GameObject confirmQuitPanel;

    //public GameObject pauseCanvas;
    public Slider volumeSlider;
    //public Toggle yellowToggle_SM,yellowToggle_DM;

    //public Dropdown visualSelection;

    private GlaucomaEffecet glaucomaScript;
    private DepthOfField depthScript; 
    private YellowEyeEffect yellowScript;

    public AudioMixer audioMixer;

    // Use this for initialization
    void Start()
    {
        glaucomaScript = eyeEffectCamera.GetComponent<GlaucomaEffecet>();
        depthScript = eyeEffectCamera.GetComponent<DepthOfField>();
        yellowScript = eyeEffectCamera.GetComponent<YellowEyeEffect>();

      
        // Initialize language
        if (!PlayerPrefs.HasKey("lang"))
        {
            PlayerPrefs.SetInt("lang",0);
        }

        // Add volume slider listener
        volumeSlider.onValueChanged.AddListener(delegate { VolumeChanged(); });

        // Initialize volume
        if (!PlayerPrefs.HasKey("volume"))
        {
            volumeSlider.value = 50;
            PlayerPrefs.SetInt("volume", 50);
        }
        else
        {
            volumeSlider.value = PlayerPrefs.GetInt("volume");
        }

        // Add yellowing toggle listener
        //yellowToggle_DM.onValueChanged.AddListener(delegate { ToggleYellowing(); });
        //yellowToggle_SM.onValueChanged.AddListener(delegate { ToggleYellowing(); });


        //yellowScript.enabled = false;
        //if (!PlayerPrefs.HasKey("yellowing"))
        //{
        //    yellowToggle_DM.isOn = false;
        //    yellowToggle_SM.isOn = false;
        //    PlayerPrefs.SetInt("yellowing", 0);
        //}
        //else
        //{
        //    volumeSlider.value = PlayerPrefs.GetInt("yellowing");
        //}






        // Reset menu to proper layer configuration
        BackToMainPanel();
    }

    private void VolumeChanged()
    {
        PlayerPrefs.SetInt("volume", Mathf.RoundToInt(volumeSlider.value));
    }

    public void LanguageChanged(String languageFile)
    {
        if (languageFile == "localizedText_pl.json")
        {
            PlayerPrefs.SetInt("lang", 1);
        }
        else
        {
            PlayerPrefs.SetInt("lang", 0);
        }
        Debug.Log("Loading " + languageFile);
        ActivateAllPanels();
        languageManager.GetComponent<LocalizationManager>().LoadLocalizedText(languageFile);
        DeactivateAllPanels();

    }

    // Keep the menu in front of player
    void Update()
    {
        //Should we update the position of the canvas?
        mainCanvas.transform.position = eyeEffectCamera.transform.position + eyeEffectCamera.transform.forward * 1;
        mainCanvas.transform.rotation = new Quaternion(0.0f, eyeEffectCamera.transform.rotation.y, 0.0f, eyeEffectCamera.transform.rotation.w);

    }

    private void ActivateAllPanels()
    {
        mainPanel.SetActive(true);
        gameModePanel.SetActive(true);
        storyModePanel.SetActive(true);
        designerModePanel.SetActive(true);
        settingsPanel.SetActive(true);
        aboutPanel.SetActive(true);
        confirmQuitPanel.SetActive(true);
        Debug.Log("Activating all panels.");
    }

    private void DeactivateAllPanels()
    {
        mainPanel.SetActive(false);
        gameModePanel.SetActive(false);
        storyModePanel.SetActive(false);
        designerModePanel.SetActive(false);
        settingsPanel.SetActive(true);  // Only settings should stay on
        aboutPanel.SetActive(false);
        confirmQuitPanel.SetActive(false);
        Debug.Log("Deactivating panels.");
    }


    public void OpenStartOptionsMenu()
    {
        mainPanel.SetActive(false);
        gameModePanel.SetActive(true);
        storyModePanel.SetActive(false);
        designerModePanel.SetActive(false);
        settingsPanel.SetActive(false);
        aboutPanel.SetActive(false);
        confirmQuitPanel.SetActive(false);
    }

    public void OpenStartStoryMenu()
    {
        gameModePanel.SetActive(false);
        storyModePanel.SetActive(true);

        PlayerPrefs.SetInt("GameMode", 0);
    }

    public void OpenStartDesignerMenu()
    {
        gameModePanel.SetActive(false);
        designerModePanel.SetActive(true);

        PlayerPrefs.SetInt("GameMode", 1);
    }


    // Scene loader
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Debug.Log(PlayerPrefs.GetInt("GameMode"));
    }

    // Back to main panel
    public void BackToMainPanel()
    {
        mainPanel.SetActive(true);
        gameModePanel.SetActive(false);
        storyModePanel.SetActive(false);
        designerModePanel.SetActive(false);
        settingsPanel.SetActive(false);
        aboutPanel.SetActive(false);
        confirmQuitPanel.SetActive(false);
    }

    public void ToggleYellowing(Toggle toggle)
    {
        if (toggle.isOn)
        {
            PlayerPrefs.SetInt("yellowing", 1);
            //yellowToggle_SM.isOn = true;
            //yellowToggle_DM.isOn = true;
            yellowScript.enabled = true;
        }
        else
        {
            yellowScript.enabled = false;
            //yellowToggle_SM.isOn = false;
            //yellowToggle_DM.isOn = false;
            PlayerPrefs.SetInt("yellowing", 0);
        }
    }

    public void ToggleGlaucoma(Toggle toggle)
    {
        if (toggle.isOn)
        {
            glaucomaScript.enabled = true;
        }
        else
        {
            glaucomaScript.enabled = false;
        }
    }
    public void ToggleDepth(Toggle toggle)
    {
        if (toggle.isOn)
        {
            depthScript.enabled = true;
        }
        else
        {
            depthScript.enabled = false;
        }
    }

    public void SetLevel(float sliderValue)
    {
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
    }

    // 
    public void OpenPanelAbout()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(false);
        aboutPanel.SetActive(true);
    }

    // 
    public void OpenPanelSettings()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
        aboutPanel.SetActive(false);
    }

    // Exit button functionality
    public void ExitGame()
    {
        Debug.Log("Quit called!");
        Application.Quit();
    }

    public void OpenPanelConfirmQuit()
    {
        mainPanel.SetActive(false);
        confirmQuitPanel.SetActive(true);
    }

}