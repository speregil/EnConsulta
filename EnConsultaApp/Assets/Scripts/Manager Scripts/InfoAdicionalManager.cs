using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InfoAdicionalManager : MonoBehaviour {
	
	//------------------------------------------------------------------------------------------------
	// Atributos
	//------------------------------------------------------------------------------------------------
	
	// Conexion con los script del DataManager
	private MainManager		mm;
	private DatosCasos		dc;
	private DatosEstudiante	de;
	private int posX;
	// Referecnia a los panles del modulo
	public 	GameObject  	panelPreguntas;
	public 	GameObject  	panelRespuestas;
	// Identificador del modulo = ModuloExamenFisico
	public 	GameObject  	pregunta;
	public 	GameObject  	respuesta;
	
	
	
	//-------------------------------------------------------------------------------------------------
	// Constructor
	//-------------------------------------------------------------------------------------------------
	
	void Start () {
		// Inicializa la conexion con los scripts del DataManager
		GameObject dm = GameObject.Find("DataManager");
 		mm = (MainManager)dm.GetComponent(typeof(MainManager));
		dc = (DatosCasos)dm.GetComponent(typeof(DatosCasos));
		de = (DatosEstudiante)dm.GetComponent(typeof(DatosEstudiante));
		posX = 100;
	}

	
	public void MostrarPreguntas(int pos){
		panelPreguntas.SetActive(true);
		bool acabo = false;
		int orIndex = 0;
		int exIndex = 0;
		panelPreguntas.SetActive(true);
		
		string[] keys = new string[dc.infoAdicional.Keys.Count];
		dc.infoAdicional.Keys.CopyTo(keys, 0);
		// Itera para dibujar el nombre de cada examens

			//Itera en la lista de examenes hasta que se acaban los labels diponebles en el panel

				GameObject button = pregunta;
				GameObject text = button.transform.GetChild(0).gameObject;
				Text txt = (Text)text.GetComponent(typeof(Text));
				txt.text = keys[pos];
				posX = pos;
				Button go = (Button)button.GetComponent(typeof(Button));
				go.interactable = true;
				// Determina si el examen ya habia sido seleccionado previamente y lo deja seleccionado
				
				
				
			
	
		

		// Retorna 0 si la lista se termino de dibujar
		//return 0;

		
		
	}
	public void QuitarPreguntas(){
		panelPreguntas.SetActive(false);
		
	}
	
	public void MostrarRespuestas(){
		panelRespuestas.SetActive(true);
		bool acabo = false;
		int orIndex = 0;
		int exIndex = 0;
		panelPreguntas.SetActive(true);
		
		string[] values = new string[dc.infoAdicional.Values.Count];
		dc.infoAdicional.Values.CopyTo(values, 0);
		// Itera para dibujar el nombre de cada examens
		
		//Itera en la lista de examenes hasta que se acaban los labels diponebles en el panel
		

		GameObject text = respuesta;
		Text txt = (Text)text.GetComponent(typeof(Text));
		txt.text = values[posX];

		// Determina si el examen ya habia sido seleccionado previamente y lo deja seleccionado
		
		
		

		
	}
	public void QuitarRespuestas(){
		panelRespuestas.SetActive(false);
		
	}

	public void AvanzarModulo(){
		mm.CambiarModulo("ModuloExamenFisico");
	}
}