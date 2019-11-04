using System;
using System.Linq;
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
            ordenarLosRendimientos();
            return true;
        }else{
            Rendimiento peorRendimiento=obtenerPeorRendimiento(rendimientos);
            if ((rendimientoNuevo.tiempo<peorRendimiento.tiempo)||(rendimientoNuevo.tiempo==peorRendimiento.tiempo&&rendimientoNuevo.errores<peorRendimiento.errores)){
                //borro el rendimiento menor
                rendimientos.Remove(peorRendimiento);
                rendimientos.Add(rendimientoNuevo);
                ordenarLosRendimientos();
                return true;
            }else{
                return false;
            }
        }
    }
    //obtengo el peor rendimiento, es decir, con mayor tiempo y si hay 2 rendimientos con el mismo tiempo obtengo el que tenga más errores
    private Rendimiento obtenerPeorRendimiento(List<Rendimiento> rendimientosAnalizados){
        Rendimiento peorRendimiento=null;
        foreach(Rendimiento rendimiento in rendimientosAnalizados){
            if(peorRendimiento==null)
                peorRendimiento=rendimiento;
            else{
                if((rendimiento.tiempo>peorRendimiento.tiempo)||(rendimiento.tiempo==peorRendimiento.tiempo&&rendimiento.errores>peorRendimiento.errores))
                    peorRendimiento=rendimiento;
            }

        }
        return peorRendimiento;
    }

    private Rendimiento obtenerMejorRendimiento(List<Rendimiento> rendimientosAnalizados){
        Rendimiento mejorRendimiento=null;
        foreach(Rendimiento rendimiento in rendimientosAnalizados){
            if(mejorRendimiento==null)
                mejorRendimiento=rendimiento;
            else{
                if((rendimiento.tiempo<mejorRendimiento.tiempo)||(rendimiento.tiempo==mejorRendimiento.tiempo&&rendimiento.errores<mejorRendimiento.errores))
                    mejorRendimiento=rendimiento;
            }

        }
        return mejorRendimiento;
    }

    public string obtenerLosRendimientos(){
        string retorno="";
        int indice=1;
        foreach(Rendimiento rendimiento in rendimientos){
            retorno+=indice.ToString()+") Tiempo="+rendimiento.tiempo.ToString()+"  Errores="+rendimiento.errores.ToString()+"\n";
            
            indice++;
        }
        Debug.Log("retorno="+retorno);
        return retorno;
    }

    public void ordenarLosRendimientos(){
        var copiaRendimientos =(rendimientos as IEnumerable<Rendimiento>).ToList();
        List<Rendimiento> rendimientosOrdenados=new List<Rendimiento>();
        while(copiaRendimientos.Count>0){
            Rendimiento mejorRendimiento=obtenerMejorRendimiento(copiaRendimientos);
            rendimientosOrdenados.Add(mejorRendimiento);
            copiaRendimientos.Remove(mejorRendimiento);
        }
        rendimientos=rendimientosOrdenados;
        
    }
}