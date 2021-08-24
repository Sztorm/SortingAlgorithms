#ifndef GRAPHSCRIPT_DUST_INCLUDED
#define GRAPHSCRIPT_DUST_INCLUDED

void GraphScript_Dust_float(
	float Noise,
	float4 Color,
	float Intensity,
	out float OutDustGreyScale,
	out float4 OutDustColor)
{
	float dustUnclamped = Intensity - Noise;

	OutDustGreyScale = saturate(dustUnclamped);
	OutDustColor = Color * OutDustGreyScale;
}

#endif