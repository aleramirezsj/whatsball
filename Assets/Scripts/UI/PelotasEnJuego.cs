using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PelotasEnJuego : MonoBehaviour {

	public Text TxtCantidadPelotas;
	public Text TxtCantidadResaltadas;
	private int cantidadPelotasActual;
	public Slider sldCantidadPelotas;
	public Slider sldCantidadResaltadas;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (sldCantidadPelotas.value!=cantidadPelotasActual)
		{
			cantidadPelotasActual=(int)sldCantidadPelotas.value;
			TxtCantidadPelotas.text=cantidadPelotasActual.ToString();

			if(ComportamientoPelota.cantidadResaltadas == cantidadPelotasActual) {
				//Restamos 1 a la propiedad estatica 'cantidadResaltadas'
				ComportamientoPelota.cantidadResaltadas--;
				//Asignamos la propiedad estatica 'cantidadResaltadas' al slider.
				sldCantidadResaltadas.value = ComportamientoPelota.cantidadResaltadas;
				//Asignamos el valor del la propiedad al texto.
				TxtCantidadResaltadas.text = ComportamientoPelota.cantidadResaltadas.ToString();
			}
		}
	}

}
