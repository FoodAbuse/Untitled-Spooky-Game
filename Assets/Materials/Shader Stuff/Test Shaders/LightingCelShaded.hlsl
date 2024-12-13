#ifndef LIGHTNG_CEL_SHADED_INCLUDED
#define LIGHTNG_CEL_SHADED_INCLUDED

#ifndef SHADERGRAPH_PREVIEW
struct SurfaceVariables { 
    float3 normal;
};

float3 CalculateCelShading(Light l, SurfaceVariables s) {
    float diffuse = saturate(dot(s.normal, l.direction));

    return l.color * diffuse;
}
#endif

void LightingCelShaded_float(float3 Normal, out float3 Color) {
#if defined(SHADERGRAPH_PREVIEW)

#else 
    SurfaceVariables s;
    s.normal = normalize(Normal);

    Light light = GetMainLight();
    Color = CalculateCelShading(light, s);
#endif
}

#endif

