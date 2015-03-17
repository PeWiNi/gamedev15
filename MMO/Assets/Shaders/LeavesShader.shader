﻿Shader "Custom/Leaves" {
	Properties {
		_Color ("Color tint", Color) = (1.0, 1.0, 1.0, 1.0)
		_MainTex ("RGBA Texture Image", 2D) = "white" {}
		_Cutoff ("Alpha Cutoff", Range(0.0, 1.0)) = 0.2
	}
	SubShader {
		Pass {
			Cull Off //draw all faces
			CGPROGRAM

			//pragmas
			#pragma vertex vert  
			#pragma fragment frag 
			
			//user defined variables
			uniform fixed4 _Color;
			uniform sampler2D _MainTex;    
			uniform fixed _Cutoff;
 
			//base input structs
			struct vertexInput {
				fixed4 vertex : POSITION;
				fixed4 texcoord : TEXCOORD0;
			};
			struct vertexOutput {
				fixed4 pos : SV_POSITION;
				fixed4 tex : TEXCOORD0;
			};
 
			//vertex function
			vertexOutput vert(vertexInput input) {
				vertexOutput output;
 
				output.tex = input.texcoord;
				output.pos = mul(UNITY_MATRIX_MVP, input.vertex);

				return output;
			}
 
			//fragment function
			fixed4 frag(vertexOutput input) : COLOR {
				fixed4 textureColor = tex2D(_MainTex, input.tex.xy);  
				if (textureColor.a < _Cutoff) {
				   discard;
				}
				return fixed4(textureColor.xyz * _Color.xyz, textureColor.a);
			}
 
			ENDCG
		}
	}
	FallBack "Diffuse"
}