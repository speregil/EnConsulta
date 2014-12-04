using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class MotivoConsultaManager : MonoBehaviour {

	//------------------------------------------------------------------------------------------------
	// Atributos
	//------------------------------------------------------------------------------------------------
	
	// Conexion con los script del DataManager
	private MainManager		mm;
	private DatosCasos		dc;
	private DatosEstudiante	de;
	// Referecnia a los panles del modulo
	public	Text			txtNarracion;
	
	
	//-------------------------------------------------------------------------------------------------
	// Constructor
	//-------------------------------------------------------------------------------------------------
	
	void Start () {
		// Inicializa la conexion con los scripts del DataManager
		GameObject dm = GameObject.Find("DataManager");
		mm = (MainManager)dm.GetComponent(typeof(MainManager));
		dc = (DatosCasos)dm.GetComponent(typeof(DatosCasos));
		de = (DatosEstudiante)dm.GetComponent(typeof(DatosEstudiante));
		Invoke("MostrarNarracion", 0.5f);
	}

	void MostrarNarracion()
	{
		List<string> motivos = dc.motivoConsulta;
		string tempTexto = "";
		foreach (string parrafo in motivos){
			tempTexto += "\n" + parrafo;
		}
		txtNarracion.text = tempTexto;
	}

	public void AvanzarModulo(){
		mm.CambiarModulo("ModuloEnfermedadActual");
	}
}