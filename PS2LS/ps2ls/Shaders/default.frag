varying vec3 normal; 
varying vec3 lightDirection;
varying vec2 texcood;

void main()
{
	const vec4 ambientColor = vec4(0.25, 0.25, 0.25, 1.0);
    const vec4 diffuseColor = vec4(1.0, 1.0, 1.0, 1.0);

    vec3 normalizedNormal = normalize(normal);
    vec3 noralizedLightDirection = normalize(lightDirection);

    float diffuseTerm = clamp(dot(normalizedNormal, noralizedLightDirection), 0.0, 1.0);

    gl_FragColor = gl_Color * (ambientColor + (diffuseColor * diffuseTerm));

}