using UnityEngine;
using System.Collections;

public class InfoAdicionalManager : MonoBehaviour {
	
	//------------------------------------------------------------------------------------------------
	// Atributos
	//------------------------------------------------------------------------------------------------
	
	// Conexion con los script del DataManager
	private MainManager		mm;
	private DatosCasos		dc;
	private DatosEstudiante	de;
	// Referecnia a los panles del modulo
	public 	GameObject  	panelPreguntas;
	public 	GameObject  	panelRespuestas;
	// Identificador del modulo = ModuloExamenFisico
	
	
	
	//-------------------------------------------------------------------------------------------------
	// Constructor
	//-------------------------------------------------------------------------------------------------
	
	void Start () {
		// Inicializa la conexion con los scripts del DataManager
		
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