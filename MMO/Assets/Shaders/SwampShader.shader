Shader "Custom/Swamp" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_TransTex ("Transperency texture (Alpha)", 2D) = "white" {}
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
 
			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			uniform sampler2D _TransTex;
 
			struct vertexInput {
				float4 vertex : POSITION;
				float4 texcoord : TEXCOORD0;
			};
			struct vertexOutput {
				float4 pos : SV_POSITION;
				float4 tex : TEXCOORD0;
			};
 
			vertexOutput vert(vertexInput v) {
				vertexOutput o;
 
				o.tex = v.texcoord;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				return o;
			}
 
			float4 frag(vertexOutput i) : COLOR {
				fixed4 tex = tex2D(_MainTex, i.tex.xy * _MainTex_ST.xy + _MainTex_ST.zw);
				fixed4 texT = tex2D(_TransTex, i.tex.xy * _MainTex_ST.xy + _MainTex_ST.zw);

				return fixed4(tex.xyz, texT.a);
			}
			ENDCG
		}
	}
	Fallback "Unlit/Transparent"
}