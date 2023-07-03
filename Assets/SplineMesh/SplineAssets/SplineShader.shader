Shader "Unlit/SplineMaterial"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Fill ("Fill", Range(0.0, 1.0)) = 1.0 
        _Empty ("Empty", Range(0.0, 1.0)) = 0.0 
        _Color("Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }

        Pass
        {
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            #define TAU 6.283185

            struct MeshData
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Interpolators
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float _Fill, _Empty;
            float4 _Color;

            Interpolators vert (MeshData v)
            {
                Interpolators o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                return o;
            }

            float4 frag (Interpolators i) : SV_Target
            {
                
                float4 col = _Color;
                float mask = step(-_Fill,-i.uv.y);
                // float maskEmpty = step(-_Empty,-i.uv.y)
                mask -= step(-_Empty,-i.uv.y);
                // clip(-maskFill);
                return col*mask;
            }
            ENDCG
        }
    }
}
