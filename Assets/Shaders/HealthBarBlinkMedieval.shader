Shader "Custom/UI/HealthBarBlinkMedieval"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		[Toggle]_Blink ("Start Blinking", float) = 0
		_Color ("Normal Color", Color) = (1,1,1,1)
		_BlinkColor ("Blink Color", Color) = (0,0,0,1)
		_Speed("Blink Speed", Range(0.0, 20.0)) = 15
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always
        Tags { "Queue" = "Transparent" }

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			float _Blink;
			float _Speed;
			float3 _Color;
			float3 _BlinkColor;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				if(_Blink)
					col.rgb = lerp(_Color,_BlinkColor,sin(_Speed*_Time.y));
				else
					col.rgb = _Color;
				return col;
			}
			ENDCG
		}
	}
}
