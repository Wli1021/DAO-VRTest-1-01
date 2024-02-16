Shader "Ripcord Dev/Billboard" {

	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Color ("Color", Color) = (1,1,1,1)
	}

	SubShader {
		
		Tags { "Queue"="Transparent" "RenderType"="Transparent" "DisableBatching"="True" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf NoLighting noambient vertex:vert alpha:fade

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		fixed4 _Color;

		fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten) {
			fixed4 c;
			c.rgb = s.Albedo;
			c.a = s.Alpha;
			return c;
		}
		
		void vert(inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT(Input, o);
			
			// apply object scale
			v.vertex.xy *= float2(length(unity_ObjectToWorld._m00_m10_m20), length(unity_ObjectToWorld._m01_m11_m21));
			
			// get the camera basis vectors
			float3 forward = -normalize(UNITY_MATRIX_V._m20_m21_m22);
			float3 up = normalize(UNITY_MATRIX_V._m10_m11_m12);
			float3 right = normalize(UNITY_MATRIX_V._m00_m01_m02);
			
			// rotate to face camera
			float4x4 rotationMatrix = float4x4(right, 0, up, 0, forward, 0, 0, 0, 0, 1);
			v.vertex = mul(v.vertex, rotationMatrix);
			v.normal = mul(v.normal, rotationMatrix);
			
			// undo object to world transform surface shader will apply
			v.vertex.xyz = mul((float3x3)unity_WorldToObject, v.vertex.xyz);
			v.normal = mul(v.normal, (float3x3)unity_ObjectToWorld);
		}
		
		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}

	ENDCG
	}
}