// Upgrade NOTE: replaced 'samplerRECT' with 'sampler2D'
// Upgrade NOTE: replaced 'texRECT' with 'tex2D'


Shader "Custom/Cataract" {
	Properties{
		_MainTex("Input", RECT) = "white" {}
		_BlurStrength("Blur strengh", Float) = 0.1
		_BlurWidth("Blur Widh", Float) = 0.1
		//_BlurWidth("Color of Blur", Color) = (0.8, 0.75, 0.7, 1.0)
		_Brightness("set Brightness", Float) = 1.0
	}
	SubShader{
		Pass
		{
			ZTest Always Cull Off ZWrite Off
			Fog
			{ 
				Mode off 
			}

			CGPROGRAM
			// Upgrade NOTE: excluded shader from DX11, OpenGL ES 2.0 because it uses unsized arrays
			//#pragma exclude_renderers d3d11 gles

			#pragma vertex vert_img
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest

			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform float _BlurStrength;
			uniform float _BlurWidth;
			uniform float _Brightness;

			float4 frag(v2f_img i) : COLOR
			{
				float4 color = tex2D(_MainTex, i.uv);

				// some sample positions
				//float samples[10] = float[](-0.08,-0.05,-0.03,-0.02,-0.01,0.01,0.02,0.03,0.05,0.08);
				static const int kernelSampleCount = 10;
				static const float samples[kernelSampleCount] = {
					float(-0.008),
					float(-0.005),
					float(-0.003),
					float(-0.002),
					float(-0.001),
					float(0.001),
					float(0.002),
					float(0.003),
					float(0.005),
					float(0.008),
				};

				//vector to the middle of the screen
				float2 dir = 0.5 - i.uv;

				//distance to center
				float dist = sqrt(dir.x*dir.x + dir.y*dir.y);  //distance(input.uv.xy, float2(0.5, 0.5));

				//normalize direction
				dir = dir / dist;

				//additional samples towards center of screen
				float4 sum = color;
				for (int n = 0; n < 10; n++)
				{
					sum += tex2D(_MainTex, i.uv + dir * _BlurWidth);// *samples[n] );
				}

				//eleven samples...
				sum = sum * (1.0 / 5.5);

				//weighten blur depending on distance to screen center
				float t = saturate(dist * _BlurStrength);
				
				//blend original with blur
			
				return lerp(sum, color, t) * _Brightness;
				//return color * _Brightness;
			}
			ENDCG
		}
	}
}

