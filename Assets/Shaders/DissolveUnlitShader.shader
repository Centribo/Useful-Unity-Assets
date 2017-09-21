Shader "Custom/Unlit/Dissolve Shader" {
	Properties { // "Public variables" to plug in data
		_MainTexture ("Main Texture", 2D) = "white" {}
		_Color ("Colour", Color) = (1, 1, 1, 1)
		_DissolveTexture ("Dissolve Texture", 2D) = "white" {}
		_DissolveCutoff ("Dissolve Cutoff", Range(0, 1)) = 1
	}

	SubShader { //Can have multiple subshaders, especially useful for multiple platforms/quality levels
		Pass { //Can have multiple passes, each pass adds a single draw call
			CGPROGRAM

			//Define what our vertex and fragment functions are going to be
			#pragma vertex vertexFunction
			#pragma fragment fragmentFunction
			
			//Include stuff
			#include "UnityCG.cginc"

			//Data we want to retrieve
			//Vertices, normal, color, UV, etc.
			struct appdata {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};

			//Object we're building
			struct v2f {
				float4 position : SV_POSITION;
				float2 uv : TEXCOORD0;
			};	

			//Get our properties in CG
			float4 _Color;
			sampler2D _MainTexture;
			sampler2D _DissolveTexture;
			float _DissolveCutoff;

			//Vertex function (Build the object)
			v2f vertexFunction(appdata IN){
				v2f OUT;

				//Model view projection = MVP
				// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'
				OUT.position = UnityObjectToClipPos(IN.vertex);
				OUT.uv = IN.uv;

				return OUT;
			}
			
			//Fragment function (Paint/Colour in the object)
			fixed4 fragmentFunction(v2f IN) : SV_TARGET {
				float4 textureColour = tex2D(_MainTexture, IN.uv);
				float4 dissolveColour = tex2D(_DissolveTexture, IN.uv);
				clip(dissolveColour.rgb - _DissolveCutoff);
				textureColour = textureColour * _Color;
				return textureColour;
			}

			ENDCG
		}
	}
}