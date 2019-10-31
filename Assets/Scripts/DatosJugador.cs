using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DatosJugador  {

	public string nombre;
	public int nivelActual;
	public Dictionary<int,NivelDeJuego> niveles=new Dictionary<int,NivelDeJuego>();
	public Dictionary<int,DatosRendimientos> rendimientosNiveles=new Dictionary<int, DatosRendimientos>();

	public DatosJugador(string nombreJugador){
		nombre=nombreJugador;
		nivelActual=1;
		//Creamos los 10 niveles de juego para el jugador
		for(int i=1;i<11;i++){
			niveles.Add(i,new NivelDeJuego(i));
			rendimientosNiveles.Add(i, new DatosRendimientos());
		}
		Debug.Log("se creo el usuario "+nombreJugador+" DatosRendimientos:"+rendimientosNiveles.Count.ToString());
	}


	public NivelDeJuego obtenerNivelDeJuego(){
		return niveles[nivelActual];
	}

	public void definirNivelDeJuego(int nivelSeleccionado){
		nivelActual=nivelSeleccionado;
	}

	public DatosRendimientos obtenerRendimientos(){
		return rendimientosNiveles[nivelActual];
	}
}
