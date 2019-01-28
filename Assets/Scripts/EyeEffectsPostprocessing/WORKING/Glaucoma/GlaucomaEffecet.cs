using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode, ImageEffectAllowedInSceneView]
public class GlaucomaEffecet : MonoBehaviour {

    public Shader glaucomaShader;

    [NonSerialized]
    Material material = null;

    [Range(0.0f, 1.0f)]
    public float radius = 0.4f;

    [Range(0.0f, 1.0f)]
    public float soft = 0.5f;
    [Range(0.0f, 1.0f)]
    public float circleColor = 0.1f;

    public void Start()
    {

    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (material == null)
        {
            material = new Material(glaucomaShader);
            material.hideFlags = HideFlags.HideAndDontSave;
        }

        material.SetFloat("_VRadius", radius);
        material.SetFloat("_VSoft", soft);
        material.SetFloat("_CircleFloatColor", circleColor);
        Graphics.Blit(source, destination, material);
    }
}
