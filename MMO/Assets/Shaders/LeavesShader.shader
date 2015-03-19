Shader "Custom/Leaves" {
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
			uniform float4 _MainTex_ST;
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
			vertexOutput vert(vertexInput v) {
				vertexOutput o;
 
				o.tex = v.texcoord;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);

				return o;
			}
 
			//fragment function
			fixed4 frag(vertexOutput i) : COLOR {
				fixed4 tex = tex2D(_MainTex, i.tex.xy * _MainTex_ST.xy + _MainTex_ST.zw);
				if (tex.a < _Cutoff) {
				   discard;
				}
				return fixed4(tex.xyz * _Color.xyz, tex.a);
			}
 
			ENDCG
		}
	}
	FallBack "Diffuse"
}
