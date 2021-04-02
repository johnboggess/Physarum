-- Agent
#version 430

const float pi = 3.141592654;

uniform writeonly image2D Texture;
uniform int Width;
uniform int Height;
uniform float Speed;

layout(std430, binding = 3) buffer Agents
{
    float agents[];
};

layout (local_size_x = 100, local_size_y = 1) in;

float rand(vec2 co){
    return fract(sin(dot(co.xy ,vec2(12.9898,78.233))) * 43758.5453);
}

float getDef(uint agent)
{
	return agents[agent + 0];
}

float getX(uint agent)
{
	return agents[agent + 1];
}

float getY(uint agent)
{
	return agents[agent + 2];
}

float getDir(uint agent)
{
	return agents[agent + 3];
}


void setX(uint agent, float x)
{
	agents[agent + 1] = x;
}
void setY(uint agent, float y)
{
	agents[agent + 2] = y;
}
void setDir(uint agent, float dir)
{
	agents[agent + 3] = dir;
}

vec2 getVelocity(uint agent)
{
	float x = getX(agent);
	float y = getY(agent);
	float dir = getDir(agent);
	
	vec2 vel = vec2(cos(dir), sin(dir))*Speed;

	int xbounce = int((x+vel.x) >= 0 && (x+vel.x) < Width);
	xbounce = (xbounce*2) - 1;
	vel.x = vel.x*xbounce;
	
	int ybounce = int((y+vel.y) >= 0 && (y+vel.y) < Height);
	ybounce = (ybounce*2) - 1;
	vel.y = vel.y*ybounce;

	return vel;
}

void move(uint agent)
{
	float x = getX(agent);
	float y = getY(agent);

	vec2 vel = getVelocity(agent);
	
	setX(agent, x+vel.x);
	setY(agent, y+vel.y);

	setDir(agent, atan(vel.y, vel.x));
}

void paint(uint agent)
{
	float x = getX(agent);
	float y = getY(agent);
	imageStore(Texture, ivec2(x,y), vec4(1,1,0,0));
}

void main()
{
	uint agent = gl_GlobalInvocationID.x*4;

	move(agent);
	paint(agent);
}