-- Physarum
#version 430

uniform writeonly image2D Texture;
uniform int Width;
uniform int Height;

layout (local_size_x = 10, local_size_y = 10) in;

void main()
{
	ivec2 screenPos = ivec2(gl_GlobalInvocationID.xy);
	imageStore(Texture, screenPos, vec4(screenPos.x/float(Width), screenPos.y/float(Height),0,0));
}