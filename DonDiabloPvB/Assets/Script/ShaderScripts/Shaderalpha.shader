// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.36 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.36;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:33146,y:32342,varname:node_4013,prsc:2|emission-4614-OUT,alpha-817-OUT;n:type:ShaderForge.SFN_Tex2d,id:975,x:32248,y:32407,ptovrint:False,ptlb:node_975,ptin:_node_975,varname:node_975,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:d35cbde6c83a8d86f99156e7c36bd61b,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Lerp,id:4614,x:32551,y:32568,varname:node_4614,prsc:2|A-975-RGB,B-7982-RGB,T-8344-OUT;n:type:ShaderForge.SFN_Multiply,id:8344,x:32157,y:32914,varname:node_8344,prsc:2|A-2602-OUT,B-525-OUT;n:type:ShaderForge.SFN_OneMinus,id:2602,x:32209,y:33121,varname:node_2602,prsc:2|IN-4227-OUT;n:type:ShaderForge.SFN_Slider,id:525,x:32363,y:33223,ptovrint:False,ptlb:node_525,ptin:_node_525,varname:node_525,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.991453,max:1;n:type:ShaderForge.SFN_Fresnel,id:4227,x:32420,y:32996,varname:node_4227,prsc:2|NRM-4961-OUT,EXP-8314-OUT;n:type:ShaderForge.SFN_Vector1,id:8314,x:32420,y:32874,varname:node_8314,prsc:2,v1:1;n:type:ShaderForge.SFN_NormalVector,id:4961,x:32593,y:32765,prsc:2,pt:True;n:type:ShaderForge.SFN_Tex2d,id:7982,x:32142,y:32659,varname:_node_975_copy,prsc:2,tex:d35cbde6c83a8d86f99156e7c36bd61b,ntxv:2,isnm:False|TEX-7802-TEX;n:type:ShaderForge.SFN_Tex2d,id:66,x:32899,y:32741,ptovrint:False,ptlb:node_66,ptin:_node_66,varname:node_66,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:1be6eeb5cc0f0b5498e51927c9725aec,ntxv:0,isnm:False|MIP-337-OUT;n:type:ShaderForge.SFN_ValueProperty,id:337,x:32895,y:33074,ptovrint:False,ptlb:node_337,ptin:_node_337,varname:node_337,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Tex2dAsset,id:7802,x:31872,y:32763,ptovrint:False,ptlb:node_7802,ptin:_node_7802,varname:node_7802,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:d35cbde6c83a8d86f99156e7c36bd61b,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:817,x:33124,y:32828,varname:node_817,prsc:2|A-66-G,B-66-R;proporder:975-525-66-337-7802;pass:END;sub:END;*/

Shader "Shader Forge/Shaderalpha" {
    Properties {
        _node_975 ("node_975", 2D) = "black" {}
        _node_525 ("node_525", Range(0, 1)) = 0.991453
        _node_66 ("node_66", 2D) = "white" {}
        _node_337 ("node_337", Float ) = 0
        _node_7802 ("node_7802", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
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
            uniform sampler2D _node_975; uniform float4 _node_975_ST;
            uniform float _node_525;
            uniform sampler2D _node_66; uniform float4 _node_66_ST;
            uniform float _node_337;
            uniform sampler2D _node_7802; uniform float4 _node_7802_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float4 _node_975_var = tex2D(_node_975,TRANSFORM_TEX(i.uv0, _node_975));
                float4 _node_975_copy = tex2D(_node_7802,TRANSFORM_TEX(i.uv0, _node_7802));
                float3 emissive = lerp(_node_975_var.rgb,_node_975_copy.rgb,((1.0 - pow(1.0-max(0,dot(normalDirection, viewDirection)),1.0))*_node_525));
                float3 finalColor = emissive;
                float4 _node_66_var = tex2Dlod(_node_66,float4(TRANSFORM_TEX(i.uv0, _node_66),0.0,_node_337));
                return fixed4(finalColor,(_node_66_var.g*_node_66_var.r));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
