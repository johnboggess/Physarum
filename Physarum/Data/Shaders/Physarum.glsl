-- Physarum
#version 430

uniform writeonly image2D Texture;
uniform int Width;
uniform int Height;

uniform float[100*4] Agents;


layout (local_size_x = 100, local_size_y = 1) in;

float rand(vec2 co){
    return fract(sin(dot(co.xy ,vec2(12.9898,78.233))) * 43758.5453);
}

void main()
{
	float def = Agents[gl_GlobalInvocationID.x*4 + 0];
	float x = Agents[gl_GlobalInvocationID.x*4 + 1];
	float y = Agents[gl_GlobalInvocationID.x*4 + 2];
	float dir = Agents[gl_GlobalInvocationID.x*4 + 3];

	imageStore(Texture, ivec2(x, y), vec4(1,1,0,0));
}