Shader "Custom/Ice Shader" {
	Properties {
		_Color ("Color Tint", Color) = (1.0,1.0,1.0,1.0)
        _MainTex ("Base (RGB)", 2D) = "white" {}
		_BumpMap ("Normal Texture", 2D) = "bump" {}
		_BumpDepth ("Bump Depth", Range(0.0,1.0)) = 1
		_SpecColor ("Specular Color", Color) = (1.0,1.0,1.0,1.0)
		_Shininess ("Shininess", float) = 10
	}
	SubShader {
		Pass {
			Tags { "LightMode" = "ForwardBase" }
			CGPROGRAM
			//pragmas
			#pragma vertex vert
			#pragma fragment frag
			
			//user defined variables
			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			uniform sampler2D _BumpMap;
			uniform float _BumpDepth;
			uniform float4 _Color;
			uniform float4 _SpecColor;
			uniform float _Shininess;

			//unity defined variables
			uniform float4 _LightColor0;

			//base input structs
			struct vertexInput {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 texcoord : TEXCOORD0;
				float4 tangent : TANGENT;
			};
			struct vertexOutput {
				float4 pos : SV_POSITION;
				float4 tex : TEXCOORD0;
				float4 lightDirection : TEXCOORD1;
				float3 viewDirection : TEXCOORD2;
				float3 normalWorld : TEXCOORD3;
				float3 tangentWorld : TEXCOORD4;
				float3 binormalWorld : TEXCOORD5;
			};
			
			//vertex function
			vertexOutput vert(vertexInput v) {
				vertexOutput o;
				
				o.normalWorld = normalize( mul( float4( v.normal, 0.0 ), _World2Object ).xyz );
				o.tangentWorld = normalize( mul( _Object2World, v.tangent ).xyz );
				o.binormalWorld = normalize( cross( o.normalWorld, o.tangentWorld) * v.tangent.w);
				
				float4 posWorld = mul(_Object2World, v.vertex);
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.tex = v.texcoord;
				
				o.viewDirection = normalize( _WorldSpaceCameraPos.xyz - posWorld.xyz );

				float3 vertexToLightSource = _WorldSpaceLightPos0.xyz - posWorld.xyz;
				float distance = length(vertexToLightSource);

				float atten = lerp(1.0, 1.0/distance, _WorldSpaceLightPos0.w);
				float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, vertexToLightSource, _WorldSpaceLightPos0.w));

				o.lightDirection = float4( lightDirection, atten );

				return o;

			}
			
			//fragment function
			float4 frag(vertexOutput i) : COLOR {
				float4 tex = tex2D(_MainTex, i.tex.xy * _MainTex_ST.xy + _MainTex_ST.zw);
				float4 texN = tex2D(_BumpMap, i.tex.xy * _MainTex_ST.xy + _MainTex_ST.zw);
				
				//unpackNormal function
				float3 localCoords = float3(2.0 * texN.ag - float2(1.0, 1.0), _BumpDepth);
				
				//normal transpose matrix
				float3x3 local2WorldTranspose = float3x3(
					i.tangentWorld,
					i.binormalWorld,
					i.normalWorld
				);
				
				//calculate normal direction
				float3 normalDirection = normalize( mul( localCoords, local2WorldTranspose ) );

				//Lighting
				//float3 diffuseReflection = i.lightDirection.w * _LightColor0.xyz * dot(normalDirection, i.lightDirection.xyz);
				float3 diffuseReflection = i.lightDirection.w * _LightColor0.xyz * max(0.0, dot(normalDirection, i.lightDirection.xyz));
				float3 specularReflection = i.lightDirection.w * _LightColor0.xyz * _SpecColor.xyz * pow(max(0.0, dot(reflect(-i.lightDirection.xyz, normalDirection), i.viewDirection)), _Shininess);
				
				float3 lightFinal = UNITY_LIGHTMODEL_AMBIENT.xyz + diffuseReflection + (specularReflection * tex.a);
				return float4(tex.xyz * lightFinal * _Color.xyz, 1.0);
			}

			ENDCG
		}/*
		Pass {
			Tags { "LightMode" = "ForwardAdd" }
			Blend One One
			CGPROGRAM
			ENDCG
		}*/
	}
	//FallBack "Specular"
}
