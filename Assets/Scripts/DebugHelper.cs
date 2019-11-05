
using UnityEngine;

public static class DebugHelper{
    internal static void ImprimirCantidadDeRendimientosDelJugadorActual(DatosJuego datosJuego){
        DatosRendimientos dr=datosJuego.jugadorActual.rendimientosNiveles[datosJuego.jugadorActual.nivelActual];
		Debug.Log("rendimientos del jugador actual="+dr.rendimientos.Count.ToString());
    }

    internal static void ImprimirElJugadorActual(DatosJuego datosJuego){
        Debug.Log("datosJuego.jugadorActual.nombre="+datosJuego.jugadorActual.nombre);
        Debug.Log("datosJuego.jugadorActual.nivelActual="+datosJuego.jugadorActual.nivelActual.ToString());
    }
}