using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CambiadorDeTiempoDeInicio : MonoBehaviour {

	public Text TxtTiempoDeInicio;
	private int tiempoDeInicio;
	private int tiempoDeColor;
	public Slider sldTiempoDeInicio;
	public Slider sldTiempoDeColor;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (sldTiempoDeInicio.value!=tiempoDeInicio)
		{
			tiempoDeInicio=(int)sldTiempoDeInicio.value;
			tiempoDeColor=(int)sldTiempoDeColor.value;
			TxtTiempoDeInicio.text=(tiempoDeInicio+tiempoDeColor).ToString();
		}
	}

	

}
