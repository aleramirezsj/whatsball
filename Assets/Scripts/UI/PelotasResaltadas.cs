using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PelotasResaltadas : MonoBehaviour {

	public Text TxtCantidadResaltadas;
	public Slider sldCantidadResaltadas;
	public Slider sldCantidadPelotas;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//definimos que la actualización de la cantidad de resaltadas se realice solo cuando sea menor a la cantidad de pelotas definidas
		if (sldCantidadResaltadas.value!=ComportamientoPelota.cantidadResaltadas && sldCantidadPelotas.value>sldCantidadResaltadas.value)
		{
			//asignamos la cantidad de pelotas resaltadas que tiene el slider a la propiedad estática "cantidadResaltadas" de la clase
			//ComportamientoPelota
			ComportamientoPelota.cantidadResaltadas=(int)sldCantidadResaltadas.value;
			//actualizamos el número de pelotas resaltadas en la pantalla
			TxtCantidadResaltadas.text=ComportamientoPelota.cantidadResaltadas.ToString();
        }
	}

	

}
