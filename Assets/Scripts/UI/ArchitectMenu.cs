using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using Assets.Scripts.EyeEffectsPostprocessing;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class ArchitectMenu : MonoBehaviour
{

    public Toggle wheelchairToggle;
    public GameObject wheelchairMesh;
    public GameObject player;
    public GameObject OVRPlayerController;

    public GameObject eyesCamera;
    public GameObject architectCanvas;

    public GameObject architectPanel;
    public GameObject optionsPanel;
    public GameObject confirmPanel;

    public Slider volumeSlider;
    public Slider sliderYellow;
    public Slider sliderDepth;
    public Slider sliderCataract;
    public Slider sliderGlaucoma;

    private GlaucomaEffecet glaucomaScript;
    private DepthOfField depthScript;
    private YellowEyeEffect yellowScript;
    private CataractManager cataractScript;

    public AudioMixer audio;

    private bool yellow, depth, glaucoma, cataract;


    // Use this for initialization
    void Start()
    {
        yellow = false;
        depth = false;
        glaucoma = false;
        cataract = false;
        glaucomaScript = eyesCamera.GetComponent<GlaucomaEffecet>();
        depthScript = eyesCamera.GetComponent<DepthOfField>();
        yellowScript = eyesCamera.GetComponent<YellowEyeEffect>();
        cataractScript = eyesCamera.GetComponent<CataractManager>();
        architectPanel.SetActive(true);
        optionsPanel.SetActive(false);
        confirmPanel.SetActive(false);

        cataractScript.enabled = true;
        wheelchairToggle.onValueChanged.AddListener(delegate {
            wheelchairToggleChanged(wheelchairToggle);
        });
        sliderYellow.onValueChanged.AddListener(delegate {
            sliderYellowToggle(sliderYellow);
        });
        sliderDepth.onValueChanged.AddListener(delegate {
            sliderDepthToggle(sliderDepth);
        });
        sliderCataract.onValueChanged.AddListener(delegate {
            sliderCataractToggle(sliderCataract);
        });
        sliderGlaucoma.onValueChanged.AddListener(delegate {
            sliderGlaucomaToggle(sliderGlaucoma);
        });

        volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);

        cataractScript.enabled = true;

    }

    public void SetLevel(float sliderValue)
    {
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
        audio.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void yellowToggle()
    {

        if (yellow)
        {
            yellowScript.enabled = false;
            yellow = false;
        }
        else
        {
            yellow = true;
            yellowScript.enabled = true;
        }
    }
    public void sliderYellowToggle(Slider slider)
    {
        switch ((int)slider.value)
        {
            case 0:
                yellowScript.enabled = false;
                yellow = false;
                break;
            case 1:
                yellow = true;
                yellowScript.enabled = true;
                break;


        } 
    }
    public void sliderDepthToggle(Slider slider)
    {
        switch ((int)slider.value)
        {
            case 0:
                depthScript.enabled = false;
                depth = false;
                break;
            case 1:
                depth = true;
                depthScript.enabled = true;
                break;


        }
    }
    public void sliderCataractToggle(Slider slider)
    {
        switch ((int)slider.value)
        {
            case 0:
                //cataractScript.enabled = false;
                cataractScript.ChangeEnableOfCataract(false);

                cataract = false;
                break;
            case 1:
                cataract = true;
                cataractScript.ChangeEnableOfCataract(true);
                break;
        }
    }
    public void sliderGlaucomaToggle(Slider slider)
    {
        switch ((int)slider.value)
        {
            case 0:
                glaucomaScript.enabled = false;
                glaucoma = false;
                break;
            case 1:
                glaucoma = true;
                glaucomaScript.enabled = true;
                break;
        }
    }
    public void depthToggle()
    {
        if (depth)
        {
            depth = false;
            depthScript.enabled = false;
        }
        else
        {
            depth = true;
            depthScript.enabled = true;
        }
    }
    public void glaucomaToggle()
    {
        if (glaucoma)
        {
            glaucoma = false;
            glaucomaScript.enabled = false;
        }
        else
        {
            glaucoma = true;
            glaucomaScript.enabled = true;
        }
    }
    public void cataractToggle()
    {
        if (glaucoma)
        {
            cataract = false;
            cataractScript.enabled = false;
        }
        else
        {
            cataract = true;
            cataractScript.enabled = true;
        }
    }

    private void wheelchairToggleChanged(Toggle wheelchairToggle)
    {
        ///Component wheelchairMovement = player.GetComponent("Wheelchair Movement");


        if (wheelchairToggle.isOn)
        {
            wheelchairMesh.SetActive(true);
        }
        else
        {
            wheelchairMesh.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //
        //tectCanvas.transform.position = eyesCamera.transform.position + eyesCamera.transform.forward * 1;
        //architectCanvas.transform.rotation = new Quaternion(0.0f, eyesCamera.transform.rotation.y, 0.0f, eyesCamera.transform.rotation.w);
        //architectCanvas.transform.rotation = new Quaternion(0.0f, eyesCamera.transform.rotation.y, 0.0f, eyesCamera.transform.rotation.w);
    }

    public void openSettings()
    {
        optionsPanel.SetActive(true);
        confirmPanel.SetActive(false);
    }

    public void openConfirm()
    {
        optionsPanel.SetActive(false);
        confirmPanel.SetActive(true);
    }

    public void resume()
    {
        optionsPanel.SetActive(false);
        confirmPanel.SetActive(false);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
