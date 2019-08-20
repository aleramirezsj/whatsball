using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DatosJugador  {

	public string nombre;
	public int nivel;
	public Dictionary<int,NivelDeJuego> niveles=new Dictionary<int,NivelDeJuego>();

	public DatosJugador(string nombreJugador){
		nombre=nombreJugador;
		nivel=1;
		//Creamos los 10 niveles de juego para el jugador
		for(int i=1;i<11;i++){
			niveles.Add(i,new NivelDeJuego(i));
		}
	}

	public NivelDeJuego obtenerNivelDeJuego(){
		return niveles[nivel];
	}

}
