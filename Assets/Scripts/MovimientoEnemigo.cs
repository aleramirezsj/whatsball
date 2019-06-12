using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour {

	public Transform pelota;
	public float velocidad;

	
	// Update is called once per frame
	void Update () {
		if (ComportamientoPelota.juegoIniciado)
		{
			if(pelota.position.y>transform.position.y)
				transform.position=new Vector3(transform.position.x,
										transform.position.y+velocidad * Time.deltaTime,
										transform.position.z);
			if(pelota.position.y<transform.position.y)
				transform.position=new Vector3(transform.position.x,
										transform.position.y-velocidad * Time.deltaTime,
										transform.position.z);
		}
	}
}
