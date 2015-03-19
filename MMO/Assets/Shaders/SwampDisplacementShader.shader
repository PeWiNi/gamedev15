Shader "Custom/Swamp Displacement" {
        Properties {
            _MainTex ("Base (RGB)", 2D) = "white" {}
            _DispTex ("Disp Texture", 2D) = "gray" {}
            _NormalMap ("Normalmap", 2D) = "bump" {}
            _Displacement ("Displacement", Range(0, 1.0)) = 0.3
			_Transparency ("transparency", Range(0.0, 1.0)) = 0.5
        }
        SubShader {
			Cull Off
            Tags { "Queue"="Transparent" "RenderType"="Opaque" }
            LOD 300
            
            CGPROGRAM
            #pragma surface surf BlinnPhong addshadow fullforwardshadows vertex:disp nolightmap alpha
            #pragma target 5.0

            struct appdata {
                float4 vertex : POSITION;
                float4 tangent : TANGENT;
                float3 normal : NORMAL;
                float2 texcoord : TEXCOORD0;
            };

            sampler2D _DispTex;
            float _Displacement;

            void disp (inout appdata v) {
                float d = tex2Dlod(_DispTex, float4(v.texcoord.xy,0,0)).r * _Displacement;
                v.vertex.xyz += v.normal * d;
            }

            struct Input {
                float2 uv_MainTex;
            };

            sampler2D _MainTex;
            sampler2D _NormalMap;
			fixed _Transparency;

            void surf (Input IN, inout SurfaceOutput o) {
                half4 c = tex2D (_MainTex, IN.uv_MainTex);
                o.Albedo = c.rgb;
                o.Specular = 0.2;
                o.Gloss = 1.0;
				o.Alpha = (c.a * _Transparency) + _Transparency;
                o.Normal = UnpackNormal(tex2D(_NormalMap, IN.uv_MainTex));
            }
            ENDCG
        }
        FallBack "Unlit/Transparent"
    }