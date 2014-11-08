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
				Text txt = (Text)toggle.GetComponent(typeof(Text));
				txt.text = keys[i];
				exIndex++;
			}
			else{
				index = i;
				acabo = true;
			}
		}

		if(acabo){
			prevIndex = orIndex;	
			return index;
		}

		return 0;
	}

	public void LimpiarPagina(){
		for(int i = 0; i < examenes.Length; i++){
			GameObject toggle = examenes[i];
			Text txt = (Text)toggle.GetComponent(typeof(Text));
			txt.text = "";
		}
	}

	public void AvanzarPagina(){

		LimpiarPagina();

		btnAnt.SetActive(true);
		nextIndex = MostrarExamenes(nextIndex);

		if(nextIndex == 0)
			btnSig.SetActive(false);
	}

	public void RetrocederPagina(){

		LimpiarPagina();

		btnSig.SetActive(true);
		nextIndex = MostrarExamenes(prevIndex);

		if(prevIndex == 0)
			btnAnt.SetActive(false);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
