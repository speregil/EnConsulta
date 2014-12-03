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

	public	GameObject[]	examenes;
	public	GameObject[]	respuestas;

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
		bool acabo = false;
		int orIndex = 0;
		int exIndex = 0;
		panelPreguntas.SetActive(true);

		string[] keys = new string[dc.antecedentes.Keys.Count];
		dc.antecedentes.Keys.CopyTo(keys, 0);
		// Itera para dibujar el nombre de cada examens
		for(int i = 0; i < keys.Length && !acabo; i++){
			//Itera en la lista de examenes hasta que se acaban los labels diponebles en el panel
			if(exIndex < examenes.Length){
				GameObject button = examenes[exIndex];
				GameObject text = button.transform.GetChild(0).gameObject;
				Text txt = (Text)text.GetComponent(typeof(Text));
				txt.text = keys[i];
				
				Button go = (Button)button.GetComponent(typeof(Button));
				go.interactable = true;
				// Determina si el examen ya habia sido seleccionado previamente y lo deja seleccionado

				
				exIndex++;
			}
			else{
				//index = i;
				acabo = true;
			}
		}
		
		if(acabo){
			//Retorna el index en el que se debe empezar a dibujar si faltan examenes en la lista
			//indxAnteriores[prevIndex] = orIndex;
			//return index;
		}
		// Retorna 0 si la lista se termino de dibujar
		//return 0;

		
		
	}
	public void QuitarPreguntas(){
		panelPreguntas.SetActive(false);
		
	}

	public void MostrarRespuestas(int pos){
	
		panelRespuestas.SetActive(true);
		bool acabo = false;
		int orIndex = 0;
		int exIndex = 0;
		panelPreguntas.SetActive(true);
		
		string[] values = new string[dc.antecedentes.Values.Count];
		dc.antecedentes.Values.CopyTo(values, 0);
		// Itera para dibujar el nombre de cada examens
		for(int i = 0; i < values.Length && !acabo; i++){
			//Itera en la lista de examenes hasta que se acaban los labels diponebles en el panel
			if(exIndex < respuestas.Length){
				GameObject text = respuestas[exIndex];			
				Text txt = (Text)text.GetComponent(typeof(Text));
				txt.text = values[pos];
				exIndex++;
				text = respuestas[exIndex];			
			    txt = (Text)text.GetComponent(typeof(Text));
				txt.text = values[pos];

				// Determina si el examen ya habia sido seleccionado previamente y lo deja seleccionado
				
				
				exIndex++;
			}
			else{
				//index = i;
				acabo = true;
			}
		}
		
		if(acabo){
			//Retorna el index en el que se debe empezar a dibujar si faltan examenes en la lista
			//indxAnteriores[prevIndex] = orIndex;
			//return index;
		}
		// Retorna 0 si la lista se termino de dibujar
		//return 0;


		
	}
	public void QuitarRespuestas(){
		panelRespuestas.SetActive(false);
		
	}
}