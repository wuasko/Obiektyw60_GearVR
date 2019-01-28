using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CataractManager : MonoBehaviour
{

    private bool flaga;

    Camera mainCamera;

    CataractEffect cataractEffect;
    BlurOptimized blurOptimized;
    RGBtoHSL rgb2hsl;

    // Use this for initialization
    void Start()
    {
        Camera[] allCameras = Camera.allCameras;
        foreach (Camera c in allCameras)
        {
            if (c.name == "CenterEyeAnchor")
                mainCamera = c;
        }

        flaga = false;
        ChangeEnableOfCataract(flaga);
    }

    // Update is called once per frame
    void Update()
    {

        //some input triiger (choose one that you want to)
        /*if (Input.GetKey(KeyCode.Space))
        {
            flaga = !flaga;
            ChangeEnableOfCataract(flaga);
        } */

    }

    public void ChangeEnableOfCataract(bool bEnable)
    {

        rgb2hsl = (RGBtoHSL)mainCamera.GetComponent(typeof(RGBtoHSL));
        cataractEffect = (CataractEffect)mainCamera.GetComponent(typeof(CataractEffect));
        blurOptimized = (BlurOptimized)mainCamera.GetComponent(typeof(BlurOptimized));

        blurOptimized.enabled = bEnable;
        cataractEffect.enabled = bEnable;
        rgb2hsl.enabled = bEnable;
        
       

        /* GetComponent<RGBtoHSL>().enabled = bEnable;
        GetComponent<BlurOptimized>().enabled = bEnable;
        GetComponent<CataractEffect>().enabled = bEnable;    */
    }
}
