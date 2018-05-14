Shader "Unlit/DisolveShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_DisolveTex ("Disolve Texture", 2D) = "white" {}
		_DisolveY ("current y of the disolve effect", float) = 0
		_DisolveTime ("duration of the disolve effect", float) = 2
		_StartingY ("starting point of the disolve", float) = -7
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float3 worldPos : TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _DisolveTex;
			float _DisolveY;
			float _DisolveTime;
			float _StartingY;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture

				float transition = 0;
				clip(0 + (transition + (tex2D(_DisolveTex,i.uv))*_DisolveTime));

				fixed4 col = tex2D(_MainTex, i.uv);
				return col;
			}
			ENDCG
		}
	}
}
