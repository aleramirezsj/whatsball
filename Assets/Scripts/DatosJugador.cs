using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DatosJugador  {

	public string nombre;
	public int nivelActual;
	public ModosEnum modoActual;
	public Dictionary<int,NivelDeJuego>[] niveles=new Dictionary<int,NivelDeJuego>[]{
		new Dictionary<int,NivelDeJuego>(),
		new Dictionary<int,NivelDeJuego>(),
		new Dictionary<int,NivelDeJuego>()
	};
	public Dictionary<int,DatosRendimientos>[] rendimientosNiveles=new Dictionary<int, DatosRendimientos>[]{
		new Dictionary<int, DatosRendimientos>(),
		new Dictionary<int, DatosRendimientos>(),
		new Dictionary<int, DatosRendimientos>()
	};


	public DatosJugador(string nombreJugador, ModosEnum modo){
		nombre=nombreJugador;
		nivelActual=1;
		modoActual=modo;
		//Creamos los 10 niveles de juego para el jugador
		for(int modoJugador=0;modoJugador<3;modoJugador++){
			for(int i=1;i<11;i++){
				niveles[modoJugador].Add(i,new NivelDeJuego(i,(int)modoJugador));
				rendimientosNiveles[modoJugador].Add(i, new DatosRendimientos());
			}
		}
		Debug.Log("se creo el usuario "+nombreJugador+" DatosRendimientos:"+rendimientosNiveles[(int)modoActual].Count.ToString());
	}	


	public NivelDeJuego obtenerNivelDeJuego(){
		return niveles[(int)modoActual][nivelActual];
	}

	public void definirNivelDeJuego(int nivelSeleccionado){
		nivelActual=nivelSeleccionado;
	}

	public DatosRendimientos obtenerRendimientos(){
		return rendimientosNiveles[(int)modoActual][nivelActual];
	}
	public void CambiarDeModo(ModosEnum modo){
		if(modoActual!=modo){
			modoActual=modo;
			nivelActual=1;	
		}
	}
}
