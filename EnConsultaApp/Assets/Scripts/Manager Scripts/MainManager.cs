using UnityEngine;
using System.Collections;

/*
 * Maneja los procesos principales durante el desarrollo del juego 
 */
public class MainManager : MonoBehaviour {

	//-----------------------------------------------------------------------------
	// Atributos
	//-----------------------------------------------------------------------------
	public	string	moduloActual;
	//-----------------------------------------------------------------------------
	// Constructor
	//-----------------------------------------------------------------------------

	// No permite que el objeto se destruya al cambiar la escena
	void Awake(){
		DontDestroyOnLoad(this.gameObject);
	}
	
	// Use this for initialization
	void Start () {
		moduloActual = "Menu";
	}

	//-----------------------------------------------------------------------------
	// Metodos
	//-----------------------------------------------------------------------------

	public void CambiarModulo(string IDModulo){
		moduloActual = IDModulo;
		Application.LoadLevel(IDModulo);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}