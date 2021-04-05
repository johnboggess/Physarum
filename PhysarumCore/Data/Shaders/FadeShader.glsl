﻿-- Fade
#version 430

layout (binding=0, rgba8) uniform image2D Texture;
uniform int Width;
uniform int Height;
uniform float DeltaTime;

layout(std140, binding = 0) uniform Settings
{
	float FadeRate;
    float DiffusionRate;
} settings;

layout (local_size_x = 10, local_size_y = 10) in;
void main()
{
	vec4 c = imageLoad(Texture, ivec2(gl_GlobalInvocationID.xy));

	vec4 sum = vec4(0,0,0,0);
	ivec2 kernel = ivec2(3,3);
	ivec2 startEnd = kernel / 2;
	for(int x = -startEnd.x; x <= startEnd.x; x++)
	{
		for(int y = -startEnd.y; y <= startEnd.y; y++)
		{
			sum += imageLoad(Texture, ivec2(gl_GlobalInvocationID.xy) + ivec2(x,y));
		}
	}

	sum = sum/(kernel.x*kernel.y);
	
	c = mix(c, sum, settings.DiffusionRate);// * DeltaTime);
	c = c - settings.FadeRate;//.01;//(FadeRate * DeltaTime);
	c = max(c, vec4(0,0,0,0));
	imageStore(Texture, ivec2(gl_GlobalInvocationID.xy), c);
}