Shader "Custom/camera" {
	Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _thresh ("Threshold", Range (0, 16)) = 1.6
        _slope ("Slope", Range (0, 3)) = 1.12
        _keyingColor ("Key Colour", Color) = (1,1,1,1)
        _rgb ("Rgb Color" , Range (0,20)) = 2.2

    }
    
    SubShader {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        LOD 100
        
        Lighting On
        ZWrite On
        AlphaTest Off
        Blend SrcAlpha OneMinusSrcAlpha 
        
        Pass {
            CGPROGRAM
                #pragma vertex vert_img
                #pragma fragment frag
                #pragma fragmentoption ARB_precision_hint_fastest

                sampler2D _MainTex;
                float3 _keyingColor;
                float _thresh;
                float _slope;
                float _rgb;

                #include "UnityCG.cginc"


             float4 frag(v2f_img i) : COLOR{
              float3 input_color = tex2D(_MainTex,i.uv); // resimdeki renkler

              //Seçtiğimiz rengin rgb değerini resmideki aynı rgb değerlerinden çıkarıyoruz.
             float d = abs(length(abs(_keyingColor.rgb  - input_color.rgb  )));

           //  float sharper = 1 - (_thresh + _rgb);
           //  float sharper1 = sharper *(_slope);7

           	float sharper = _thresh *(1 - _slope);
           	float sharper1 = sharper / _rgb;

             float alpha = smoothstep(sharper1,_thresh,d);

             float sharper2 = alpha * _rgb;

             float alpha1 = smoothstep(sharper2 , _thresh ,d);


              
              return float4(input_color,alpha1) ;


             
              
            
              } 
              
            ENDCG
        }
    } 
    
     FallBack "Unlit/Texture"

}
