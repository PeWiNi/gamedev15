Shader "Custom/Aim Shader" {
	Properties {
		_Color ("Color Tint", Color) = (1.0,1.0,1.0,1.0)
		_MinVisDistance ("Minimum viewable distance", float) = 30
		_MaxVisDistance ("Maximum viewable distance", float) = 35
	}
	SubShader {
        Tags {"Queue"="Transparent" "RenderType"="Opaque"}
		Pass {
			Blend SrcAlpha OneMinusSrcAlpha
			ZWrite Off

			CGPROGRAM
			#pragma vertex vert  
			#pragma fragment frag 
			
			uniform fixed4 _Color;
			uniform half _MaxVisDistance;
			uniform half _MinVisDistance;

			struct vertexInput {
				half4 vertex : POSITION;
			};
			struct vertexOutput {
				half4 pos : SV_POSITION;
                half3 vertPos : TEXCOORD0;
				fixed4 color : COLOR;
			};

			vertexOutput vert(vertexInput v) {
				vertexOutput o;

				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.vertPos = v.vertex.xyz;
				o.color = _Color;
				
				//Distance falloff
				half3 viewDirW = _WorldSpaceCameraPos - mul((half4x4)_Object2World, v.vertex);
				half viewDist = length(viewDirW);
				half falloff = saturate((viewDist - _MinVisDistance) / (_MaxVisDistance - _MinVisDistance));
				o.color.a *= (0.0f + falloff);

				return o;
			}

			float4 frag(vertexOutput i) : COLOR {
				return i.color;
			}
			ENDCG
		}
	}
	//Fallback "Unlit/Transparent"
}