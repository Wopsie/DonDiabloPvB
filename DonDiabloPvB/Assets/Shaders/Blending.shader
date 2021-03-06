﻿Shader "Unlit/Blending"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_SecondTex ("Second Texture", 2D) = "white" {}

		_LerpValue ("value of lerp", Range(0,1)) = 0.5

		_DisolveCount ("Color controller", float) = 0
	}
	SubShader
	{
        Tags {"Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        ZWrite Off

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
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
			};

			sampler2D _MainTex;
			sampler2D _SecondTex;

			float4 _MainTex_ST;
			float4 _SecondTex_ST;

			float _LerpValue;

			float _DisolveCount;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);

				clip(_DisolveCount + tex2D(_SecondTex,i.uv));
				return col;
			}
			ENDCG
		}
	}
}
