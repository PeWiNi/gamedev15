Shader "Custom/Aim Shader" {
	Properties {
		_Color ("Color Tint", Color) = (1.0,1.0,1.0,1.0)
		_Transparency ("Transparency", Range(0.0, 1.0)) = 0.5
	}
	SubShader {
        Tags {"Queue"="Transparent" "RenderType"="Opaque"}
		Pass {
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert  
			#pragma fragment frag 
			
			uniform fixed4 _Color;
			uniform fixed _Transparency;

			struct vertexInput {
				fixed4 vertex : POSITION;
			};
			struct vertexOutput {
				fixed4 pos : SV_POSITION;
                half3 vertPos : TEXCOORD0;
			};

			vertexOutput vert(vertexInput v) {
				vertexOutput o;

				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.vertPos = v.vertex.xyz;
				return o;
			}

			float4 frag(vertexOutput i) : COLOR {
				fixed trans = lerp(_Transparency * 0.1, _Transparency * 0.5, abs(i.vertPos.x + 1));
				return fixed4(_Color.xyz, _Color.a * trans);
			}
			ENDCG
		}
	}
	//Fallback "Unlit/Transparent"
}