using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CambiarVelocidadPelotas : MonoBehaviour {

	public Text txtVelocidadPelotas;
	private int velocidadPelotasActual;
	public Slider sldVelocidadPelotas;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (sldVelocidadPelotas.value!=velocidadPelotasActual)
		{
			velocidadPelotasActual=(int)sldVelocidadPelotas.value;
			txtVelocidadPelotas.text=velocidadPelotasActual.ToString();
		}		
	}
	
	

}
