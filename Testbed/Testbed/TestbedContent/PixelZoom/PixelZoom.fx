sampler ScreenS : register(s0);

int ZoomLevel;

float4 main(float4 color : COLOR0, float2 texCoord: TEXCOORD0) : COLOR0
{
	float2 zoomedCoords = texCoord;
	//zoomedCoords.x = texCoord.x;
	float4 tex = tex2D(ScreenS, zoomedCoords);	
	

	return tex;
}

technique PixelZoom
{
	pass Pass1
	{
		PixelShader = compile ps_2_0 main();
	}
}