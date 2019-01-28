using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode, ImageEffectAllowedInSceneView]
public class CataractEffect : MonoBehaviour {

    public Shader cataractShader;    

    [NonSerialized]
    Material material = null;

    [Range(0.3f, 1.6f)]
    public float brightness = 0.6f;

    [Range(0.0f, 3.0f)]
    public float blurStrength = 0.0f;
    [Range(0.0f, 3.0f)]
    public float blurWidth = 0.0f;        

    public void Start()
    {  

    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (material == null)
        {
            material = new Material(cataractShader)
            {
                hideFlags = HideFlags.HideAndDontSave
            };
        }

        material.SetFloat("_BlurStrength", blurStrength);
        material.SetFloat("_BlurWidth", blurWidth);    
        material.SetFloat("_Brightness", brightness);

        Graphics.Blit(source, destination, material);
    }
}
