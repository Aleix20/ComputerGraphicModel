#include "material.h"



Material::Material()
{
	ambient.set(1,1,1); //reflected ambient light
	diffuse.set(1, 1, 1); //reflected diffuse light
	specular.set(1, 1, 1); //reflected specular light
	shininess = 30.0; //glosiness coefficient (plasticity)
}
Material::Material(Vector3 ambient, Vector3 diffuse, Vector3 specular, float shininess)
{
	this->ambient= ambient; //reflected ambient light
	this->diffuse= diffuse; //reflected diffuse light
	this->specular= specular; //reflected specular light
	this->shininess = shininess; //glosiness coefficient (plasticity)
}


