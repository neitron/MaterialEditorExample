Shader "Neitron/ImageFx"
{
    Properties
    {
        [HideInInspector] _MainTex ("Texture", 2D) = "white" {}
		_Speed ("Speed", Range(0.0, 1.0)) = 0.5
		_Frequency("Frequency", Range(1.0, 20.0)) = 5.0
    }
    SubShader
    {
		Tags
		{
			"PreviewType" = "Plane"
		}
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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
			float _Speed;
			float _Frequency;
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
			
				col.rgb = lerp(col.rgb, 1.0f - col.rgb, frac(cos(i.uv.x + _Time.w * _Speed) * sin(i.uv.y) * _Frequency));
                return col;
            }
            ENDCG
        }
    }
}
