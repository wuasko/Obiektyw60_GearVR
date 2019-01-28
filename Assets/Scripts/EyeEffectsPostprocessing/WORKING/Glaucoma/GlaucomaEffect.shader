Shader "Custom/GlaucomaEffect"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_VRadius("Vignette Radius", Range(0.0, 1.0)) = 0.3
		_VSoft("Vignette Softness", Range(0.0, 1.0)) = 0.5
		_CircleFloatColor("The color of the circle", Float) = 0.1
	}

		SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#include "UnityCG.cginc"

			// Properties
			sampler2D _MainTex;
			float _VRadius;
			float _VSoft;
			float _CircleFloatColor;

			float4 frag(v2f_img input) : COLOR
			{
				// sample texture for color
				float4 base = tex2D(_MainTex, input.uv);
				
				// add vignette
				float distFromCenter = distance(input.uv.xy, float2(0.5, 0.5));
				float vignette = smoothstep(_VRadius, _VRadius - _VSoft, distFromCenter);
				base = base * vignette;

				return saturate(base);
			}

			ENDCG
		}
	}
}