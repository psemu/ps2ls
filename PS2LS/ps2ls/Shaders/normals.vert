varying vec4 color;

void main(void)
{
    gl_Position = ftransform();

    color.xyz = (-gl_Normal * 0.5) + 0.5;
    color.w = 1.0;
}