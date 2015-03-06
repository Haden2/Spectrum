// created by Marc D'Amico
// updated dec 2014

Shader "DirtyLens/2.0/DirtyLens_02_Particle" {
    Properties {
        _Flare ("Flare", 2D) = "white" {}
        _FlareMultiplier ("Flare Multiplier", Float ) = 5
        _FlarePower ("Flare Power", Float ) = 1
        [MaterialToggle] _ClampFlareColour ("Clamp Flare Colour", Float ) = 0
        _DirtTexture ("Dirt Texture", 2D) = "white" {}
        _DirtBrightness ("Dirt Brightness", Float ) = 1
        [MaterialToggle] _UseScreenMask ("UseScreenMask", Float ) = 0.827451
        _ScreenMask ("Screen Mask", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            ZWrite Off
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform sampler2D _Flare; uniform float4 _Flare_ST;
            uniform sampler2D _DirtTexture; uniform float4 _DirtTexture_ST;
            uniform sampler2D _ScreenMask; uniform float4 _ScreenMask_ST;
            uniform float _FlareMultiplier;
            uniform float _FlarePower;
            uniform fixed _UseScreenMask;
            uniform fixed _ClampFlareColour;
            uniform float _DirtBrightness;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 screenPos : TEXCOORD1;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.screenPos = o.pos;
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5;
////// Lighting:
////// Emissive:
                float2 node_38 = i.uv0;
                float3 node_83 = pow((i.vertexColor.rgb*(_FlareMultiplier*tex2D(_Flare,TRANSFORM_TEX(node_38.rg, _Flare)).rgb)),_FlarePower);
                float2 node_45 = sceneUVs;
                float3 emissive = (lerp( node_83, saturate(node_83), _ClampFlareColour )*((_DirtBrightness*tex2D(_DirtTexture,TRANSFORM_TEX(node_45.rg, _DirtTexture)).rgb)*lerp( 1.0, tex2D(_ScreenMask,TRANSFORM_TEX(node_45.rg, _ScreenMask)).rgb, _UseScreenMask )));
                float3 finalColor = emissive;
/// Final Color:
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
