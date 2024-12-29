Shader "Custom/StandardWithOutline"
{
    Properties
    {
        _Color("Main Color", Color) = (1, 1, 1, 1)
        _MainTex("Main Texture", 2D) = "white" {}
        _NormalMap("Normal Map", 2D) = "bump" {} // ����� ��������
        _Glossiness("Smoothness", Range(0, 1)) = 0.5
        _Metallic("Metallic", Range(0, 1)) = 0.0
        _OutlineColor("Outline Color", Color) = (0, 0, 0, 1) // ���� �������
        _OutlineWidth("Outline Width", Float) = 0.03 // ������� �������
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" }

        // Pass ��� �������
        Pass
        {
            Name "Outline"
            Tags { "LightMode" = "Always" }
            Cull Front
            ZWrite On
            ZTest LEqual
            ColorMask RGB

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            float _OutlineWidth;
            float4 _OutlineColor;

            v2f vert(appdata v)
            {
                v2f o;
                // ��������� ������ ����� ��� ��������
                float3 offset = v.normal * _OutlineWidth;
                o.pos = UnityObjectToClipPos(v.vertex + float4(offset, 0));
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                return _OutlineColor; // ������ ������� �������� ������
            }
            ENDCG
        }

        // �������� Pass ��� ������������ ��������� � ���������� ����� ��������
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_NormalMap; // ��������� UV-���������� ��� ����� ��������
        };

        sampler2D _MainTex;
        sampler2D _NormalMap; // ������� ��� ����� ��������
        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            // �������� ����
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;

            // ��������� ����� ��������
            float3 normalMap = tex2D(_NormalMap, IN.uv_NormalMap).rgb;
            normalMap = normalMap * 2.0 - 1.0; // �������������� � �������� [-1, 1]
            o.Normal = normalize(normalMap);   // ���������� ����� ��������
        }
        ENDCG
    }

    FallBack "Diffuse"
}
