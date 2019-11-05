using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScriptConfiguracion : MonoBehaviour {
	public Text lblJugador;
	private DatosJuego datosJuego;
	public Text TxtVelocidadPelotas;
	public Text TxtTiempoDeInicio;
	public Text TxtTiempoDeColor;
	public Text TxtCantidadPelotas;
	public Text TxtCantidadResaltadas;
	public Text TxtTamanioPelota;
	public Dropdown dropSelectorNivel;
	public Dropdown dropSelectorModo;
	public GameObject pelota;

	public void CambiarEscenaA(string nombreEscena)
	{
		SceneManager.LoadScene(nombreEscena);

	}
	public void salir(){
	    Application.Quit(); 
	}
	void Start () {
		 //si existe el archivo con la configuración del juego lo recupera y setea todas las configuraciones de la pantalla con los valores recuperados		
		Screen.fullScreen = false;
		datosJuego=DatosJuegoHelper.ObtenerDesdeArchivo();
		if(datosJuego!=null){
			recuperarSeteosJugador();
			dropSelectorNivel.value=datosJuego.jugadorActual.nivelActual-1;
			dropSelectorModo.value=(int)datosJuego.jugadorActual.modoActual;
		}else{
			Debug.Log("No encontró el archivo");
		}
	}

	void OnDisable()
	{
		BinaryFormatter bf= new BinaryFormatter();
		FileStream archivo=File.Open(Application.persistentDataPath+"/DatosWhatsBall.dat",FileMode.OpenOrCreate);	
		bf.Serialize(archivo,datosJuego);
		archivo.Close();
		
		
	}
	// Update is called once per frame
	void Update () 
		{
		//musicPlayer.Play();
		if (Input.GetKeyDown(KeyCode.Escape)) 
			Application.Quit(); 
		}

	public void dropSelectorNivelChanged (){
			int nivelSeleccionado = dropSelectorNivel.value+1;
			datosJuego.jugadorActual.definirNivelDeJuego(nivelSeleccionado);
			recuperarSeteosJugador();
	}
	public void dropSelectorModoChanged(){
			datosJuego.jugadorActual.CambiarDeModo((ModosEnum)dropSelectorModo.value);
			dropSelectorNivel.value=datosJuego.jugadorActual.nivelActual-1;
			recuperarSeteosJugador();
	}

	void recuperarSeteosJugador(){
		Debug.Log("Ejecutando el método recuperarSeteosJugador");
		DebugHelper.ImprimirElJugadorActual(datosJuego);
		//coloco los valores recuperados en la pantalla
		lblJugador.text=datosJuego.jugadorActual.nombre;	
		// coloco los parametros recuperados en cada lugar que le corresponde
		NivelDeJuego nivelDeJuego = datosJuego.jugadorActual.obtenerNivelDeJuego();

		TxtVelocidadPelotas.text = nivelDeJuego.velocidadActualPelotas.ToString();
		TxtCantidadPelotas.text = nivelDeJuego.cantidadTotalPelotas.ToString();
		TxtCantidadResaltadas.text = nivelDeJuego.cantidadResaltadas.ToString();
		TxtTamanioPelota.text = nivelDeJuego.tamanioPelota.ToString();
		TxtTiempoDeColor.text = nivelDeJuego.tiempoDeColor.ToString();
		TxtTiempoDeInicio.text = nivelDeJuego.tiempoDeInicio.ToString();
		
		/*JOAQUIN*/ pelota.transform.localScale=new Vector3(int.Parse(TxtTamanioPelota.text)/2,int.Parse(TxtTamanioPelota.text)/2,int.Parse(TxtTamanioPelota.text)/2);		
	}
}
