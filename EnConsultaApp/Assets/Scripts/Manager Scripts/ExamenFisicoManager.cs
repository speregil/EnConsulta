using UnityEngine;
using System.Collections;

/*
 * Controla las acciones principales del modulo de examen fisico
 */
public class ExamenFisicoManager : MonoBehaviour {
	//---------------------------------------------------------------
	// Atributos
	//---------------------------------------------------------------
	private MainManager		mm;
	private DatosCasos		dc;
	private DatosEstudiante	de;
	public	string 			IDModulo;

	//---------------------------------------------------------------
	// Constructor
	//---------------------------------------------------------------

	// Use this for initialization
	void Start () {
		GameObject dm = GameObject.Find("DataManager");
		mm = (MainManager)dm.GetComponent(typeof(MainManager));
		dc = (DatosCasos)dm.GetComponent(typeof(DatosCasos));
		de = (DatosEstudiante)dm.GetComponent(typeof(DatosEstudiante));

		Debug.Log(dc.resultadosExamenes.Values);
	}

	//---------------------------------------------------------------
	// Metodos
	//---------------------------------------------------------------

	// Update is called once per frame
	void Update () {
	
	}
}
