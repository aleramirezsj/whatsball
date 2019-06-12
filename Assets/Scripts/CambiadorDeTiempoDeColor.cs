using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CambiadorDeTiempoDeColor : MonoBehaviour {

	public Text TxtTiempoDeColor;
	public Text TxtTiempoDeInicio;
	private int tiempoDeColor;
	private int tiempoDeInicio;
	public Slider sldTiempoDeColor;
	public Slider sldTiempoDeInicio;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (sldTiempoDeColor.value!=tiempoDeColor)
		{
			tiempoDeColor=(int)sldTiempoDeColor.value;
			tiempoDeInicio=(int)sldTiempoDeInicio.value;
			TxtTiempoDeColor.text=tiempoDeColor.ToString();
			TxtTiempoDeInicio.text=(tiempoDeColor+tiempoDeInicio).ToString();
		}
	}

	

}
