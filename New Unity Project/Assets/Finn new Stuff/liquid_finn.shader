// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:True,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:34102,y:32536,varname:node_3138,prsc:2|emission-4998-OUT,clip-8888-OUT;n:type:ShaderForge.SFN_Tex2d,id:7491,x:32673,y:32873,varname:node_7491,prsc:2,tex:20ce8679a3bdfb540bbdc81ce108e2e1,ntxv:0,isnm:False|UVIN-6165-OUT,TEX-7172-TEX;n:type:ShaderForge.SFN_Time,id:2937,x:31312,y:32867,varname:node_2937,prsc:2;n:type:ShaderForge.SFN_Multiply,id:627,x:31675,y:33010,varname:node_627,prsc:2|A-2937-TSL,B-5117-OUT;n:type:ShaderForge.SFN_Vector1,id:5117,x:31675,y:33175,varname:node_5117,prsc:2,v1:-6;n:type:ShaderForge.SFN_Add,id:5720,x:32330,y:33349,varname:node_5720,prsc:2|A-627-OUT,B-1862-V,C-2535-OUT;n:type:ShaderForge.SFN_TexCoord,id:1862,x:31455,y:33411,varname:node_1862,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Append,id:6165,x:32621,y:33220,varname:node_6165,prsc:2|A-6137-OUT,B-5720-OUT;n:type:ShaderForge.SFN_Tex2d,id:6203,x:32578,y:33576,varname:_node_7491_copy,prsc:2,tex:20ce8679a3bdfb540bbdc81ce108e2e1,ntxv:0,isnm:False|TEX-7172-TEX;n:type:ShaderForge.SFN_Add,id:3721,x:33005,y:33458,varname:node_3721,prsc:2|A-7491-R,B-8672-R;n:type:ShaderForge.SFN_Tex2dAsset,id:7172,x:31829,y:32534,ptovrint:False,ptlb:node_7172,ptin:_node_7172,varname:_node_7172,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:20ce8679a3bdfb540bbdc81ce108e2e1,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Clamp01,id:362,x:33328,y:33270,varname:node_362,prsc:2|IN-8011-OUT;n:type:ShaderForge.SFN_RemapRange,id:8888,x:33603,y:33392,varname:node_8888,prsc:2,frmn:0.4,frmx:0.41,tomn:0,tomx:1|IN-362-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4786,x:31628,y:31839,ptovrint:False,ptlb:node_4786,ptin:_node_4786,varname:_node_4786,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:3;n:type:ShaderForge.SFN_Multiply,id:8251,x:31874,y:31679,varname:node_8251,prsc:2|A-2937-TSL,B-4786-OUT;n:type:ShaderForge.SFN_Add,id:1355,x:32165,y:31914,varname:node_1355,prsc:2|A-8251-OUT,B-9429-V;n:type:ShaderForge.SFN_TexCoord,id:9429,x:31421,y:31909,varname:node_9429,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Append,id:5384,x:32331,y:31810,varname:node_5384,prsc:2|A-9429-U,B-1355-OUT;n:type:ShaderForge.SFN_Tex2d,id:6311,x:32522,y:31716,varname:node_6311,prsc:2,tex:20ce8679a3bdfb540bbdc81ce108e2e1,ntxv:0,isnm:False|UVIN-5384-OUT,TEX-7172-TEX;n:type:ShaderForge.SFN_Multiply,id:2535,x:32840,y:31592,varname:node_2535,prsc:2|A-9559-OUT,B-2958-OUT;n:type:ShaderForge.SFN_RemapRange,id:2958,x:32861,y:31828,varname:node_2958,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-6311-G;n:type:ShaderForge.SFN_ValueProperty,id:9559,x:32622,y:31914,ptovrint:False,ptlb:node_9559,ptin:_node_9559,varname:_node_9559,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.08;n:type:ShaderForge.SFN_Add,id:1923,x:32924,y:33178,varname:node_1923,prsc:2|A-6165-OUT,B-2535-OUT;n:type:ShaderForge.SFN_ObjectPosition,id:3253,x:31341,y:35309,varname:node_3253,prsc:2;n:type:ShaderForge.SFN_Subtract,id:3647,x:31772,y:35344,varname:node_3647,prsc:2|A-3253-XYZ,B-4429-OUT;n:type:ShaderForge.SFN_Normalize,id:9986,x:32023,y:35381,varname:node_9986,prsc:2|IN-3647-OUT;n:type:ShaderForge.SFN_Vector3,id:4429,x:31536,y:35397,varname:node_4429,prsc:2,v1:1,v2:0,v3:0;n:type:ShaderForge.SFN_Subtract,id:6605,x:31741,y:35604,varname:node_6605,prsc:2|A-3253-XYZ,B-8045-OUT;n:type:ShaderForge.SFN_ViewPosition,id:47,x:31310,y:35681,varname:node_47,prsc:2;n:type:ShaderForge.SFN_Append,id:8045,x:31620,y:35735,varname:node_8045,prsc:2|A-47-X,B-8167-OUT,C-47-Z;n:type:ShaderForge.SFN_Vector1,id:8167,x:31364,y:35917,varname:node_8167,prsc:2,v1:0;n:type:ShaderForge.SFN_Normalize,id:2623,x:32009,y:35676,varname:node_2623,prsc:2|IN-6605-OUT;n:type:ShaderForge.SFN_Dot,id:1498,x:32384,y:35366,varname:node_1498,prsc:2,dt:0|A-9986-OUT,B-2623-OUT;n:type:ShaderForge.SFN_Cross,id:1848,x:32386,y:35658,varname:node_1848,prsc:2|A-9986-OUT,B-2623-OUT;n:type:ShaderForge.SFN_ArcTan2,id:2734,x:32620,y:35475,varname:node_2734,prsc:2,attp:2|A-1498-OUT,B-1848-OUT;n:type:ShaderForge.SFN_ComponentMask,id:1228,x:32809,y:35511,varname:node_1228,prsc:2,cc1:1,cc2:-1,cc3:-1,cc4:-1|IN-2734-OUT;n:type:ShaderForge.SFN_Add,id:6137,x:31372,y:34054,varname:node_6137,prsc:2|A-1862-U,B-4336-OUT;n:type:ShaderForge.SFN_Multiply,id:4336,x:32938,y:35311,varname:node_4336,prsc:2|A-4861-OUT,B-1228-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4861,x:32552,y:35183,ptovrint:False,ptlb:node_4861,ptin:_node_4861,varname:_node_4861,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:6;n:type:ShaderForge.SFN_Add,id:2162,x:32215,y:34291,varname:node_2162,prsc:2|A-2598-U,B-1683-OUT,C-2535-OUT;n:type:ShaderForge.SFN_TexCoord,id:2598,x:31758,y:34197,varname:node_2598,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Append,id:1381,x:32494,y:34032,varname:node_1381,prsc:2|A-6913-OUT,B-2162-OUT;n:type:ShaderForge.SFN_Add,id:8011,x:33362,y:33768,varname:node_8011,prsc:2|A-5687-OUT,B-6203-B;n:type:ShaderForge.SFN_Tex2d,id:8672,x:32839,y:34102,varname:node_8672,prsc:2,tex:20ce8679a3bdfb540bbdc81ce108e2e1,ntxv:0,isnm:False|UVIN-1381-OUT,TEX-7172-TEX;n:type:ShaderForge.SFN_ValueProperty,id:7935,x:31799,y:33955,ptovrint:False,ptlb:node_7935,ptin:_node_7935,varname:node_7935,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:-2;n:type:ShaderForge.SFN_Multiply,id:8466,x:32117,y:34058,varname:node_8466,prsc:2|A-2937-TSL,B-7935-OUT;n:type:ShaderForge.SFN_Add,id:6913,x:32100,y:34465,varname:node_6913,prsc:2|A-8466-OUT,B-2598-V,C-9876-OUT;n:type:ShaderForge.SFN_Negate,id:1683,x:33082,y:35118,varname:node_1683,prsc:2|IN-4336-OUT;n:type:ShaderForge.SFN_Tex2d,id:6955,x:33699,y:34414,ptovrint:False,ptlb:node_3820,ptin:_node_3820,varname:node_8672,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:20ce8679a3bdfb540bbdc81ce108e2e1,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Vector1,id:9876,x:31697,y:34625,varname:node_9876,prsc:2,v1:0.5;n:type:ShaderForge.SFN_TexCoord,id:1521,x:31730,y:34809,varname:node_1521,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_RemapRange,id:4731,x:32071,y:34844,varname:node_4731,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-1521-U;n:type:ShaderForge.SFN_Abs,id:8104,x:32359,y:34863,varname:node_8104,prsc:2|IN-4731-OUT;n:type:ShaderForge.SFN_RemapRange,id:7640,x:32623,y:34871,varname:node_7640,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-8104-OUT;n:type:ShaderForge.SFN_RemapRange,id:4850,x:32807,y:34861,varname:node_4850,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-7640-OUT;n:type:ShaderForge.SFN_Clamp01,id:2775,x:33000,y:34878,varname:node_2775,prsc:2|IN-4850-OUT;n:type:ShaderForge.SFN_OneMinus,id:3892,x:33170,y:34903,varname:node_3892,prsc:2|IN-2775-OUT;n:type:ShaderForge.SFN_Multiply,id:5687,x:33329,y:34035,varname:node_5687,prsc:2|A-3721-OUT,B-3892-OUT;n:type:ShaderForge.SFN_Color,id:2460,x:33572,y:32510,ptovrint:False,ptlb:node_2460,ptin:_node_2460,varname:node_2460,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.9224138,c3:0.6691177,c4:1;n:type:ShaderForge.SFN_Color,id:5188,x:33608,y:32756,ptovrint:False,ptlb:node_5188,ptin:_node_5188,varname:node_5188,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.9338235,c2:0.4051146,c3:0.4051146,c4:1;n:type:ShaderForge.SFN_Lerp,id:4998,x:33814,y:32644,varname:node_4998,prsc:2|A-2460-RGB,B-5188-RGB,T-8046-OUT;n:type:ShaderForge.SFN_Add,id:8046,x:33097,y:32795,varname:node_8046,prsc:2|A-7491-R,B-6203-B;proporder:7172-4786-9559-4861-7935-2460-5188;pass:END;sub:END;*/

Shader "Shader Forge/liquid_finn" {
    Properties {
        _node_7172 ("node_7172", 2D) = "white" {}
        _node_4786 ("node_4786", Float ) = 3
        _node_9559 ("node_9559", Float ) = 0.08
        _node_4861 ("node_4861", Float ) = 6
        _node_7935 ("node_7935", Float ) = -2
        _node_2460 ("node_2460", Color) = (1,0.9224138,0.6691177,1)
        _node_5188 ("node_5188", Color) = (0.9338235,0.4051146,0.4051146,1)
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
            "CanUseSpriteAtlas"="True"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _node_7172; uniform float4 _node_7172_ST;
            uniform float _node_4786;
            uniform float _node_9559;
            uniform float _node_4861;
            uniform float _node_7935;
            uniform float4 _node_2460;
            uniform float4 _node_5188;
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
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                float3 node_9986 = normalize((objPos.rgb-float3(1,0,0)));
                float3 node_2623 = normalize((objPos.rgb-float3(_WorldSpaceCameraPos.r,0.0,_WorldSpaceCameraPos.b)));
                float node_4336 = (_node_4861*((atan2(dot(node_9986,node_2623),cross(node_9986,node_2623))/6.28318530718)+0.5).g);
                float4 node_2937 = _Time;
                float2 node_5384 = float2(i.uv0.r,((node_2937.r*_node_4786)+i.uv0.g));
                float4 node_6311 = tex2D(_node_7172,TRANSFORM_TEX(node_5384, _node_7172));
                float node_2535 = (_node_9559*(node_6311.g*2.0+-1.0));
                float2 node_6165 = float2((i.uv0.r+node_4336),((node_2937.r*(-6.0))+i.uv0.g+node_2535));
                float4 node_7491 = tex2D(_node_7172,TRANSFORM_TEX(node_6165, _node_7172));
                float2 node_1381 = float2(((node_2937.r*_node_7935)+i.uv0.g+0.5),(i.uv0.r+(-1*node_4336)+node_2535));
                float4 node_8672 = tex2D(_node_7172,TRANSFORM_TEX(node_1381, _node_7172));
                float4 _node_7491_copy = tex2D(_node_7172,TRANSFORM_TEX(i.uv0, _node_7172));
                clip((saturate((((node_7491.r+node_8672.r)*(1.0 - saturate(((abs((i.uv0.r*2.0+-1.0))*2.0+-1.0)*2.0+-1.0))))+_node_7491_copy.b))*100.0001+-40.00004) - 0.5);
////// Lighting:
////// Emissive:
                float3 emissive = lerp(_node_2460.rgb,_node_5188.rgb,(node_7491.r+_node_7491_copy.b));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Back
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _node_7172; uniform float4 _node_7172_ST;
            uniform float _node_4786;
            uniform float _node_9559;
            uniform float _node_4861;
            uniform float _node_7935;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                float3 node_9986 = normalize((objPos.rgb-float3(1,0,0)));
                float3 node_2623 = normalize((objPos.rgb-float3(_WorldSpaceCameraPos.r,0.0,_WorldSpaceCameraPos.b)));
                float node_4336 = (_node_4861*((atan2(dot(node_9986,node_2623),cross(node_9986,node_2623))/6.28318530718)+0.5).g);
                float4 node_2937 = _Time;
                float2 node_5384 = float2(i.uv0.r,((node_2937.r*_node_4786)+i.uv0.g));
                float4 node_6311 = tex2D(_node_7172,TRANSFORM_TEX(node_5384, _node_7172));
                float node_2535 = (_node_9559*(node_6311.g*2.0+-1.0));
                float2 node_6165 = float2((i.uv0.r+node_4336),((node_2937.r*(-6.0))+i.uv0.g+node_2535));
                float4 node_7491 = tex2D(_node_7172,TRANSFORM_TEX(node_6165, _node_7172));
                float2 node_1381 = float2(((node_2937.r*_node_7935)+i.uv0.g+0.5),(i.uv0.r+(-1*node_4336)+node_2535));
                float4 node_8672 = tex2D(_node_7172,TRANSFORM_TEX(node_1381, _node_7172));
                float4 _node_7491_copy = tex2D(_node_7172,TRANSFORM_TEX(i.uv0, _node_7172));
                clip((saturate((((node_7491.r+node_8672.r)*(1.0 - saturate(((abs((i.uv0.r*2.0+-1.0))*2.0+-1.0)*2.0+-1.0))))+_node_7491_copy.b))*100.0001+-40.00004) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
