using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnfermedadActualManager : MonoBehaviour {

	//------------------------------------------------------------------------------------------------
	// Atributos
	//------------------------------------------------------------------------------------------------
	
	// Conexion con los script del DataManager
	private MainManager		mm;
	private DatosCasos		dc;
	private DatosEstudiante	de;
	// Referecnia a los panles del modulo
	public 	GameObject		panelPreguntas;
	public 	GameObject		panelRespuestas;
	public 	GameObject		panelNarracion;
	public 	GameObject		scrollBar;
	public	Text[]			preguntas;
	public	Button[]		btnPreguntas;
	public	Text			respuesta;
	public	GameObject		botonNarracion;
	public	GameObject		botonPreguntas;
	public	Text			txtNarracion;
	public	GameObject		btnAceptarRespuesta;

	
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
	
	//---------------------------------------------------------------------------------------------------
	// Metodos
	//---------------------------------------------------------------------------------------------------
	
	public void MostrarPreguntas()
	{
		botonNarracion.SetActive(false);
		botonPreguntas.SetActive(false);
		panelPreguntas.SetActive(true);
		string[] keys = new string[dc.enfermedadActual.Keys.Count];
		dc.enfermedadActual.Keys.CopyTo(keys, 0);
		for(int i = 0; i < keys.Length; i++){
			preguntas[i].text = keys[i];
			btnPreguntas[i].interactable = true;
		}		
	}

	public void MostrarRespuesta(int i)
	{
		panelRespuestas.SetActive(true);
		string[] keys = new string[dc.enfermedadActual.Keys.Count];
		dc.enfermedadActual.Keys.CopyTo(keys, 0);
		string tempRespuesta;
		string tempPregunta = keys[i];
		dc.enfermedadActual.TryGetValue(tempPregunta, out tempRespuesta);
		respuesta.text = tempRespuesta;
	}

	public void QuitarRespuesta(int i)
	{
		panelRespuestas.SetActive(false);
	}

	public void MostrarNarracion()
	{
		botonNarracion.SetActive(false);
		botonPreguntas.SetActive(false);
		panelNarracion.SetActive(true);
		scrollBar.SetActive(true);
		string[] keys = new string[dc.enfermedadActual.Keys.Count];
		dc.enfermedadActual.Keys.CopyTo(keys, 0);
		string tempTexto = "";
		for(int i = 0; i < keys.Length; i++){
			tempTexto += "\nPregunta: " + keys[i];
			string tempRespuesta;
			string tempPregunta = keys[i];
			dc.enfermedadActual.TryGetValue(tempPregunta, out tempRespuesta);
			tempTexto += "\nRespuesta: " + tempRespuesta;
		}
		txtNarracion.text = tempTexto;
	}

	public void AvanzarModulo(){
		mm.CambiarModulo("ModuloAntecedentes");
	}
}
