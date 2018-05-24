// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.36 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.36;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:32719,y:32712,varname:node_4013,prsc:2|emission-6885-OUT,alpha-7219-OUT;n:type:ShaderForge.SFN_Tex2d,id:4306,x:32188,y:33087,ptovrint:False,ptlb:node_4306,ptin:_node_4306,varname:node_4306,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:a7e5cc1153717224597b3e4b35ee26ac,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Add,id:9629,x:32397,y:33070,varname:node_9629,prsc:2|A-4306-R,B-4306-G,C-4306-B;n:type:ShaderForge.SFN_Tex2d,id:3475,x:32298,y:32717,ptovrint:False,ptlb:node_4306_copy,ptin:_node_4306_copy,varname:_node_4306_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:a7e5cc1153717224597b3e4b35ee26ac,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Color,id:9832,x:32285,y:32898,ptovrint:False,ptlb:Color_copy,ptin:_Color_copy,varname:_Color_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.8958418,c2:0.4191176,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:6885,x:32521,y:32812,varname:node_6885,prsc:2|A-3475-RGB,B-9832-RGB;n:type:ShaderForge.SFN_Multiply,id:7219,x:32554,y:33144,varname:node_7219,prsc:2|A-9629-OUT,B-8877-OUT;n:type:ShaderForge.SFN_Slider,id:8877,x:32251,y:33270,ptovrint:False,ptlb:node_8877,ptin:_node_8877,varname:node_8877,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.506682,max:1;proporder:4306-3475-9832-8877;pass:END;sub:END;*/

Shader "Shader Forge/GroundTailsMinus" {
    Properties {
        _node_4306 ("node_4306", 2D) = "white" {}
        _node_4306_copy ("node_4306_copy", 2D) = "white" {}
        _Color_copy ("Color_copy", Color) = (0.8958418,0.4191176,1,1)
        _node_8877 ("node_8877", Range(0, 1)) = 0.506682
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
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _node_4306; uniform float4 _node_4306_ST;
            uniform sampler2D _node_4306_copy; uniform float4 _node_4306_copy_ST;
            uniform float4 _Color_copy;
            uniform float _node_8877;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 _node_4306_copy_var = tex2D(_node_4306_copy,TRANSFORM_TEX(i.uv0, _node_4306_copy));
                float3 emissive = (_node_4306_copy_var.rgb*_Color_copy.rgb);
                float3 finalColor = emissive;
                float4 _node_4306_var = tex2D(_node_4306,TRANSFORM_TEX(i.uv0, _node_4306));
                fixed4 finalRGBA = fixed4(finalColor,((_node_4306_var.r+_node_4306_var.g+_node_4306_var.b)*_node_8877));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
