// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/HSL" {

	Properties{

		_MainTex("Texture", 2D) = "white" {}

		_HueShift("HueShift", Float) = 0

		_Sat("Saturation", Float) = 1

		_Bright("Brighness", Float) = 1

	}

	SubShader{



		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }

		ZWrite Off

		Blend SrcAlpha OneMinusSrcAlpha

		Cull Off



		Pass

		{

			CGPROGRAM

			#pragma vertex vert

			#pragma fragment frag

			#pragma target 2.0



			#include "UnityCG.cginc"


			//------------Converting Pure HUE to RGB/------------
			float3 HUEtoRGB(in float H)
			{
				float R = abs(H * 6 - 3) - 1;
				float G = 2 - abs(H * 6 - 2);
				float B = 2 - abs(H * 6 - 4);
				return saturate(float3(R,G,B));
			}
			
			//------------Converting RGB/chroma/value to hue------------
			float RGBCVtoHUE(in float3 RGB, in float C, in float V)
			{
				float3 Delta = (V - RGB) / C;
				Delta.rgb -= Delta.brg;
				Delta.rgb += float3(2,4,6);
				// NOTE 1
				Delta.brg = step(V, RGB) * Delta.brg;
				float H;
				//#if NO_ASM
				H = max(Delta.r, max(Delta.g, Delta.b));
				//#else
				//float4 Delta4 = Delta.rgbr;
				//asm { max4 H, Delta4 };
				//#endif
				return frac(H / 6);
			}

			//------------Converting HSV to RGB/------------
			float3 HSVtoRGB(in float3 HSV)
			{
				float3 RGB = HUEtoRGB(HSV.x);
				return ((RGB - 1) * HSV.y + 1) * HSV.z;
			}

			//------------Converting HSL to RGB/------------
			float3 HSLtoRGB(in float3 HSL)
			{
				float3 RGB = HUEtoRGB(HSL.x);
				float C = (1 - abs(2 * HSL.z - 1)) * HSL.y;
				return (RGB - 0.5) * C + HSL.z;
			}

			//------------Converting HCY to RGB/------------
			// The weights of RGB contributions to luminance.
			// Should sum to unity.
			float3 HCYwts = float3(0.299, 0.587, 0.114);

			float3 HCYtoRGB(in float3 HCY)
			{
				float3 RGB = HUEtoRGB(HCY.x);
				float Z = dot(RGB, HCYwts);
				if (HCY.z < Z)
				{
					HCY.y *= HCY.z / Z;
				}
				else if (Z < 1)
				{
					HCY.y *= (1 - HCY.z) / (1 - Z);
				}
				return (RGB - Z) * HCY.y + HCY.z;
			}

			//------------Converting HCL to RGB/------------
			float HCLgamma = 3;
			float HCLy0 = 100;
			float HCLmaxL = 0.530454533953517; // == exp(HCLgamma / HCLy0) - 0.5
			float PI = 3.1415926536;

			float3 HCLtoRGB(in float3 HCL)
			{
				float3 RGB = 0;
				if (HCL.z != 0)
				{
					float H = HCL.x;
					float C = HCL.y;
					float L = HCL.z * HCLmaxL;
					float Q = exp((1 - C / (2 * L)) * (HCLgamma / HCLy0));
					float U = (2 * L - C) / (2 * Q - 1);
					float V = C / Q;
					float T = tan((H + min(frac(2 * H) / 4, frac(-2 * H) / 8)) * PI * 2);
					H *= 6;
					if (H <= 1)
					{
						RGB.r = 1;
						RGB.g = T / (1 + T);
					}
					else if (H <= 2)
					{
						RGB.r = (1 + T) / T;
						RGB.g = 1;
					}
					else if (H <= 3)
					{
						RGB.g = 1;
						RGB.b = 1 + T;
					}
					else if (H <= 4)
					{
						RGB.g = 1 / (1 + T);
						RGB.b = 1;
					}
					else if (H <= 5)
					{
						RGB.r = -1 / T;
						RGB.b = 1;
					}
					else
					{
						RGB.r = 1;
						RGB.b = -T;
					}
					RGB = RGB * V + U;
				}
				return RGB;
			}

			//------------Converting RGB to HSV------------
				float3 RGBtoHSV(in float3 RGB)
			{
				float3 HSV = 0;
				//#if NO_ASM
				HSV.z = max(RGB.r, max(RGB.g, RGB.b));
				float M = min(RGB.r, min(RGB.g, RGB.b));
				float C = HSV.z - M;
				//#else
				//float4 RGB4 = RGB.rgbr;
				//asm { max4 HSV.z, RGB4 };
				//asm { max4 RGB4.w, -RGB4 };
				//float C = HSV.z + RGB4.w;
				//#endif
				if (C != 0)
				{
					HSV.x = RGBCVtoHUE(RGB, C, HSV.z);
					HSV.y = C / HSV.z;
				}
				return HSV;
			}


			//------------Converting RGB to HSL------------
			float3 RGBtoHSL(in float3 RGB)
			{
				float3 HSL = 0;
				float U, V;
				//#if NO_ASM
				U = -min(RGB.r, min(RGB.g, RGB.b));
				V = max(RGB.r, max(RGB.g, RGB.b));
				//#else
				//float4 RGB4 = RGB.rgbr;
				//asm { max4 U, -RGB4 };
				//asm { max4 V, RGB4 };
				//#endif
				HSL.z = (V - U) * 0.5;
				float C = V + U;
				if (C != 0)
				{
					HSL.x = RGBCVtoHUE(RGB, C, V);
					HSL.y = C / (1 - abs(2 * HSL.z - 1));
				}
				return HSL;
			}


			//------------Converting RGB to HCY------------
			float3 RGBtoHCY(in float3 RGB)
			{
				float3 HCY = 0;
				float U, V;
				//#if NO_ASM
				U = -min(RGB.r, min(RGB.g, RGB.b));
				V = max(RGB.r, max(RGB.g, RGB.b));
				//#else
				//float4 RGB4 = RGB.rgbr;
				//asm { max4 U, -RGB4 };
				//asm { max4 V, RGB4 };
				//#endif
				HCY.y = V + U;
				HCY.z = dot(RGB, HCYwts);
				if (HCY.y != 0)
				{
					HCY.x = RGBCVtoHUE(RGB, HCY.y, V);
					float Z = dot(HUEtoRGB(HCY.x), HCYwts);
					if (HCY.z > Z)
					{
						HCY.z = 1 - HCY.z;
						Z = 1 - Z;
					}
					HCY.y *= Z / HCY.z;
				}
				return HCY;
			}


			//------------Converting RGB to HCL------------
			float3 RGBtoHCL(in float3 RGB)
			{
				float3 HCL;
				float H = 0;
				float U, V;
				//#if NO_ASM
				U = -min(RGB.r, min(RGB.g, RGB.b));
				V = max(RGB.r, max(RGB.g, RGB.b));
				//#else
				//float4 RGB4 = RGB.rgbr;
				//asm { max4 U, -RGB4 };
				//asm { max4 V, RGB4 };
				//#endif
				float Q = HCLgamma / HCLy0;
				HCL.y = V + U;
				if (HCL.y != 0)
				{
					H = atan2(RGB.g - RGB.b, RGB.r - RGB.g) / PI;
					Q *= -U / V;
				}
				Q = exp(Q);
				HCL.x = frac(H / 2 - min(frac(H), frac(-H)) / 6);
				HCL.y *= Q;
				HCL.z = lerp(U, V, Q) / (HCLmaxL * 2);
				return HCL;
			}


			struct v2f {

				float4  pos : SV_POSITION;

				float2  uv : TEXCOORD0;

			};



			float4 _MainTex_ST;



			v2f vert(appdata_base v)

			{

				v2f o;

				o.pos = UnityObjectToClipPos(v.vertex);

				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);

				return o;

			}



			sampler2D _MainTex;

			float _HueShift;

			float _Sat;

			float _Bright;



			half4 frag(v2f i) : COLOR

			{

				half4 col = tex2D(_MainTex, i.uv);

				float3 hsl = float3(_HueShift, _Sat, _Bright);

				//1ST convert RGB -> HSL

				float3 tempCol1;
				tempCol1 = RGBtoHSL(col);

				//2ND set the hue, saturation and brightness properties

				tempCol1.x *= hsl.x;
				tempCol1.y *= hsl.y;
				tempCol1.z *= hsl.z;

				//3DT back converting from HSL -> RGB

				float3 returnCol;
				returnCol = HSLtoRGB(tempCol1);

				// return the changed RGB result

				//return float4(tempCol1, col.a);
				return float4(returnCol, col.a);


			}

			ENDCG

		}

	}

		Fallback "Particles/Alpha Blended"

}