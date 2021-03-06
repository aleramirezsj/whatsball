﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScriptNuevoJugador : MonoBehaviour {
	public TextMeshProUGUI TxtNombreJugador;
	private DatosJuego datosJuego;
	public Dropdown dropSelectorModo;
	public Dropdown dropSelectorDeporte;
	public Image imagenFondoCancha;
	public Image imagenPelota;

	public void CambiarEscenaA(string nombreEscena)
	{
		  
		SceneManager.LoadScene(nombreEscena);
	}
    public void CrearJugador(){
        DatosJuegoHelper.almacenarNuevoJugador(datosJuego,TxtNombreJugador.text, (ModosEnum)dropSelectorModo.value, (DeportesEnum) dropSelectorDeporte.value);      
        CambiarEscenaA("Home");
    }
	public void salir(){
	    Application.Quit(); 
	}
	void Start () {
		
		 //si existe el archivo con la configuración del juego lo recupera 
		Screen.fullScreen = false;
        datosJuego=DatosJuegoHelper.ObtenerDesdeArchivo();
	}

		void OnDisable()
	{



	}
	// Update is called once per frame
	void Update () 
		{
		//musicPlayer.Play();
		if (Input.GetKeyDown(KeyCode.Escape)) 
			CambiarEscenaA("Home"); 
		}

	public void dropSelectorDeporteChanged(){
		imagenFondoCancha.sprite=JuegoHelper.obtenerFondo((DeportesEnum)dropSelectorDeporte.value);
		imagenPelota.sprite=JuegoHelper.obtenerPelota((DeportesEnum)dropSelectorDeporte.value);

	}




}
