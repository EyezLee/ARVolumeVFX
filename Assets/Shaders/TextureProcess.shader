Shader "Custom/AR/TextureProcess"
{
    Properties
    {
        _MainTex("", 2D) = "black" {}
        _textureY("", 2D) = "black" {}
        _textureCbCr("", 2D) = "black" {}
        _HumanStencil("", 2D) = "black" {}
        _EnvironmentDepth("", 2D) = "black" {}
    }
    SubShader
    {
        Cull Off ZTest Always ZWrite Off
        Pass
        {
            HLSLPROGRAM
            #define LIDAR_COLOR
            #include "TextureProcess.hlsl"
            #pragma vertex Vertex
            #pragma fragment Fragment
            ENDHLSL
        }
        Pass
        {
            HLSLPROGRAM
            #define LIDAR_DEPTH
            #include "TextureProcess.hlsl"
            #pragma vertex Vertex
            #pragma fragment Fragment
            ENDHLSL
        }
            
    }
}
