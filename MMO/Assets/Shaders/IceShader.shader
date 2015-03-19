Shader "Custom/Ice Shader" {
	Properties {
		_Color ("Color Tint", Color) = (1.0,1.0,1.0,1.0)
        _MainTex ("Base (RGB)", 2D) = "white" {}
		_BumpMap ("Normal Texture", 2D) = "bump" {}
		_BumpDepth ("Bump Depth", Range(0.0,1.0)) = 1
		_SpecColor ("Specular Color", Color) = (1.0,1.0,1.0,1.0)
		_Shininess ("Shininess", float) = 10
		_Reflect ("Reflective lightness", Range(1.0,5.0)) = 2
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
			uniform fixed4 _MainTex_ST;
			uniform sampler2D _BumpMap;
			uniform fixed _BumpDepth;
			uniform fixed4 _Color;
			uniform fixed4 _SpecColor;
			uniform half _Shininess;
			uniform half _Reflect;

			//unity defined variables
			uniform half4 _LightColor0;

			//base input structs
			struct vertexInput {
				fixed4 vertex : POSITION;
				fixed3 normal : NORMAL;
				fixed4 texcoord : TEXCOORD0;
				fixed4 tangent : TANGENT;
			};
			struct vertexOutput {
				fixed4 pos : SV_POSITION;
				fixed4 tex : TEXCOORD0;
				fixed4 lightDirection : TEXCOORD1;
				fixed3 viewDirection : TEXCOORD2;
				fixed3 normalWorld : TEXCOORD3;
				fixed3 tangentWorld : TEXCOORD4;
				fixed3 binormalWorld : TEXCOORD5;
			};
			
			//vertex function
			vertexOutput vert(vertexInput v) {
				vertexOutput o;
				
				o.normalWorld = normalize( mul( fixed4( v.normal, 0.0 ), _World2Object ).xyz );
				o.tangentWorld = normalize( mul( _Object2World, v.tangent ).xyz );
				o.binormalWorld = normalize( cross( o.normalWorld, o.tangentWorld) * v.tangent.w);
				
				fixed4 posWorld = mul(_Object2World, v.vertex);
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.tex = v.texcoord;
				
				o.viewDirection = normalize( _WorldSpaceCameraPos.xyz - posWorld.xyz );

				half3 vertexToLightSource = _WorldSpaceLightPos0.xyz - posWorld.xyz;
				half distance = length(vertexToLightSource);

				fixed atten = lerp(1.0, 1.0/distance, _WorldSpaceLightPos0.w);
				fixed3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, vertexToLightSource, _WorldSpaceLightPos0.w));

				o.lightDirection = half4( lightDirection, atten );

				return o;
			}
			
			//fragment function
			fixed4 frag(vertexOutput i) : COLOR {
				fixed4 tex = tex2D(_MainTex, i.tex.xy * _MainTex_ST.xy + _MainTex_ST.zw);
				fixed4 texN = tex2D(_BumpMap, i.tex.xy * _MainTex_ST.xy + _MainTex_ST.zw);
				
				//unpackNormal function
				half3 localCoords = half3(2.0 * texN.ag - fixed2(1.0, 1.0), _BumpDepth);
				
				//normal transpose matrix
				half3x3 local2WorldTranspose = half3x3(
					i.tangentWorld,
					i.binormalWorld,
					i.normalWorld
				);
				
				//calculate normal direction
				half3 normalDirection = normalize( mul( localCoords, local2WorldTranspose ) );

				//Lighting
				//float3 diffuseReflection = i.lightDirection.w * _LightColor0.xyz * dot(normalDirection, i.lightDirection.xyz);
				half3 diffuseReflection = i.lightDirection.w * _LightColor0.xyz * max(0.0, dot(normalDirection, i.lightDirection.xyz));
				half3 specularReflection = i.lightDirection.w * _LightColor0.xyz * _SpecColor.xyz * pow(max(0.0, dot(reflect(-i.lightDirection.xyz, normalDirection), i.viewDirection)), _Shininess);
				
				fixed3 lightFinal = UNITY_LIGHTMODEL_AMBIENT.xyz + diffuseReflection + (specularReflection * tex.a);
				return fixed4(tex.xyz * (lightFinal * _Reflect) * _Color.xyz, 1.0);
			}
			ENDCG
		}
		Pass {
			Tags { "LightMode" = "ForwardAdd" }
			Blend One One
			CGPROGRAM
			//pragmas
			#pragma vertex vert
			#pragma fragment frag
			
			//user defined variables
			uniform sampler2D _MainTex;
			uniform fixed4 _MainTex_ST;
			uniform sampler2D _BumpMap;
			uniform fixed _BumpDepth;
			uniform fixed4 _Color;
			uniform fixed4 _SpecColor;
			uniform half _Shininess;

			//unity defined variables
			uniform half4 _LightColor0;

			//base input structs
			struct vertexInput {
				fixed4 vertex : POSITION;
				fixed3 normal : NORMAL;
				fixed4 texcoord : TEXCOORD0;
				fixed4 tangent : TANGENT;
			};
			struct vertexOutput {
				fixed4 pos : SV_POSITION;
				fixed4 tex : TEXCOORD0;
				fixed4 lightDirection : TEXCOORD1;
				fixed3 viewDirection : TEXCOORD2;
				fixed3 normalWorld : TEXCOORD3;
				fixed3 tangentWorld : TEXCOORD4;
				fixed3 binormalWorld : TEXCOORD5;
			};
			
			//vertex function
			vertexOutput vert(vertexInput v) {
				vertexOutput o;
				
				o.normalWorld = normalize( mul( fixed4( v.normal, 0.0 ), _World2Object ).xyz );
				o.tangentWorld = normalize( mul( _Object2World, v.tangent ).xyz );
				o.binormalWorld = normalize( cross( o.normalWorld, o.tangentWorld) * v.tangent.w);
				
				fixed4 posWorld = mul(_Object2World, v.vertex);
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.tex = v.texcoord;
				
				o.viewDirection = normalize( _WorldSpaceCameraPos.xyz - posWorld.xyz );

				half3 vertexToLightSource = _WorldSpaceLightPos0.xyz - posWorld.xyz;
				half distance = length(vertexToLightSource);

				fixed atten = lerp(1.0, 1.0/distance, _WorldSpaceLightPos0.w);
				fixed3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, vertexToLightSource, _WorldSpaceLightPos0.w));

				o.lightDirection = half4( lightDirection, atten );

				return o;
			}
			
			//fragment function
			fixed4 frag(vertexOutput i) : COLOR {
				fixed4 tex = tex2D(_MainTex, i.tex.xy * _MainTex_ST.xy + _MainTex_ST.zw);
				fixed4 texN = tex2D(_BumpMap, i.tex.xy * _MainTex_ST.xy + _MainTex_ST.zw);
				
				//unpackNormal function
				half3 localCoords = half3(2.0 * texN.ag - fixed2(1.0, 1.0), _BumpDepth);
				
				//normal transpose matrix
				half3x3 local2WorldTranspose = half3x3(
					i.tangentWorld,
					i.binormalWorld,
					i.normalWorld
				);
				
				//calculate normal direction
				half3 normalDirection = normalize( mul( localCoords, local2WorldTranspose ) );

				//Lighting
				//float3 diffuseReflection = i.lightDirection.w * _LightColor0.xyz * dot(normalDirection, i.lightDirection.xyz);
				half3 diffuseReflection = i.lightDirection.w * _LightColor0.xyz * max(0.0, dot(normalDirection, i.lightDirection.xyz));
				half3 specularReflection = i.lightDirection.w * _LightColor0.xyz * _SpecColor.xyz * pow(max(0.0, dot(reflect(-i.lightDirection.xyz, normalDirection), i.viewDirection)), _Shininess);
				
				fixed3 lightFinal = UNITY_LIGHTMODEL_AMBIENT.xyz + diffuseReflection + (specularReflection * tex.a);
				return fixed4(lightFinal * _Color.xyz, 1.0);
			}
			ENDCG
		}
	}
	//FallBack "Specular"
}