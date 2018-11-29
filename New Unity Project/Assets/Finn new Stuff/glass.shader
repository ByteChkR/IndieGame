// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:33036,y:32811,varname:node_4795,prsc:2|emission-6083-OUT,alpha-9168-OUT;n:type:ShaderForge.SFN_Color,id:3568,x:32028,y:32645,ptovrint:False,ptlb:node_3568,ptin:_node_3568,varname:node_3568,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.8014706,c2:0.8014706,c3:0.8014706,c4:1;n:type:ShaderForge.SFN_Color,id:7866,x:32016,y:32837,ptovrint:False,ptlb:node_7866,ptin:_node_7866,varname:node_7866,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.751724,c3:1,c4:1;n:type:ShaderForge.SFN_Lerp,id:9077,x:32423,y:32755,varname:node_9077,prsc:2|A-3568-RGB,B-7866-RGB,T-9168-OUT;n:type:ShaderForge.SFN_Blend,id:6083,x:32702,y:32878,varname:node_6083,prsc:2,blmd:5,clmp:True|SRC-562-OUT,DST-9077-OUT;n:type:ShaderForge.SFN_Fresnel,id:9168,x:32188,y:33181,varname:node_9168,prsc:2|EXP-3755-OUT;n:type:ShaderForge.SFN_Vector1,id:3755,x:31907,y:33251,varname:node_3755,prsc:2,v1:1;n:type:ShaderForge.SFN_Cubemap,id:8035,x:32144,y:33025,ptovrint:False,ptlb:node_8035,ptin:_node_8035,varname:node_8035,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,cube:5c913975591e41299de2034d31ae279f,pvfc:0;n:type:ShaderForge.SFN_Multiply,id:562,x:32433,y:32973,varname:node_562,prsc:2|A-8035-RGB,B-5585-OUT;n:type:ShaderForge.SFN_Vector1,id:5585,x:32385,y:33215,varname:node_5585,prsc:2,v1:1;proporder:3568-7866-8035;pass:END;sub:END;*/

Shader "Shader Forge/glass" {
    Properties {
        _node_3568 ("node_3568", Color) = (0.8014706,0.8014706,0.8014706,1)
        _node_7866 ("node_7866", Color) = (0,0.751724,1,1)
        _node_8035 ("node_8035", Cube) = "_Skybox" {}
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
            uniform float4 _node_3568;
            uniform float4 _node_7866;
            uniform samplerCUBE _node_8035;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                UNITY_FOG_COORDS(2)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
////// Lighting:
////// Emissive:
                float node_9168 = pow(1.0-max(0,dot(normalDirection, viewDirection)),1.0);
                float3 emissive = saturate(max((texCUBE(_node_8035,viewReflectDirection).rgb*1.0),lerp(_node_3568.rgb,_node_7866.rgb,node_9168)));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,node_9168);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
