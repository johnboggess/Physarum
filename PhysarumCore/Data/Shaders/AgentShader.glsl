-- Agent
#version 430

const float pi = 3.141592654;

layout (binding=0, rgba8) uniform image2D Texture;
uniform int Width;
uniform int Height;
uniform float Speed;
uniform int Iteration;
uniform float RandomDirection;

struct Agent
{
	vec2 Position;
	float Direction;
	bool Defined;
	float fs;
	float ls;
	float rs;
	int id;
	float debug0;
	float debug1;
	float debug2;
};

layout(std430, binding = 3) buffer Agents
{
    Agent agents[];
};

layout (local_size_x = 100, local_size_y = 1) in;

float rand(vec2 co){
    return fract(sin(dot(co.xy ,vec2(12.9898,78.233))) * 43758.5453);
}

vec2 directionToVector(float dir)
{
	return vec2(cos(dir), sin(dir));
}


float sampleArea(ivec2 center, int size, inout Agent agent)
{
	int startEnd = size/2;
	float v = 0;
	for(int xx = -startEnd; xx <= startEnd; xx++)
	{
		for(int yy = -startEnd; yy <= startEnd; yy++)
		{
			vec4 c = imageLoad(Texture, center + ivec2(xx,yy));
			v+= float(c.x > 0 && c.y > 0) * c.x;
		}
	}
	return v;
}


vec2 getVelocity(inout Agent agent)
{
	float x = agent.Position.x;
	float y = agent.Position.y;
	float dir = agent.Direction;

	vec2 forward = directionToVector(dir);
	forward = forward * 20;
	
	vec2 left = directionToVector(dir+pi/4f);
	left = left * 20;
	
	vec2 right = directionToVector(dir-pi/4f);
	right = right * 20;


	float fs = sampleArea(ivec2(x,y) + ivec2(forward), 7, agent);
	float ls = sampleArea(ivec2(x,y) + ivec2(left), 7, agent);
	float rs = sampleArea(ivec2(x,y) + ivec2(right), 7, agent);


	agent.fs = fs;
	agent.ls = ls;
	agent.rs = rs;

	/*int size = 13;
	int startEnd = size/2;
	float s = sampleArea(ivec2(x,y) + ivec2(forward), 7, agent);

	if(s > 0)
	{
		for(int xx = -startEnd; xx <= startEnd; xx++)
		{
			for(int yy = -startEnd; yy <= startEnd; yy++)
			{
				imageStore(Texture, ivec2(x,y) + ivec2(forward) + ivec2(xx,yy), vec4(s*2f,0,0,0));
				imageStore(Texture, ivec2(x,y) + ivec2(left) + ivec2(xx,yy), vec4(1,0,0,0));
				imageStore(Texture, ivec2(x,y) + ivec2(right) + ivec2(xx,yy), vec4(0,1,0,0));
			}
		}
	}*/


	vec2 steerVec = normalize(forward)*fs + normalize(left)*ls + normalize(right)*rs;
	float steerAmount = atan(steerVec.y, steerVec.x) - atan(forward.y, forward.x);
	steerAmount = steerAmount * float(steerVec.x != 0 || steerVec.y != 0);

	dir = dir + steerAmount;
	dir = dir + ((rand(vec2(x+Iteration,y+Iteration))*2f - 1f) * RandomDirection); 

	vec2 vel = directionToVector(dir)*Speed;

	int xbounce = int((x+vel.x) >= 0 && (x+vel.x) < Width);
	xbounce = (xbounce*2) - 1;
	vel.x = vel.x*xbounce;
	
	int ybounce = int((y+vel.y) >= 0 && (y+vel.y) < Height);
	ybounce = (ybounce*2) - 1;
	vel.y = vel.y*ybounce;

	return vel;
}

void move(inout Agent agent)
{
	float x = agent.Position.x;
	float y = agent.Position.y;

	vec2 vel = getVelocity(agent);
	
	agent.Direction = atan(vel.y, vel.x);
	agent.Position.x = x+vel.x;
	agent.Position.y = y+vel.y;

}

void paint(inout Agent agent)
{
	float x = agent.Position.x;
	float y = agent.Position.y;

	vec2 forward = directionToVector(agent.Direction);
	forward = forward * 20;
	
	vec2 left = directionToVector(agent.Direction+pi/4f);
	left = left * 20;
	
	vec2 right = directionToVector(agent.Direction-pi/4f);
	right = right * 20;


	imageStore(Texture, ivec2(x,y), vec4(1,1,0,0));
}

void main()
{
	Agent agent = agents[gl_GlobalInvocationID.x];

	move(agent);
	paint(agent);
	
	agents[gl_GlobalInvocationID.x] = agent;
}