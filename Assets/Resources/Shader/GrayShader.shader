Shader "Custom/GrayscaleEffect"
{
    Properties
    {
        _MainTex("Base Texture", 2D) = "white" {}
        _Grayscale("Grayscale Intensity", Range(0, 1)) = 1.0
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" "IgnoreProjector"="True" "PreviewType"="Plane" "CanUseSpriteAtlas"="True" }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float _Grayscale;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                // �ؽ�ó���� ���� ���� �����ɴϴ�.
                half4 color = tex2D(_MainTex, i.uv);

                // ������ ������� ��ȯ (Luminance formula: 0.299 R + 0.587 G + 0.114 B)
                half grayscale = dot(color.rgb, half3(0.299, 0.587, 0.114));

                // _Grayscale ���� ����� ��� ���� ����
                return lerp(color, half4(grayscale, grayscale, grayscale, color.a), _Grayscale);
            }
            ENDCG
        }
    }
}
