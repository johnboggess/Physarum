-- Dispersion
#version 430

layout (binding=0, rgba8) uniform image2D Texture;
uniform int Width;
uniform int Height;
uniform float Evaporation;

layout (local_size_x = 10, local_size_y = 10) in;
void main()
{
	vec4 c = imageLoad(Texture, ivec2(gl_GlobalInvocationID.xy));
	c = c - Evaporation;
	c = max(c, vec4(0,0,0,0));
	imageStore(Texture, ivec2(gl_GlobalInvocationID.xy), c);
}