using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ImpresionDiagnosticaManager : MonoBehaviour {

	//------------------------------------------------------------------------------------------------
	// Atributos
	//------------------------------------------------------------------------------------------------
	

	private MainManager		mm;
	private DatosCasos		dc;
	private DatosEstudiante	de;

	public	GameObject		btnSig;
	public	GameObject		btnAnt;
	
	public	GameObject[]	diagnosticos;
	private	List<string>	seleccionados;

	private	int				nextIndex;
	private	int				prevIndex;
	private	int[]			indxAnteriores;
	
	//-------------------------------------------------------------------------------------------------
	// Constructor
	//-------------------------------------------------------------------------------------------------

	// Use this for initialization
	void Start () {
		// Inicializa la conexion con los scripts del DataManager
		GameObject dm = GameObject.Find("DataManager");
		mm = (MainManager)dm.GetComponent(typeof(MainManager));
		dc = (DatosCasos)dm.GetComponent(typeof(DatosCasos));
		de = (DatosEstudiante)dm.GetComponent(typeof(DatosEstudiante));

		seleccionados = new List<string>();

		indxAnteriores = new int[dc.diagnosticosPosibles.Count];
		prevIndex = 0;
		// Dibuja los examenes disponibles en el panel
		nextIndex = MostrarDiagnosticos(0);
		if(nextIndex != 0)
			btnSig.SetActive(true);
	}

	//---------------------------------------------------------------------------------------------------
	// Metodos
	//---------------------------------------------------------------------------------------------------

	public int MostrarDiagnosticos(int index){
		bool acabo = false;
		int orIndex = index;
		int exIndex = 0;

		List<string> list = dc.diagnosticosPosibles; 

		for(int i = index; i < list.Count && !acabo; i++){
			//Itera en la lista de examenes hasta que se acaban los labels diponebles en el panel
			if(exIndex < diagnosticos.Length){
				GameObject toggle = diagnosticos[exIndex];
				GameObject label = toggle.transform.GetChild(0).GetChild(1).gameObject;
				Text txt = (Text)label.GetComponent(typeof(Text));
				txt.text = list[i];
				
				Toggle go = (Toggle)toggle.GetComponent(typeof(Toggle));
				go.interactable = true;
				// Determina si el examen ya habia sido seleccionado previamente y lo deja seleccionado
				if(seleccionados.Contains(txt.text))
					go.isOn = true;
				
				exIndex++;
			}
			else{
				index = i;
				acabo = true;
			}
		}
		
		if(acabo){
			//Retorna el index en el que se debe empezar a dibujar si faltan examenes en la lista
			indxAnteriores[prevIndex] = orIndex;
			return index;
		}
		// Retorna 0 si la lista se termino de dibujar
		return 0;
	}

	// Limpia los label y los Toggle del panel de seleccion
	public void LimpiarPagina(){
		for(int i = 0; i < diagnosticos.Length; i++){
			GameObject toggle = diagnosticos[i];
			
			
			GameObject label = toggle.transform.GetChild(0).GetChild(1).gameObject;
			Text txt = (Text)label.GetComponent(typeof(Text));
			txt.text = "";
			
			Toggle t = (Toggle)toggle.GetComponent(typeof(Toggle));
			t.isOn = false;
			t.interactable = false;
		}
	}

	// Avanza en la pagina de seleccion
	public void AvanzarPagina(){
		
		LimpiarPagina();
		
		btnAnt.SetActive(true);
		prevIndex++;
		nextIndex = MostrarDiagnosticos(nextIndex);
		
		if(nextIndex == 0)
			btnSig.SetActive(false);
	}
	
	// Retrocede en la pagina de seleccion
	public void RetrocederPagina(){
		
		LimpiarPagina();
		
		btnSig.SetActive(true);
		prevIndex--;
		nextIndex = MostrarDiagnosticos(indxAnteriores[prevIndex]);
		if(prevIndex == 0)
			btnAnt.SetActive(false);
	}

	public void AgregarSeleccionado(GameObject toggle){
		// Obtiene el scrip del toggle
		Toggle t = (Toggle)toggle.GetComponent(typeof(Toggle));
		GameObject l = toggle.transform.GetChild(0).GetChild(1).gameObject;
		Text txt = (Text)l.GetComponent(typeof(Text));
		string llave = txt.text;
		// Agrega un examen si se activa un toggle, lo elimina si se desactiva
		if(t.isOn){
			// Solo agrega un examen si no existe ya en la lista
			if(!seleccionados.Contains(llave)){
				seleccionados.Add(llave);
			}
		}
		else{
			// Evita problemas al cambiar las paginas
			if(!llave.Equals(""))
				seleccionados.Remove(llave);
		}
	}

	public void Terminar(){
		de.diagnosticos = seleccionados;
		mm.CambiarModulo("ModuloFeedback");
	}
}