cbuffer CB1// : register(b0)
{
    float4 Acolor;
    float4 Aposition;    
    float4 Acolor3;
};

cbuffer CB2// : register(b1)
{
    float4 Acolor2;
};

struct CB3
{
    float4 Acolor4;
};

struct PSInput
{
    float4 position : SV_POSITION;
    float4 color : COLOR;
};

PSInput VSMain(float4 position : POSITION, float4 color : COLOR)
{
	PSInput result;

    result.position = position + Aposition;
	result.color = color;
    //result.color = m3.Acolor4;
    //result.color = Acolor;
    
    printf("aaaaa");
    //Acolor3[0] = Acolor2;
	return result;
}

float4 PSMain(PSInput input) : SV_TARGET
{
    //return Acolor2;
	//return input.color;
    //return Acolor3;
    return Acolor2;
}