using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ScriptHome : MonoBehaviour {

	public Text lblNivel;
	public Text lblModo;
	public Dropdown dropSelectorJugador;
	private DatosJuego datosJuego;
	private int indexJugadorSeleccionado;
	public Button BtnJugar;
	public Button BtnConfiguracion;
	public Button BtnEstadisticas;
	

	public void CambiarEscenaA(string nombreEscena)
	{
		DatosJuegoHelper.almacenarArchivoDeDatos(datosJuego, dropSelectorJugador.captionText.text.ToString());
		SceneManager.LoadScene(nombreEscena);
	}

	public void salir(){
	    Application.Quit(); 
	}
	void Start () {
		Debug.Log("se está ejecutando el Start");
		//DatosJuegoHelper.BorrarArchivoParaInicializarElJuego();
		 //si existe el archivo con la configuración del juego lo recupera y setea todas las configuraciones de la pantalla con los valores recuperados		
		Screen.fullScreen = false;
		datosJuego=DatosJuegoHelper.ObtenerDesdeArchivo();
		if(datosJuego!=null){
			lblNivel.text="Nivel "+datosJuego.jugadorActual.nivelActual.ToString();
			lblModo.text="Modo "+datosJuego.jugadorActual.modoActual.ToString();
			CargarDropDownJugadores();
		}else{
			DesactivarActivarControles(false);
	
			Debug.Log("No encontró el archivo");
		}
		
	}
	private void DesactivarActivarControles(bool value){
		BtnJugar.interactable=value;
		BtnConfiguracion.interactable=value;
		BtnEstadisticas.interactable=value;
		
	}
	private void CargarDropDownJugadores(){
		int indexActual=1;
		//dropSelectorJugador.ClearOptions();
		if(datosJuego.jugadores.Count>0&&dropSelectorJugador.options.Count==0){
			dropSelectorJugador.options.Add(new Dropdown.OptionData());
			foreach(KeyValuePair<string,DatosJugador> jugador in datosJuego.jugadores){	
				dropSelectorJugador.options.Add(new Dropdown.OptionData(jugador.Key));
				if(jugador.Key.ToUpper()==datosJuego.jugadorActual.nombre.ToUpper()){
					this.indexJugadorSeleccionado=indexActual;
				}
				indexActual++;
			}
			dropSelectorJugador.value=this.indexJugadorSeleccionado;
		}
	}



	void OnDisable()
	{


	}

	void Update(){
		//musicPlayer.Play();
		if (Input.GetKeyDown(KeyCode.Escape)) 
			Application.Quit(); 
	}

	public void dropSelectorJugadorChanged(){
		if(dropSelectorJugador.captionText.text.Trim().Length!=0){
			DesactivarActivarControles(true);
			datosJuego.recuperarJugador(dropSelectorJugador.captionText.text);
			lblNivel.text="Nivel "+datosJuego.jugadorActual.nivelActual.ToString();

		}else{
			//BtnJugar.enabled=false;
			DesactivarActivarControles(false);
			//txtNombreJugador
		}
	}
}
