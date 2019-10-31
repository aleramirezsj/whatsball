using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DatosRendimientos  {
    public  List<Rendimiento> rendimientos;
    public DatosRendimientos(){
        rendimientos=new List<Rendimiento>();
    }

    //este método recibe como parámetro un rendimiento, buscará dentro de los rendimientos que ya tiene almacenados, tomando 2 reglas principales:
    //1) si todavía no hay 10 rendimientos almacenados, directamente lo almacena
    //2) si ya hay 10 rendimientos almacenados se fija si el rendimiento recibido es mejor al peor de los rendimientos almacenados, si es mejor lo almacena.
    public bool AlmacenarSiCorresponde(Rendimiento rendimientoNuevo){
        if(rendimientos.Count<10){
            rendimientos.Add(rendimientoNuevo);
            return true;
        }else{
            Rendimiento peorRendimiento=obtenerPeorRendimiento();
            if ((rendimientoNuevo.tiempo<peorRendimiento.tiempo)||(rendimientoNuevo.tiempo==peorRendimiento.tiempo&&rendimientoNuevo.errores<peorRendimiento.errores)){
                //borro el rendimiento menor
                rendimientos.Remove(peorRendimiento);
                rendimientos.Add(rendimientoNuevo);
                return true;
            }else{
                return false;
            }
        }
    }
    //obtengo el rendimiento con mayor tiempo y si hay 2 rendimientos con el mismo tiempo obtengo el que tenga más errores
    private Rendimiento obtenerPeorRendimiento(){
        Rendimiento peorRendimiento=null;
        foreach(Rendimiento rendimiento in rendimientos){
            if(peorRendimiento==null)
                peorRendimiento=rendimiento;
            else{
                if((rendimiento.tiempo>peorRendimiento.tiempo)||(rendimiento.tiempo==peorRendimiento.tiempo&&rendimiento.errores>peorRendimiento.errores))
                    peorRendimiento=rendimiento;
            }

        }
        return peorRendimiento;
    }
}