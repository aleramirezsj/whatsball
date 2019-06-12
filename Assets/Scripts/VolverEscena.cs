using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class VolverEscena : MonoBehaviour {
	
	public GameObject pelota;

	public void CambiarEscenaA(string nombreEscena)
	{
		SceneManager.LoadScene(nombreEscena);
		ComportamientoPelota.juegoIniciado=false;
		ComportamientoPelota.finalizarJuego=false;
		ComportamientoPelota.cantidadEncontradas=0;
		ComportamientoPelota.instancias=0;
		ComportamientoPelota.pelotasEnElJuego.Clear();
		ComportamientoPelota.esNecesarioVolver=false;

	}

	void Start () {
		Debug.Log("inicio de volverEscena");
	}

	void OnDisable()
	{
		

	}
}
