using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DatosJuego  {

	public Dictionary<string,DatosJugador> jugadores=new Dictionary<string, DatosJugador>();
	public DatosJugador jugadorActual;
	public DatosJuego (string nombreJugador){
		recuperarJugador(nombreJugador);

	}
	public DatosJuego (string nombreJugador,ModosEnum modo){
		crearJugador(nombreJugador,modo);

	}
	public void crearJugador(string nombreJugador, ModosEnum modo){
			//creamos un nuevo jugador y lo asignamos a la propiedad jugadorActual
			jugadorActual= new DatosJugador(nombreJugador,modo);
			//almacenamos al jugador que creamos en el diccionario
			jugadores.Add(nombreJugador.ToUpper(),jugadorActual);		
	}
	public void recuperarJugador(string nombreJugador){
		//buscamos en el diccionario si el jugador ya esta almacenado
		if (jugadores.ContainsKey(nombreJugador.ToUpper())){
			//obtenemos los datos almacenados en el diccionario de ese jugador y lo asignamos a la propiedad jugadorActual
			jugadorActual= jugadores[nombreJugador.ToUpper()];
			//Debug.Log("Encontró al jugador= "+jugadorActual.nombre+System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));
		}else{
			Debug.Log("NO SE ENCONTRÓ AL JUGADOR="+nombreJugador);

		}
	}

}
