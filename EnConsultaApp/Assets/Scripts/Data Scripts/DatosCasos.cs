﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Clase que modela la infromacion del caso actual y permite recuperalos datos
// durante la ejecucion. Este scrip tambien se encarga de mantener vivo el objeto 
// DataManager a lo largo de la ejecucion del programa
public class DatosCasos : MonoBehaviour {

	//------------------------------------------------------------------------------------
	// Atributos
	//------------------------------------------------------------------------------------

	// Contiene los distintos parrafos de informacion que hacer parte del motivo de la
	// consulta expuesta por el paciente
	public	List<string>					motivoConsulta;

	// Contiene una llave pregunta-respuesta con las preguntas que el estudiante puede
	// hacer para conocer informacion de la enfermedad actual
	public	Dictionary<string,string>		enfermedadActual;

	// Contiene una llave pregunta-respuesta con las preguntas que el estudiante puede
	// hacer para conocer los antecedentes del paciente
	public	Dictionary<string,string>		antecedentes;

	// Contiene una llave pregunta-respuesta con las preguntas que debe y no debe hacer
	// para conocer informacion adicional del paciente
	public	Dictionary<string, string>		infoAdicional;

	// Contiene una llave pregunta-valor,razon con la relevancia que el profesor da a cada una
	// de las preguntas de la infromacion adicional
	public	Dictionary<string, string[]>	preguntasCalificacion;

	// Contiene una llave examen-resultado con los examenes dispuestos por el profesor
	// y el resultado que estos arrojan
	public	Dictionary<string, string>		resultadosExamenes;

	// Contienen una llave examen-valor,razon con los examenes dispuestos en la lista anterior
	// y la relevancia que el profesor dispone de cada uno de ellos
	public	Dictionary<string, string[]>	examenesCalificacion;

	// Continene una lista con los posibles diagnoticos al caso, dispuestos por el profesor
	public 	List<string>					diagnosticosPosibles; 

	//---------------------------------------------------------------------------------------
	// Constructor
	//---------------------------------------------------------------------------------------

	// No permite que el objeto se destruya al cambiar la escena
	void Awake(){
		DontDestroyOnLoad(this.gameObject);
	}
	
	void Start () {
		motivoConsulta = new List<string>();
		enfermedadActual = new Dictionary<string,string>();
		antecedentes = new Dictionary<string, string>();
		infoAdicional = new Dictionary<string, string>();
		preguntasCalificacion = new Dictionary<string, string[]>();
		resultadosExamenes = new Dictionary<string, string>();
		examenesCalificacion = new Dictionary<string, string[]>();
		diagnosticosPosibles = new List<string>();

		Inicializar();
	}

	//--------------------------------------------------------------------------------------
	// Metodos
	//--------------------------------------------------------------------------------------

	// Update is called once per frame
	void Update () {
	
	}

	public void Inicializar(){
		Debug.Log("iniciando");

		// Carga motivoConsulta
		TextAsset request = (TextAsset)Resources.Load("Caso/motivoConsulta", typeof(TextAsset));
		string[] data = request.text.Split('\n');
		for(int i=0;i < data.Length;i++){
			motivoConsulta.Add(data[i]);
		}

		//carga enfermedad actual
		request = (TextAsset)Resources.Load("Caso/enfermedadActual", typeof(TextAsset));
		data = request.text.Split('\n');
		for(int i=0;i < data.Length;i++){
			string[] dupla = data[i].Split(';');
			enfermedadActual.Add(dupla[0],dupla[1]);
		}

		//carga antecedentes
		request = (TextAsset)Resources.Load("Caso/antecedentes", typeof(TextAsset));
		data = request.text.Split('\n');
		for(int i=0;i < data.Length;i++){
			string[] dupla = data[i].Split(';');
			antecedentes.Add(dupla[0],dupla[1]);
		}

		//carga infoAdicional
		request = (TextAsset)Resources.Load("Caso/infoAdicional", typeof(TextAsset));
		data = request.text.Split('\n');
		for(int i=0;i < data.Length;i++){
			string[] dupla = data[i].Split(';');
			infoAdicional.Add(dupla[0],dupla[1]);
		}

		//carga preguntasCalificacion
		request = (TextAsset)Resources.Load("Caso/preguntasCalificacion", typeof(TextAsset));
		data = request.text.Split('\n');
		for(int i=0;i < data.Length;i++){
			string[] tripleta = data[i].Split(';');
			string[] valor = new string[2];
			valor[0] = tripleta[1];
			valor[1] = tripleta[2];
			preguntasCalificacion.Add(tripleta[0],valor);
		}
	

		//carga resultadosExamenes
		request = (TextAsset)Resources.Load("Caso/resultadosExamenes", typeof(TextAsset));
		data = request.text.Split('\n');
		for(int i=0;i < data.Length;i++){
			string[] dupla = data[i].Split(';');
			resultadosExamenes.Add(dupla[0],dupla[1]);
		}

		//carga examenesCalificacion
		request = (TextAsset)Resources.Load("Caso/examenesCalificacion", typeof(TextAsset));
		data = request.text.Split('\n');
		for(int i=0;i < data.Length;i++){
			string[] tripleta = data[i].Split(';');
			string[] valor = new string[2];
			valor[0] = tripleta[1];
			valor[1] = tripleta[2];
			examenesCalificacion.Add(tripleta[0],valor);
		}

		//carga diagnosticosPosibles
		request = (TextAsset)Resources.Load("Caso/diagnosticosPosibles", typeof(TextAsset));
		data = request.text.Split('\n');
		for(int i=0;i < data.Length;i++){
			diagnosticosPosibles.Add(data[i]);
		}

		Debug.Log("Carga Exitosa");
	}
}