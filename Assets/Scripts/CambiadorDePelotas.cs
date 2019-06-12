using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CambiadorDePelotas : MonoBehaviour {

	public Text TxtCantidadPelotas;
	private int cantidadPelotasActual;
	public Slider sldCantidadPelotas;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (sldCantidadPelotas.value!=cantidadPelotasActual)
		{
			cantidadPelotasActual=(int)sldCantidadPelotas.value;
			TxtCantidadPelotas.text=cantidadPelotasActual.ToString();
		}
	}

	

}
