//this var comes from the vertex shader
uniform mat4 model;
uniform mat4 viewprojection;

//they are baricentric interpolated by pixel according to the distance to every vertex
varying vec3 v_wPos;
varying vec3 v_wNormal;

//here create uniforms for all the data we need here
uniform vec3 camera_position;
uniform vec3 light_position;

uniform vec3 light_ambient;
uniform vec3 light_diffuse;
uniform vec3 light_specular;
uniform vec3 material_color;



//this var comes from the vertex shader
varying vec2 v_coord;

//the texture passed from the application
uniform sampler2D color_texture;
uniform sampler2D texture_normal;

void main()
{
	//here write the computations for PHONG.
	vec4 colorT = texture2D( color_texture, v_coord );
	//Calculem els vectors L, V, N per realitzar els càlculs de la llum
	vec3 L = light_position - v_wPos.xyz;
	vec3 V = camera_position - v_wPos.xyz;
	vec3 normal = texture2D(texture_normal, v_coord).xyz;
	normal = normal * 2.0 - 1.0;
	vec3 N=(model * vec4(normal,0.0)).xyz;
	N = normalize(N);
	//Normalitzem els vectors
	
	L = normalize(L);
	V = normalize(V);

	//Calculem el vector R que serà la reflexió de L respecte a la Normal
	vec3 R = reflect(-L, N);	

	//calculem les contribucions de la llum difusa i l'especular
	vec3 diffuse = clamp(dot(N,L),0.0,1.0) * light_diffuse;

	vec3 specular = pow(clamp(dot(R,V),0.0,1.0), 30.0) * light_specular;
	vec3 ks =colorT.xyz*colorT.w;
	//Calculem el color final segons la llum i les propietats del material
	vec3 color = colorT.xyz*light_ambient + colorT.xyz*diffuse + ks*specular;
	//vec3 color=(light_ambient+diffuse+specular)*colorT.xyz;
	//set the ouput color por the pixel

	gl_FragColor = vec4( color, 1.0 ) *1.0* vec4(material_color,0.0);
}
