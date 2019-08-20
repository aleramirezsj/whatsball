using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NivelDeJuego
{
    
    public int velocidadActualPelotas=1;
    public int cantidadTotalPelotas=2;
    public float tamanioPelota=1;
    public String jugadorActual;
    public bool iniciarInmediatamente=false;
    public int tiempoDeColor=1;
    public int tiempoDeInicio=1;
    public bool continuarRebotes=false;
    public int cantidadResaltadas=1;

    public NivelDeJuego(int nivel){
        int[] arrayTamanioPelota={6,6,6,4,4,4,2,2,2,2}; 
        int[] arrayCantidadPelotas={3,4,5,5,6,6,7,8,9,10}; 
        int[] arrayVelocidadPelotas={4,4,5,5,6,6,7,7,8,8}; 
        int[] arrayTiempoColor={4,4,4,4,4,3,3,3,2,2}; 
        int[] arrayCantidadResaltadas={1,1,2,2,2,3,3,3,4,5};
        bool[] arrayContinuarRebotes={false,false,false,false,false,false,false,false,false,true};



        tamanioPelota=arrayTamanioPelota[nivel-1];
        cantidadTotalPelotas=arrayCantidadPelotas[nivel-1];
        velocidadActualPelotas=arrayVelocidadPelotas[nivel-1];
        tiempoDeColor=arrayTiempoColor[nivel-1];
        cantidadResaltadas=arrayCantidadResaltadas[nivel-1];
        continuarRebotes=arrayContinuarRebotes[nivel-1];
        tiempoDeInicio=tiempoDeColor;
        iniciarInmediatamente=false;

    }

}
