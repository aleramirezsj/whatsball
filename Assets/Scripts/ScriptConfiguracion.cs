﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScriptConfiguracion : MonoBehaviour {
	public Text lblJugador;
	public Text lblNivel;
	private DatosJuego datosJuego;
	public Text TxtVelocidadPelotas;
	public Text TxtTiempoDeInicio;
	public Text TxtTiempoDeColor;
	public Text TxtCantidadPelotas;
	public Text TxtCantidadResaltadas;
	public Text TxtTamanioPelota;
	public Dropdown dropSelectorNivel;

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
		if (File.Exists(Application.persistentDataPath+"/DatosWhatsBall.dat")){
			Debug.Log("si encontró el archivo");
			BinaryFormatter bf= new BinaryFormatter();
			FileStream archivo=File.Open(Application.persistentDataPath+"/DatosWhatsBall.dat",FileMode.OpenOrCreate);
			datosJuego= (DatosJuego)bf.Deserialize(archivo);

			//datosJuego = new DatosJuego("hacha");
			archivo.Close();
			
			
			recuperarSeteosJugador();
		}else{
			Debug.Log("No encontró el archivo");
		}
	}

		void OnDisable()
	{
		Debug.Log(datosJuego);
		/*if (datosJuego==null){
			Debug.Log("CReando la instancia");
			datosJuego=new DatosJuego(lblJugador.text);
		}else{
			Debug.Log("Creando el jugador");
			if(datosJuego.jugadorActual.nombre.ToUpper()!=lblJugador.text.ToUpper()){
				datosJuego.recuperarOCrearJugador(lblJugador.text);
			}
		}*/

		BinaryFormatter bf= new BinaryFormatter();
		FileStream archivo=File.Open(Application.persistentDataPath+"/DatosWhatsBall.dat",FileMode.OpenOrCreate);	
		bf.Serialize(archivo,datosJuego);
		archivo.Close();
		
		
		PlayerPrefs.SetString("nombreJugador",lblJugador.text);
		PlayerPrefs.SetInt("nivelActual",datosJuego.jugadorActual.nivelActual);
		


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

	void recuperarSeteosJugador(){
		//coloco los valores recuperados en la pantalla
		lblJugador.text=datosJuego.jugadorActual.nombre;	
		lblNivel.text="Nivel "+datosJuego.jugadorActual.nivelActual.ToString();
		// coloco los parametros recuperados en cada lugar que le corresponde
		NivelDeJuego nivelDeJuego = datosJuego.jugadorActual.obtenerNivelDeJuego();

		TxtVelocidadPelotas.text = nivelDeJuego.velocidadActualPelotas.ToString();
		TxtCantidadPelotas.text = nivelDeJuego.cantidadTotalPelotas.ToString();
		TxtCantidadResaltadas.text = nivelDeJuego.cantidadResaltadas.ToString();
		TxtTamanioPelota.text = nivelDeJuego.tamanioPelota.ToString();
		TxtTiempoDeColor.text = nivelDeJuego.tiempoDeColor.ToString();
		TxtTiempoDeInicio.text = nivelDeJuego.tiempoDeInicio.ToString();
		dropSelectorNivel.value=datosJuego.jugadorActual.nivelActual-1;
	}
}