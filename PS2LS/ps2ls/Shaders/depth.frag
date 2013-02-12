
void main(void)
{
    vec3 color = 1.0 / vec3(gl_FragCoord.z);

    gl_FragColor = vec4(color, 1.0);
}