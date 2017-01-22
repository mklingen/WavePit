// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/PsychoShader" {
	Properties{
		_HueTex ("HueTex", 2D) = "white" {}
		_timeOffset("Time Offset", Float) = 0.5
		_waveRadius("Wave Radius", Float) = 1.0
		_baseColor("Base Color", Color) = (1, 1, 1, 1)
	    _MainTex("Main Texture", 2D) = "white" {}
		}
		SubShader{
			Tags {"LightMode" = "ForwardBase"}
			Pass{

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			#include "UnityLightingCommon.cginc" // for _LightColor0
#pragma multi_compile_fwdbase
			#include "AutoLight.cginc"
			sampler2D _HueTex;
			sampler2D _MainTex;
			float _timeOffset;
			float _waveRadius;
			float4 _baseColor;
			float4 _MainTex_ST;
			struct v2f {
				float4 pos : SV_POSITION;
				float4 color : COLOR0;
				float3 pos3D : POSITION1;
				float2 uv : TEXCOORD1;
				LIGHTING_COORDS(0, 1)
			};

			v2f vert(appdata_base v)
			{
				v2f o;
				float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				float n = length(worldPos);
				float texBlend = 1.0 - clamp(abs(_waveRadius - n) * 0.1f, 0, 1);
				o.pos = UnityObjectToClipPos(v.vertex + float3(0, sin(_timeOffset * 2 + n) * texBlend * 2, 0));
				o.pos3D = worldPos;
				// get vertex normal in world space
				half3 worldNormal = UnityObjectToWorldNormal(v.normal);
				// dot product between normal and light direction for
				// standard diffuse (Lambert) lighting
				half nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
				// factor in the light color
				o.color = nl * _LightColor0 * _baseColor;
				TRANSFER_VERTEX_TO_FRAGMENT(o);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				float n = length(i.pos3D);
				float4 diffuse_color = tex2D(_MainTex, i.uv);
				float4 texColor = tex2D(_HueTex, float2((n * 0.01 * diffuse_color.x + _timeOffset / 6), _timeOffset));
				float texBlend = clamp(abs(_waveRadius - n) * 0.1f, 0, 1);
				return  (texColor * (1.0 - texBlend) + (texBlend) * (i.color * 0.75 + 0.25 * diffuse_color)) * LIGHT_ATTENUATION(i);
			}
				ENDCG

			}
	}
	Fallback "Diffuse"
}
