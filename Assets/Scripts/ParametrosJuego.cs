using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ParametrosJuego
{
    public List<string> jugadores = new List<string>();
    public int velocidadActualPelotas=1;
    public int cantidadTotalPelotas=2;
    public float tamanioActualPelota=1;
    public String jugadorActual;
    public bool iniciarInmediatamente=false;
    public int tiempoDeColor=1;
    public int tiempoDeInicio=1;
    public bool continuarRebotes=false;
    public int cantidadResaltadas=1;
}
