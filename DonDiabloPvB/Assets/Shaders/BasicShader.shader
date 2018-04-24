Shader "Custom/BasicShader" 
{
	Properties
	{
		_MainTex("Texture Map", 2D) = "White"{}


		
		_BumpMap ("Normal Map", 2D) = "Bump"{}
		_BumpPower ("Normal Power", float) = 1

		[Header(Normal Color)]
		_ColorNormal ("Normal Color", color) = (1,1,1,0)

		[Header(Dissolve Text)]
		_DissolveTex ("Dissolve Map", 2D) = "White"{}

		[Header(Dissolving Slider)]
		_DissolveIntensity ("Dissolve Intensity", Range(0.0, 1.0)) = 0
	
		[Header(Edge Settings)]
		_DissolveEdgeRange ("Dissolve Edge Range", Range(0.0, 1.0)) = 0
		_DissolveEdgeMultiplier ("Dissolve Edge Multiplier", Float) = 1
		_DissolveEdgeColor ("Dissolve Edge Color", Color) = (1,1,1,0)

		[Header(Shield Color)]
		_ColorTint ("Color Tint", Color) = (1,1,1,1)

		[Header(Rim Settings)]
		_RimColor ("Rim Color", color) = (1,1,1,1)
		_RimPower ("Rim Power", Range(1.0,6.0)) = 3.0
	} 	


	SubShader
    {
      Tags { "RenderType"="Opaque" }

      CGPROGRAM
      #pragma surface surf Lambert

      struct Input
      {
        float2 uv_MainTex;
		float2 uv_BumpMap;
		float3 worldNormal;
		float3 viewDir;
		float4 color : Color;
      };
     
		sampler2D _MainTex;
		sampler2D _BumpMap;
		float4 _ColorTint;
		float4 _RimColor;
		float _RimPower;
    
      void surf (Input IN, inout SurfaceOutput o)
      {
      	IN.color = _ColorTint;
		o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb * IN.color;
		o.Normal = UnpackNormal(tex2D (_BumpMap, IN.uv_BumpMap));
		o.Alpha = _ColorTint.a;

		half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
		o.Emission = _RimColor.rgb * pow(rim, _RimPower);
      }
      ENDCG

	  CGPROGRAM
      #pragma surface surf Lambert

      struct Input
      {
          float2 uv_MainTex;
          float2 uv_BumpMap;
          float2 uv_DissolveTex;
          float3 worldPos;
          float3 viewDir;
          float3 worldNormal;
      };

      sampler2D _MainTex;
      sampler2D _BumpMap;
      sampler2D _DissolveTex;

      float _BumpPower;      
      float _DissolveEdgeRange;
      float _DissolveIntensity;
      float _DissolveEdgeMultiplier;
      float4 _DissolveEdgeColor;
      float4 _Color;

      void surf (Input IN, inout SurfaceOutput o)
      {
        float4 dissolveColor = tex2D(_DissolveTex, IN.uv_DissolveTex);                  
        half dissolveClip = dissolveColor.r - _DissolveIntensity;
        half edgeRamp = max(0, _DissolveEdgeRange - dissolveClip);
        clip(dissolveClip);

        float4 texColor = tex2D(_MainTex, IN.uv_MainTex);               
        o.Albedo = lerp( texColor, _DissolveEdgeColor, min(1, edgeRamp * _DissolveEdgeMultiplier));

        fixed3 normal = UnpackNormal(tex2D(_BumpMap,IN.uv_BumpMap) );
        normal.z = normal.z / _BumpPower;

        o.Normal = normalize(normal);
        return;
      }
      ENDCG
    }
    Fallback "Transparent/VertexLit"
	
}
