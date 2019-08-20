using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DatosJuego  {

	public Dictionary<string,DatosJugador> jugadores=new Dictionary<string, DatosJugador>();
	public DatosJugador jugadorActual;
	public DatosJuego (string nombreJugador){
		recuperarOCrearJugador(nombreJugador);

	}
	
	public void recuperarOCrearJugador(string nombreJugador){
		//buscamos en el diccionario si el jugador ya esta almacenado
		if (jugadores.ContainsKey(nombreJugador.ToUpper())){
			//obtenemos los datos almacenados en el diccionario de ese jugador y lo asignamos a la propiedad jugadorActual
			jugadorActual= jugadores[nombreJugador.ToUpper()];
			//Debug.Log("Encontró al jugador= "+jugadorActual.nombre+System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));
		}else{
			//creamos un nuevo jugador y lo asignamos a la propiedad jugadorActual
			jugadorActual= new DatosJugador(nombreJugador);
			//almacenamos al jugador que creamos en el diccionario
			jugadores.Add(nombreJugador.ToUpper(),jugadorActual);
			//Debug.Log("Creo y almacenó el jugador= "+jugadorActual.nombre+System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));

		}
	}

}
