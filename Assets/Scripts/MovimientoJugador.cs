using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//imprimimos en la consola las coordenadas de mouse
		//Debug.Log(Input.mousePosition);
		//creamos un objeto del tipo Vector3 que guarda la información de X, Y, Z y le asignamos el valor de la ubicación del mouse
		//adaptada esa ubicación en las dimensiones de pantalla del juego
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		//ajustamos la posición de la paleta con el objeto transform, al asignarle el mismo valor
		//de x, hacemos que no pueda moverse lateralmente, solo verticalmente
		//y manteniendo z, hacemos que la cámara no tape al objeto
		transform.position= new Vector3(transform.position.x,Mathf.Clamp(mousePos.y,-3.8f,3.8f), transform.position.z);

	}
}
