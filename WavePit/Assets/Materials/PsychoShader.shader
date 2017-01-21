Shader "Custom/PsychoShader" {
	Properties{
		_HueTex ("HueTex", 2D) = "white" {}
		_timeOffset("Time Offset", Float) = 0.5
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
			float _timeOffset;
			struct v2f {
				float4 pos : SV_POSITION;
				float4 color : COLOR0;
				float3 pos3D : POSITION1;
				LIGHTING_COORDS(0, 1)
			};

			v2f vert(appdata_base v)
			{
				v2f o;
				float n = length(v.vertex);
				o.pos = UnityObjectToClipPos(v.vertex + float3(0, sin(_timeOffset + n) * 5, 0));
				o.pos3D = v.vertex;
				// get vertex normal in world space
				half3 worldNormal = UnityObjectToWorldNormal(v.normal);
				// dot product between normal and light direction for
				// standard diffuse (Lambert) lighting
				half nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
				// factor in the light color
				o.color = nl * _LightColor0;
				TRANSFER_VERTEX_TO_FRAGMENT(o);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				float n = length(i.pos3D) * 0.01;
			float attenuation = LIGHT_ATTENUATION(i);
				return tex2D(_HueTex, float2(n + _timeOffset, 0.5f)) * i.color * attenuation;
			}
				ENDCG

			}
	}
	Fallback "Diffuse"
}
