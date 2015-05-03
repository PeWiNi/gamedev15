Shader "Custom/Swamp Shader" {
	Properties {
		_Color ("Color tint", Color) = (1.0, 1.0, 1.0, 1.0)
		_MainTex ("Texture", 2D) = "white" {}
		_Transparency ("Transparency", Range(0.0, 1.0)) = 0.5
	}
	SubShader {
		Tags {"Queue" = "Transparent"} 
		Pass {	
			Cull Back //Only render front
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert  
			#pragma fragment frag 
 
 			uniform fixed4 _Color;
			uniform sampler2D _MainTex;
			uniform fixed4 _MainTex_ST;
			uniform fixed _Transparency;
 
			struct vertexInput {
				fixed4 vertex : POSITION;
				fixed4 texcoord : TEXCOORD0;
			};
			struct vertexOutput {
				fixed4 pos : SV_POSITION;
				fixed4 tex : TEXCOORD0;
			};
 
			vertexOutput vert(vertexInput v) {
				vertexOutput o;
 
				o.tex = v.texcoord;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				return o;
			}
 
			float4 frag(vertexOutput i) : COLOR {
				fixed4 tex = tex2D(_MainTex, i.tex.xy * _MainTex_ST.xy + _MainTex_ST.zw);
				fixed trans = (tex.a * _Transparency) + _Transparency;
				return fixed4(tex.xyz * _Color.xyz, trans);
			}
			ENDCG
		}
	}
	Fallback "Unlit/Transparent"
}