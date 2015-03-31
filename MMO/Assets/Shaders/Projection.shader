Shader "Custom/Projection Shader" {
	Properties {
		_ShadowTex ("Projected Image", 2D) = "white" {}  
	}
	SubShader {
		Pass {
            // add color of _ShadowTex to the color in the framebuffer 
			ZWrite Off // don't change depths
			Offset -1, -1 // avoid depth fighting
			Blend SrcAlpha OneMinusSrcAlpha // Alpha blending
 
			CGPROGRAM
 
			#pragma vertex vert  
			#pragma fragment frag 
 
			// User-specified properties
			uniform sampler2D _ShadowTex; 
 
			// Projector-specific uniforms
			uniform float4x4 _Projector; // transformation matrix 
				// from object space to projector space 
 
			struct vertexInput {
				fixed4 vertex : POSITION;
			};
			struct vertexOutput {
				fixed4 pos : SV_POSITION;
				fixed4 posProj : TEXCOORD0;
				// position in projector space
			};
 
			vertexOutput vert(vertexInput v) {
				vertexOutput o;
 
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.posProj = mul(_Projector, v.vertex);
				return o;
			}
 
			float4 frag(vertexOutput i) : COLOR {
				if (i.posProj.w > 0.0) { // in front of projector?
					fixed4 tex = tex2D(
						//_ShadowTex , i.posProj.xy / i.posProj.w); 
						_ShadowTex, i.posProj);
					return fixed4(tex.xyz, normalize(tex.a));
				} else // behind projector
               return fixed4(0.0, 0.0, 0.0, 0.0);
			}
			ENDCG
		}
	} 
	// Fallback "Projector/Light"
}