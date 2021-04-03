-- Fade
#version 430

layout (binding=0, rgba8) uniform image2D Texture;
uniform int Width;
uniform int Height;
uniform float DeltaTime;
uniform float FadeRate;
uniform float DiffusionRate;

layout (local_size_x = 10, local_size_y = 10) in;
void main()
{
	vec4 c = imageLoad(Texture, ivec2(gl_GlobalInvocationID.xy));

	vec4 sum = vec4(0,0,0,0);
	for(int x = -1; x < 2; x++)
	{
		for(int y = -1; y < 2; y++)
		{
			sum += imageLoad(Texture, ivec2(gl_GlobalInvocationID.xy) + ivec2(x,y));
		}
	}

	sum = sum/9f;
	
	c = mix(c, sum, DiffusionRate * DeltaTime);
	c = c - (FadeRate * DeltaTime);
	c = max(c, vec4(0,0,0,0));
	imageStore(Texture, ivec2(gl_GlobalInvocationID.xy), c);
}