Shader "Custom/ShowInsideWithCubemapURP"
{
    Properties
    {
        _Cubemap("Cubemap", Cube) = "" {} // Assign a Cubemap here
    }

    SubShader
    {
        Tags { "RenderPipeline" = "UniversalRenderPipeline" "RenderType" = "Opaque" }

        Pass
        {
            Tags { "LightMode" = "UniversalForward" }

            Cull Front // Render only the inside faces
            ZWrite On
            ZTest LEqual

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            // Define cubemap texture
            TEXTURECUBE(_Cubemap);
            SAMPLER(sampler_Cubemap);

            // Vertex Attributes
            struct Attributes
            {
                float4 position : POSITION;
            };

            struct Varyings
            {
                float4 position : SV_POSITION;
                float3 worldPos : TEXCOORD0;
            };

            Varyings vert(Attributes v)
            {
                Varyings o;
                o.position = TransformObjectToHClip(v.position.xyz);
                o.worldPos = TransformObjectToWorld(v.position.xyz);
                return o;
            }

            half4 frag(Varyings i) : SV_Target
            {
                // Convert world position to view direction
                float3 viewDir = normalize(i.worldPos - _WorldSpaceCameraPos);

                // Rotate with the object using its matrix
                float3 rotatedDir = mul((float3x3)unity_WorldToObject, viewDir);
                rotatedDir.x = -rotatedDir.x;

                // Sample from the cubemap
                half4 texColor = SAMPLE_TEXTURECUBE(_Cubemap, sampler_Cubemap, rotatedDir);

                // Ensure valid output
                return texColor;
            }
            ENDHLSL
        }
    }
}
