using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class FeedbackManager : MonoBehaviour {
	private MainManager		mm;
	private DatosCasos		dc;
	private DatosEstudiante	de;

	public Text feedB;
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
		mostrarFeedback ();
	}

	void mostrarFeedback()
	{
		bool acabo = false;
		string[] feedback = new string[de.seleccionInfoAdicional.Count];
		string[] respuestas = new string[2];

		de.seleccionInfoAdicional.CopyTo (feedback, 0);
		string text = "";
		for (int pos = 0; pos < de.seleccionInfoAdicional.Count && !acabo; pos++) {
			dc.preguntasCalificacion.TryGetValue(feedback[pos], out respuestas);
			text= text +"\n"+feedback[pos]+": "+respuestas[0]+" "+respuestas[1];		
		}

		feedback = new string[de.seleccionExamenes.Count];
		de.seleccionExamenes.CopyTo (feedback, 0);
		for (int pos = 0; pos < de.seleccionExamenes.Count && !acabo; pos++) {
			dc.examenesCalificacion.TryGetValue(feedback[pos], out respuestas);
			text= text +"\n"+feedback[pos]+": "+respuestas[0]+" "+respuestas[1];		
		}

		feedback = new string[de.diagnosticos.Count];
		de.diagnosticos.CopyTo (feedback, 0);
		for (int pos = 0; pos < de.seleccionExamenes.Count && !acabo; pos++) {
			dc.diagnosticosCalificacion.TryGetValue(feedback[pos], out respuestas);
			text= text +"\n"+feedback[pos]+": "+respuestas[0]+" "+respuestas[1];		
		}


		feedB.text = text;


	}

	public void AvanzarModulo(){
		mm.CambiarModulo("ModuloTratamiento");
	}
}