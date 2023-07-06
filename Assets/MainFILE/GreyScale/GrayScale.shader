Shader "Unlit/GrayScale"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Intensity ("BlindnessMagnitude", Range(0.0, 1.0)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

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
            float _Intensity;

            Interpolators vert (MeshData v)
            {
                Interpolators o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex); // controls tiling and offset

                return o;
            }

            float4 frag (Interpolators i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                float3 greyScale = float3(_Intensity, _Intensity, _Intensity);
                float grayValue = dot(col.rgb, greyScale);
                col = float4(grayValue, grayValue, grayValue, col.a);
                return col;
            }
            ENDCG
        }
    }
}
