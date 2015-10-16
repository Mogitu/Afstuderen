Shader "Custom/AMC3DText" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}		
	}
	SubShader {
		Tags { "Queue"="3000" "IgnoreProjector"="True" "RenderType"="ZTest" } 

		Lighting Off Cull back ZWrite Off ZTest On Fog { Mode Off } 
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			Color [_Color]
			SetTexture[_MainTex]
			{
				combine primary, texture * primary
			}
		}		
	}		
}
