using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TratamientoManager : MonoBehaviour {
	//------------------------------------------------------------------------------------------------
	// Atributos
	//------------------------------------------------------------------------------------------------
	
	// Conexion con los script del DataManager
	private MainManager		mm;
	private DatosCasos		dc;
	private DatosEstudiante	de;
	// Referecnia a los panles del modulo
	public 	GameObject		panelConfirmacion;
	public 	GameObject		panelResultados;
	public 	GameObject		panelMedia;
	// Identificador del modulo = ModuloExamenFisico
	public	string 			IDModulo;
	// Lista con los 8 toggles que comforman el panel de seleccion
	public	GameObject[]	tratamientos;
	// Lista con los 8 labels que conforman el panel de resultados
	public	GameObject[]	resultados;
	// Botones para avanzar y retroceder en el panel de seleccion y resultados
	public	GameObject		btnSig;
	public	GameObject		btnAnt;
	public	GameObject		btnSigResultados;
	public	GameObject		btnAntResultados;
	// Controlan la posicion actual y previa en la viualizacion de los tratamientos
	private	int				nextIndex;
	private	int				prevIndex;
	private	int[]			indxAnteriores;
	// Guarda los tratamientos selecionados por el estudiante
	private	List<string>	seleccionados;
	//Guarda la direccion al audio actual de una lista de resultados
	private	string			audioActual;
	private	string			imagenActual;
	
	//-------------------------------------------------------------------------------------------------
	// Constructor
	//-------------------------------------------------------------------------------------------------
	
	void Start () {
		// Inicializa la conexion con los scripts del DataManager
		GameObject dm = GameObject.Find("DataManager");
		mm = (MainManager)dm.GetComponent(typeof(MainManager));
		dc = (DatosCasos)dm.GetComponent(typeof(DatosCasos));
		de = (DatosEstudiante)dm.GetComponent(typeof(DatosEstudiante));
		// Inicializa la lista de seleccionados
		seleccionados = new List<string>();
		// Iniciliza los atributos de control para recorrer los tratamientos
		indxAnteriores = new int[dc.tratamientosPosibles.Keys.Count];
		prevIndex = 0;
		// Dibuja los tratamientos disponibles en el panel
		nextIndex = Mostrartratamientos(0);
		if(nextIndex != 0)
			btnSig.SetActive(true);
	}
	
	//---------------------------------------------------------------------------------------------------
	// Metodos del panel de Seleccion y Confirmacion
	//---------------------------------------------------------------------------------------------------
	
	// Dibuja los tratamientos disponibles en el panel de seleccion
	// Recibe por parametro el index de la lista de tratamientos donde debe emepzar a dibujar
	// Retorna el index del examen donde qued{o si la lista continua y no se logro dibujar toda
	// Retorna 0 si la lista se completo de dibujar
	public int Mostrartratamientos(int index){
		// Variables de control
		bool acabo = false;
		int orIndex = index;
		int exIndex = 0;
		// Recupera la lista de tratamientos del DataManager
		string[] keys = new string[dc.tratamientosPosibles.Keys.Count];
		dc.tratamientosPosibles.Keys.CopyTo(keys, 0);
		// Itera para dibujar el nombre de cada examens
		for(int i = index; i < keys.Length && !acabo; i++){
			//Itera en la lista de tratamientos hasta que se acaban los labels diponebles en el panel
			if(exIndex < tratamientos.Length){
				GameObject toggle = tratamientos[exIndex];
				GameObject label = toggle.transform.GetChild(0).GetChild(1).gameObject;
				Text txt = (Text)label.GetComponent(typeof(Text));
				txt.text = keys[i];
				
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
			//Retorna el index en el que se debe empezar a dibujar si faltan tratamientos en la lista
			indxAnteriores[prevIndex] = orIndex;
			return index;
		}
		// Retorna 0 si la lista se termino de dibujar
		return 0;
		
	}
	
	// Limpia los label y los Toggle del panel de seleccion
	public void LimpiarPagina(){
		for(int i = 0; i < tratamientos.Length; i++){
			GameObject toggle = tratamientos[i];
			
			
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
		nextIndex = Mostrartratamientos(nextIndex);
		
		if(nextIndex == 0)
			btnSig.SetActive(false);
	}
	
	// Retrocede en la pagina de seleccion
	public void RetrocederPagina(){
		
		LimpiarPagina();
		
		btnSig.SetActive(true);
		prevIndex--;
		nextIndex = Mostrartratamientos(indxAnteriores[prevIndex]);
		if(prevIndex == 0)
			btnAnt.SetActive(false);
	}
	
	// Se activa al seleccionar un Toggle, y agrega o elimina una seleccion
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
	
	// Abre el panel de confirmacion con el resumen de las selecciones hechas
	public void Seleccionar(){
		// Activa el panel y obtiene los cripts
		panelConfirmacion.SetActive(true);
		GameObject panel = GameObject.Find("txtLista");
		
		Text txt = (Text)panel.GetComponent(typeof(Text));
		string lista = "";
		// Compone todos los tratamientos en una misma cadena
		for(int i = 0; i < seleccionados.Count; i++){
			lista = lista + "\n" + seleccionados[i];
		}
		// Asocia la cadena al label
		txt.text = lista;
	}
	
	// Cierra el panel del confirmacion
	public void Volver(){
		panelConfirmacion.SetActive(false);
	}
	
	// Confirma la seleccion de tratamientos e inicia la muestra de resultados
	public void Confirmar(){
		de.selecciontratamientos = seleccionados;
		panelConfirmacion.SetActive(false);
		GameObject.Find("Clipboard").SetActive(false);
		panelResultados.SetActive(true);
		indxAnteriores = new int[seleccionados.Count];
		prevIndex = 0;
		// Dibuja los resultados seleccionados en el panel
		nextIndex = MostrarResultados(0);
		if(nextIndex != 0)
			btnSigResultados.SetActive(true);
	}
	
	//--------------------------------------------------------------------------------------------------
	// Metodos para el panel de resultados
	//--------------------------------------------------------------------------------------------------
	
	public int MostrarResultados(int index){
		// Variables de control
		bool acabo = false;
		int orIndex = index;
		int exIndex = 0;
		// Itera para dibujar el nombre de cada examens
		for(int i = index; i < seleccionados.Count && !acabo; i++){
			//Itera en la lista de tratamientos hasta que se acaban los labels diponebles en el panel
			if(exIndex < resultados.Length){
				GameObject label = resultados[exIndex];
				Text txt = (Text)label.GetComponent(typeof(Text));
				txt.text = seleccionados[i];
				
				GameObject button = label.transform.GetChild(0).gameObject;
				button.SetActive(true);
				exIndex++;
			}
			else{
				index = i;
				acabo = true;
			}
		}
		
		if(acabo){
			//Retorna el index en el que se debe empezar a dibujar si faltan tratamientos en la lista
			indxAnteriores[prevIndex] = orIndex;
			return index;
		}
		// Retorna 0 si la lista se termino de dibujar
		return 0;
	}
	
	// Limpia los label y los Toggle del panel de seleccion
	public void LimpiarResultados(){
		for(int i = 0; i < resultados.Length; i++){
			
			GameObject label = resultados[i];
			Text txt = (Text)label.GetComponent(typeof(Text));
			txt.text = "";
			
			GameObject button = label.transform.GetChild(0).gameObject;
			button.SetActive(false);
		}
	}
	
	// Avanza en la pagina de resultados
	public void AvanzarResultados(){
		
		LimpiarResultados();
		
		btnAntResultados.SetActive(true);
		prevIndex++;
		nextIndex = MostrarResultados(nextIndex);
		
		if(nextIndex == 0)
			btnSigResultados.SetActive(false);
	}
	
	// Retrocede en la pagina de resultados
	public void RetrocederResultados(){
		
		LimpiarResultados();
		
		btnSigResultados.SetActive(true);
		prevIndex--;
		nextIndex = MostrarResultados(indxAnteriores[prevIndex]);
		if(prevIndex == 0)
			btnAntResultados.SetActive(false);
	}
	
	public void MostrarMedia(GameObject resultado){
		panelMedia.SetActive(true);
		string text = "No result";
		Text label = (Text)resultado.GetComponent(typeof(Text));
		dc.tratamientosPosibles.TryGetValue(label.text,out text);
		string[] parse = text.Split(':');
		Debug.Log(parse[1]);
		
		
		if(parse[1].Equals("text")){
			GameObject panelTexto = panelMedia.transform.GetChild(1).gameObject;
			panelTexto.SetActive(true);
			panelTexto = panelTexto.transform.GetChild(0).gameObject;
			label = (Text)panelTexto.GetComponent(typeof(Text));
			Debug.Log(parse[0]);
			label.text = parse[0];
		}
		
		else if(parse[1].Equals("img")){
			GameObject spriteImagen = panelMedia.transform.GetChild(3).gameObject;
			spriteImagen.SetActive(true);
			Image render = (Image)spriteImagen.GetComponent(typeof(Image));
			render.sprite = (Sprite)Resources.Load("Imagenes/" + parse[0], typeof(Sprite));
		}
		
		else if(parse[1].Equals("aud")){
			GameObject botonAudio = panelMedia.transform.GetChild(2).gameObject;
			botonAudio.SetActive(true);
			audioActual = parse[0];
		}
		else
			Debug.Log("Default");
	}
	
	public void CorrerAudioActual(){
		AudioSource audio = (AudioSource)this.gameObject.GetComponent(typeof(AudioSource));
		audio.clip = (AudioClip)Resources.Load("Audio/" + audioActual, typeof(AudioClip));
		audio.Play();
	}
	
	public void AvanzarModulo(){
		mm.CambiarModulo("ModuloImpresionDiagnostica");
	}
}
