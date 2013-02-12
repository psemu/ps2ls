varying vec3 normal;
varying vec3 lightDirection;

varying vec2 texcoord;

void main() 
{ 
    gl_Position = ftransform();

    gl_FrontColor = gl_Color;

	texcoord = gl_MultiTexcoord0;

    vec3 eyeVec = vec3(gl_ModelViewProjectionMatrix * gl_Vertex);

    normal = gl_NormalMatrix * gl_Normal;

    lightDirection = -eyeVec;
}