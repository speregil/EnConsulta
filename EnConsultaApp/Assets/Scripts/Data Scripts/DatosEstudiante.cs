using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

// Clase que modela la infromacion que el estudiante genera durante
// la ejecucion del programa
public class DatosEstudiante : MonoBehaviour {

	//-----------------------------------------------------------------------
	// Atributos
	//-----------------------------------------------------------------------

	//Informacion del estudiante
	public string usuario;

	// Guarda las preguntas seleccionadas por el estudiante en el modulo correspondiente
	public List<string> seleccionInfoAdicional;

	// Guarda las preguntas seleccionadas por el estudiante en el modulo correspondiente
	public List<string> seleccionExamenes;

	public List<string> seleccionTratamientos;

	// Guerda el diagnostico elegido por el estudiante
	public List<string> diagnosticos;

	//------------------------------------------------------------------------
	// Constructor
	//------------------------------------------------------------------------


	// Use this for initialization
	void Start () {
		seleccionInfoAdicional = new List<string>();
		seleccionExamenes = new List<string>();
		diagnosticos = new List<string>();
		usuario = "sinUsuario";
	}

	//------------------------------------------------------------------------
	// Metodos
	//------------------------------------------------------------------------

	//Agrega una pregunta a la lista correspondiente
	public void AgregarSeleccionInfoAdicional(string pregunta){
		seleccionInfoAdicional.Add(pregunta);
	}

	//Agrega un examen a la lista correspondiente
	public void AgregarSeleccionExamen(string examen){
		seleccionExamenes.Add(examen);
	}

	//Asigna el nombre de usuario
	public void AsignarUsuario(Text texto) {
		usuario = texto.text;
	}
}
