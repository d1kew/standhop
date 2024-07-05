Shader "Particles/FXVille Blood Fast" {
	Properties {
		_Columns ("Flipbook Columns", Float) = 1
		_Rows ("Flipbook Rows", Float) = 1
		_TintColor ("Tint Color", Vector) = (0.5,0.5,0.5,0.5)
		_ChannelMask ("Channel Mask Color", Vector) = (0,0,0,1)
		_MainTex ("Particle Texture", 2D) = "white" {}
		_EdgeMin ("SmoothStep Edge Min", Float) = 0.05
		_EdgeSoft ("SmoothStep Softness", Float) = 0.05
		_Detail ("Detail Tex", 2D) = "gray" {}
		_DetailTile ("Detail Tiling", Float) = 6
		_DetailPan ("Detail Alpha Pan", Float) = 0.1
		_DetailAlphaAffect ("Detail Alpha Affect", Float) = 1
		_DetailBrightAffect ("Detail Brightness Affect", Float) = 0.5
		_Overbright ("Overbright", Float) = 0
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Paricles/Alpha Blended"
}