using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode, ImageEffectAllowedInSceneView]
public class RGBtoHSL : MonoBehaviour {

    public Shader cataractShader;

    [NonSerialized]
    Material material = null;

    [Range(0.0f, 1.0f)]
    public float hueShift = 1.0f;
    [Range(0.0f, 1.0f)]
    public float saturation = 0.6f;
    [Range(0.0f, 1.0f)]
    public float brightness = 1.0f;
                   
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (material == null)
        {
            material = new Material(cataractShader)
            {
                hideFlags = HideFlags.HideAndDontSave
            };
        }
         
        material.SetFloat("_HueShift", hueShift);
        material.SetFloat("_Sat", saturation);
        material.SetFloat("_Bright", brightness);
        Graphics.Blit(source, destination, material);
    }
}
