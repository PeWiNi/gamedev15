Shader "Custom/Shader1" {
	Properties {
		_Color ("Color Tint", Color) = (1.0,1.0,1.0,1.0)
		_MainTex ("Diffuse Texture, gloss(A)", 2D) = "white" {}
		_BumpMap ("Normal Texture", 2D) = "bump" {}
		_EmitMap ("Emission Texture", 2D) = "black" {}
		_BumpDepth ("Bump Depth", Range(0.0,1.0)) = 1
		_SpecColor ("Specular Color", Color) = (1.0,1.0,1.0,1.0)
		_Shininess ("Shininess", float) = 0
		_RimColor ("Rim Color", Color) = (1.0,1.0,1.0,1.0)
		_RimPower ("Rim Power", Range(0.1,10.0)) = 0
		_EmitStrength ("Decal Strength", Range(0.0,2.0)) = 0
	}
	SubShader {
		Pass {
			Tags { "LightMode" = "ForwardBase" }
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			//user defined variables
			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			uniform sampler2D _BumpMap;
			uniform float4 _BumpMap_ST;
			uniform sampler2D _DecalMap;
			uniform float4 _DecalMap_ST;
			uniform float4 _Color;
			uniform float4 _SpecColor;
			uniform float4 _RimColor;
			uniform float _Shininess;
			uniform float _RimPower;
			uniform float _BumpDepth;
			uniform float _DecalStrength;

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

				float3 fragmentToLightSource = _WorldSpaceLightPos0.xyz - posWorld.xyz;
				float distance = length(fragmentToLightSource);

				float atten = lerp(1.0, 1.0/distance, _WorldSpaceLightPos0.w);
				float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0, fragmentToLightSource, _WorldSpaceLightPos0.w));
			
				o.lightDirection = float4(lightDirection, atten);

				return o;
			}

			//fragment function
			float4 frag(vertexOutput i) : COLOR {
				//Texture Maps
				float4 tex = tex2D(_MainTex, i.tex.xy * _MainTex_ST.xy + _MainTex_ST.zw);
				float4 texN = tex2D(_BumpMap, i.tex.xy * _BumpMap_ST.xy + _BumpMap_ST.zw);
				float4 texD = tex2D(_DecalMap, i.tex.xy * _DecalMap_ST.xy + _DecalMap_ST.zw);

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

				//lighting
				float3 diffuseReflection = i.lightDirection.w * _LightColor0.xyz * saturate(dot(normalDirection, i.lightDirection.xyz));
				float3 specularReflection = diffuseReflection * _SpecColor.xyz * pow(saturate(dot(reflect(-i.lightDirection.xyz, normalDirection), i.viewDirection)), _Shininess);

				//Rim Lightning
				float rim = 1 - saturate(dot(i.viewDirection, normalDirection));
				float3 rimLighting = saturate(dot(normalDirection, i.lightDirection.xyz) * _RimColor.xyz * _LightColor0.xyz * pow( rim, _RimPower));

				float3 lightFinal = UNITY_LIGHTMODEL_AMBIENT.xyz + diffuseReflection + (specularReflection * tex.a) + rimLighting + (texD.xyz * _DecalStrength);

				return float4(tex.xyz * lightFinal * _Color.xyz, 1.0);
			}
			ENDCG
		}
	}
	FallBack "Diffuse"
}
