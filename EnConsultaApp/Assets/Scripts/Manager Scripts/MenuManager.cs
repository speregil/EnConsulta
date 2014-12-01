using UnityEngine;
using System.Collections;

/*
 * Controla todas las acciones principales del Menu principal
 */
public class MenuManager : MonoBehaviour {
	//-----------------------------------------------------------------------
	// Atributos
	//-----------------------------------------------------------------------

	// Escena de inicio del juego
	public 	string		escenaInicio; 
	// Axeso a los metodos de control del juego
	private MainManager mm;
	//-----------------------------------------------------------------------
	// Constructor
	//-----------------------------------------------------------------------
	
	// Use this for initialization
	void Start () {
		GameObject dm = GameObject.Find("DataManager");
		mm = (MainManager)dm.GetComponent(typeof(MainManager));
	}

	//-----------------------------------------------------------------------
	// Metodos
	//-----------------------------------------------------------------------

	// Lanza el primer modulo del caso
	public void IniciarCaso(){
		mm.CambiarModulo(escenaInicio);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
