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

    public NivelDeJuego(int nivel, int modo){
        int[,] arrayTamanioPelota={{6,6,6,5,5,5,4,4,4,4},
                                    {4,4,4,3,3,3,2,2,2,2},
                                    {3,3,3,2,2,2,1,1,1,1}}; 
        int[,] arrayCantidadPelotas={{4,4,4,5,5,5,6,6,6,6},
                                     {4,6,6,7,7,7,8,8,8,8},
                                     {4,7,8,8,8,8,9,9,10,10}}; 
        int[,] arrayVelocidadPelotas={{4,4,4,5,5,5,6,6,6,6},
                                      {6,6,7,7,7,8,8,8,8,8},
                                      {8,8,9,9,9,9,10,10,10,10}}; 
        int[,] arrayTiempoColor={{2,2,2,2,2,2,2,2,2,2},
                                 {3,3,3,3,3,3,3,3,3,3},
                                 {4,4,4,4,4,4,4,4,4,4}}; 
        int[,] arrayCantidadResaltadas={{2,2,2,2,2,3,3,3,3,3},
                                        {2,2,2,3,3,3,3,3,3,4},
                                        {2,3,4,4,4,4,4,4,4,5}};
        bool[,] arrayContinuarRebotes={{false,false,false,false,false,false,false,false,false,true},
                                       {false,false,false,false,false,false,false,false,false,true},
                                       {false,false,false,false,false,false,false,false,false,true}};



        tamanioPelota=arrayTamanioPelota[modo,nivel-1];
        cantidadTotalPelotas=arrayCantidadPelotas[modo,nivel-1];
        velocidadActualPelotas=arrayVelocidadPelotas[modo,nivel-1];
        tiempoDeColor=arrayTiempoColor[modo,nivel-1];
        cantidadResaltadas=arrayCantidadResaltadas[modo,nivel-1];
        continuarRebotes=arrayContinuarRebotes[modo,nivel-1];
        tiempoDeInicio=tiempoDeColor;
        iniciarInmediatamente=false;

    }

    public NivelDeJuego(){

    }

}
