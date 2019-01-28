using System;       
using UnityEngine;


namespace Assets.Scripts.EyeEffectsPostprocessing
{
    /// <summary>
    /// Simulation of eye dissfunction
    /// </summary>
    [ExecuteInEditMode, ImageEffectAllowedInSceneView]
    public class YellowEyeEffect : MonoBehaviour
    {

        /// <summary>
        /// Necessary field of Class (DepthOfFieldShader)
        /// </summary>
        //[HideInInspector]
        public Shader yellEyeShader;
        /// <summary>
        /// sliders to set the color of yellowEyeEffect
        /// </summary>
        //[Range(0.0f, 255.0f), ExecuteInEditMode]
        private float red, green, blue;        

        public Color yellEyeColor;

        [NonSerialized]
        Material yellEyeMaterial = null;
                      

        public void Start()
        {
            //red = 255.0f;
            //green = 248.0f;
            //blue = 207.0f;
            //yellEyeColor = new Color(red, green, blue);
        }

        void OnRenderImage(RenderTexture source, RenderTexture destination)
        { 
            if (yellEyeMaterial == null)
            {
                yellEyeMaterial = new Material(yellEyeShader);
                yellEyeMaterial.hideFlags = HideFlags.HideAndDontSave;
            }


            yellEyeMaterial.SetColor("_YellowishCol", yellEyeColor);
            Graphics.Blit(source, destination, yellEyeMaterial);
        }

    }
}
