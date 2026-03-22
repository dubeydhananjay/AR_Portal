Shader "Custom/PortalWindow" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        [Enum(Equal, 3, NotEqual, 6)] _StencilTest ("Stencil Test", int) = 6
    }
    SubShader {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }

        Stencil {
            Ref 1
            Comp [_StencilTest]
        }

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        sampler2D _MainTex;
        struct Input { float2 uv_MainTex; };
        fixed4 _Color;

        void surf (Input IN, inout SurfaceOutputStandard o) {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}