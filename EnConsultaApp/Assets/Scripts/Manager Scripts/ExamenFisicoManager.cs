using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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
	public	GameObject[]	examenes;
	public	GameObject		btnSig;
	public	GameObject		btnAnt;
	public	int				nextIndex;
	public 	int				prevIndex;
	public	int[]			indxAnteriores;
	public	List<string>	seleccionados;
	public	GameObject		panelConfirmacion;

	//---------------------------------------------------------------
	// Constructor
	//---------------------------------------------------------------

	// Use this for initialization
	void Start () {
		IDModulo = "ModuloExamenFisico";
		GameObject dm = GameObject.Find("DataManager");
		mm = (MainManager)dm.GetComponent(typeof(MainManager));
		dc = (DatosCasos)dm.GetComponent(typeof(DatosCasos));
		de = (DatosEstudiante)dm.GetComponent(typeof(DatosEstudiante));
		seleccionados = new List<string>();
		indxAnteriores = new int[dc.resultadosExamenes.Keys.Count];
		prevIndex = 0;
		nextIndex = MostrarExamenes(0);

		if(nextIndex != 0)
			btnSig.SetActive(true);
	}

	//---------------------------------------------------------------
	// Metodos
	//---------------------------------------------------------------

	public int MostrarExamenes(int index){
		bool acabo = false;
		int orIndex = index;
		int exIndex = 0;
	    string[] keys = new string[dc.resultadosExamenes.Keys.Count];
		dc.resultadosExamenes.Keys.CopyTo(keys, 0);
		for(int i = index; i < keys.Length && !acabo; i++){
			if(exIndex < examenes.Length){
				GameObject toggle = examenes[exIndex];
				GameObject label = toggle.transform.GetChild(0).GetChild(1).gameObject;
				Text txt = (Text)label.GetComponent(typeof(Text));
				txt.text = keys[i];

				Toggle go = (Toggle)toggle.GetComponent(typeof(Toggle));
				go.interactable = true;
				exIndex++;
			}
			else{
				index = i;
				acabo = true;
			}
		}

		if(acabo){
			indxAnteriores[prevIndex] = orIndex;
			return index;
		}

		return 0;
	}

	public void LimpiarPagina(){
		for(int i = 0; i < examenes.Length; i++){
			GameObject toggle = examenes[i];
			Text txt = (Text)toggle.GetComponent(typeof(Text));
			txt.text = "";

			GameObject ot = GameObject.Find("Toggle" + (i+1));
			Toggle t = (Toggle)ot.GetComponent(typeof(Toggle));
			t.isOn = false;
		}
	}

	public void AvanzarPagina(){

		LimpiarPagina();

		btnAnt.SetActive(true);
		prevIndex++;
		nextIndex = MostrarExamenes(nextIndex);

		if(nextIndex == 0)
			btnSig.SetActive(false);
	}

	public void RetrocederPagina(){

		LimpiarPagina();

		btnSig.SetActive(true);
		prevIndex--;
		nextIndex = MostrarExamenes(indxAnteriores[prevIndex]);
		if(prevIndex == 0)
			btnAnt.SetActive(false);
	}

	public void AgregarSeleccionado(GameObject toggle){

		Toggle t = (Toggle)toggle.GetComponent(typeof(Toggle));
		GameObject l = toggle.transform.GetChild(0).GetChild(1).gameObject;
		Text txt = (Text)l.GetComponent(typeof(Text));
		string llave = txt.text;
		if(t.isOn){
			if(!seleccionados.Contains(llave))
				Debug.Log("Meti: " + llave);
				seleccionados.Add(llave);
		}
		else{
			if(!llave.Equals(""))
				seleccionados.Remove(llave);
		}
	}

	public void Seleccionar(){
		panelConfirmacion.SetActive(true);
		GameObject panel = GameObject.Find("txtLista");

		Text txt = (Text)panel.GetComponent(typeof(Text));
		string lista = "";

		for(int i = 0; i < seleccionados.Count; i++){
			lista = lista + "\n" + seleccionados[i];
		}

		txt.text = lista;
	}
	// Update is called once per frame
	void Update () {
	
	}
}