Shader "Custom/SpriteGradient" {
	Properties {
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Left Color", Color) = (0, 0, 0, 0)
		_Color2 ("Right Color", Color) = (1, 1, 1, 1)
	}
	SubShader {
		Tags {"Queue"="Background"  "IgnoreProjector"="True"}
		LOD 100
		
		ZWrite On
		
		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			
			uniform fixed4 _Color;
			uniform fixed4 _Color2;
			uniform sampler2D _MainTex;    
			uniform float4 _MainTex_ST;
			uniform fixed _isStep;
			
			struct vertexInput {
				fixed4 vertex : POSITION;
				fixed4 texcoord : TEXCOORD0;
			};
			struct vertexOutput {
				fixed4 pos : SV_POSITION;
				fixed4 tex : TEXCOORD0;
				fixed4 col : COLOR;
			};

			vertexOutput vert(vertexInput v) {
				vertexOutput o;

				o.tex = v.texcoord;
				o.pos = mul (UNITY_MATRIX_MVP, v.vertex);

				fixed determine = step(0, lerp(-1, 1, v.texcoord.x));
				if(determine == 0)
					o.col = _Color;
				else
					o.col = _Color2;
				return o;
			}
			
			float4 frag (vertexOutput i) : COLOR {
				fixed4 tex = tex2D(_MainTex, i.tex.xy * _MainTex_ST.xy + _MainTex_ST.zw);
				float4 c = i.col;
				if (tex.a < .1f) {
				   discard;
				}
				return fixed4(tex.xyz * c.xyz, tex.a);
			}
			ENDCG
         }
	}
}