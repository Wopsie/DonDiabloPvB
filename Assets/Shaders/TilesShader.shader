// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.36 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.36;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|emission-7731-OUT;n:type:ShaderForge.SFN_Tex2d,id:6210,x:32112,y:32520,ptovrint:False,ptlb:node_1050_copy,ptin:_node_1050_copy,varname:_node_1050_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:df1fad9f2935f3278662bc35c18d320c,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:2044,x:32112,y:32318,ptovrint:False,ptlb:node_1050_copy_copy,ptin:_node_1050_copy_copy,varname:_node_1050_copy_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:4f0f92e366cd343357bfaaa2083600a2,ntxv:0,isnm:False|MIP-7863-OUT;n:type:ShaderForge.SFN_Blend,id:7731,x:32521,y:32689,varname:node_7731,prsc:2,blmd:10,clmp:True|SRC-2911-OUT,DST-6210-RGB;n:type:ShaderForge.SFN_Color,id:1105,x:31778,y:32431,ptovrint:False,ptlb:node_1105,ptin:_node_1105,varname:node_1105,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Vector1,id:7863,x:31791,y:32643,varname:node_7863,prsc:2,v1:1;n:type:ShaderForge.SFN_Blend,id:2911,x:32316,y:32398,varname:node_2911,prsc:2,blmd:10,clmp:True|SRC-2044-RGB,DST-1105-RGB;proporder:6210-2044-1105;pass:END;sub:END;*/

Shader "Shader Forge/TilesShader" {
    Properties {
        _node_1050_copy ("node_1050_copy", 2D) = "white" {}
        _node_1050_copy_copy ("node_1050_copy_copy", 2D) = "white" {}
        _node_1105 ("node_1105", Color) = (1,1,1,1)
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _node_1050_copy; uniform float4 _node_1050_copy_ST;
            uniform sampler2D _node_1050_copy_copy; uniform float4 _node_1050_copy_copy_ST;
            uniform float4 _node_1105;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos(v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 _node_1050_copy_copy_var = tex2Dlod(_node_1050_copy_copy,float4(TRANSFORM_TEX(i.uv0, _node_1050_copy_copy),0.0,1.0));
                float4 _node_1050_copy_var = tex2D(_node_1050_copy,TRANSFORM_TEX(i.uv0, _node_1050_copy));
                float3 emissive = saturate(( _node_1050_copy_var.rgb > 0.5 ? (1.0-(1.0-2.0*(_node_1050_copy_var.rgb-0.5))*(1.0-saturate(( _node_1105.rgb > 0.5 ? (1.0-(1.0-2.0*(_node_1105.rgb-0.5))*(1.0-_node_1050_copy_copy_var.rgb)) : (2.0*_node_1105.rgb*_node_1050_copy_copy_var.rgb) )))) : (2.0*_node_1050_copy_var.rgb*saturate(( _node_1105.rgb > 0.5 ? (1.0-(1.0-2.0*(_node_1105.rgb-0.5))*(1.0-_node_1050_copy_copy_var.rgb)) : (2.0*_node_1105.rgb*_node_1050_copy_copy_var.rgb) ))) ));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
