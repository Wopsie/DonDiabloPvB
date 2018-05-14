Shader "Custom/BlendingShader" {
	Properties
    {
        _MainTex ("Albedo Texture", 2D) = "white" {}
		_GradientTex ("Gradiant Texture", 2D) = "white"{}
        _TintColor("Tint Color", Color) = (1,1,1,1)
		_GradientColor("Gradiant Color", Color) = (1,1,1,1)
        _Transparency("Transparency", Range(1,1)) = 1
        _CutoutThresh("Cutout Threshold", Range(1,1)) = 1
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
			sampler2D _GradientTex;
            float4 _MainTex_ST;
			float4 _GradientTex_ST;
            float4 _TintColor;
			float4 _GradientColor;
            float _Transparency;
            float _CutoutThresh;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _GradientTex);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv) + _TintColor;
                col.a = (tex2D(_GradientTex, i.uv) + _GradientColor).a;
                clip(col.r - _CutoutThresh);
                return col;
            }
            ENDCG
        }
    }
}
