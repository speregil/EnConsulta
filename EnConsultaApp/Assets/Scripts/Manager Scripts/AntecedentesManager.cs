using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class AntecedentesManager : MonoBehaviour {

	//------------------------------------------------------------------------------------------------
	// Atributos
	//------------------------------------------------------------------------------------------------
	
	// Conexion con los script del DataManager
	private MainManager		mm;
	private DatosCasos		dc;
	private DatosEstudiante	de;
	// Referecnia a los panles del modulo
	public 	GameObject  	panelNarracion;
	public 	GameObject  	panelPreguntas;
	public 	GameObject  	panelRespuestas;
	// Identificador del modulo = ModuloExamenFisico


	
	//-------------------------------------------------------------------------------------------------
	// Constructor
	//-------------------------------------------------------------------------------------------------
	
	void Start () {
		// Inicializa la conexion con los scripts del DataManager
		GameObject dm = GameObject.Find("DataManager");
		mm = (MainManager)dm.GetComponent(typeof(MainManager));
		dc = (DatosCasos)dm.GetComponent(typeof(DatosCasos));
		de = (DatosEstudiante)dm.GetComponent(typeof(DatosEstudiante));
	}


	public void MostrarNarracion(){
		panelNarracion.SetActive(true);
	

	}
	public void QuitarNarracion(){
		panelNarracion.SetActive(false);
		
	}

	public void MostrarPreguntas(){
		panelPreguntas.SetActive(true);
		
		
	}
	public void QuitarPreguntas(){
		panelPreguntas.SetActive(false);
		
	}

	public void MostrarRespuestas(){
		panelRespuestas.SetActive(true);
		
		
	}
	public void QuitarRespuestas(){
		panelRespuestas.SetActive(false);
		
	}
}