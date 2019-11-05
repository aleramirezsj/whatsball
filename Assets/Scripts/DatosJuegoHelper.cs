using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


using TMPro;
using UnityEngine.UI;

public static class DatosJuegoHelper
{
    internal static DatosJuego ObtenerDesdeArchivo()
    {
        if (File.Exists(Application.persistentDataPath+"/DatosWhatsBall.dat")){
			Debug.Log("si encontró el archivo");
			BinaryFormatter bf= new BinaryFormatter();
			FileStream archivo=File.Open(Application.persistentDataPath+"/DatosWhatsBall.dat",FileMode.OpenOrCreate);
			DatosJuego datosJuego= (DatosJuego)bf.Deserialize(archivo);
			archivo.Close();
            return datosJuego;
        }else{
            return null;
        }	
		
    }
    internal static void BorrarArchivoParaInicializarElJuego(){
        File.Delete(Application.persistentDataPath+"/DatosWhatsBall.dat");
    }

    internal static void almacenarArchivoDeDatos(DatosJuego datosJuego, string nombreJugador){
    //ALTERNATIVAS QUE CONTEMPLA EL SIGUIENTE CÓDIGO
    //1) Que no haya encontrado el archivo y por lo tanto el objeto datosJuego se igual a nulo
    //	1.1)Además si no se definió un nombre de jugador crea un Usuario Random
    // crea una nueva estructura de Juego para almacenar en el archivo
    //2) Si el archivo de datos fue encontrado
    //	 crea o recupera el jugador pasado por parametros
    if (nombreJugador.Trim().Length==0)
        nombreJugador="Usuario"+(int)Random.Range(1,1000);
    if (datosJuego==null){
        datosJuego=new DatosJuego(nombreJugador);
    }else{
       
        datosJuego.recuperarJugador(nombreJugador);

    }
    BinaryFormatter bf= new BinaryFormatter();
    FileStream archivo=File.Open(Application.persistentDataPath+"/DatosWhatsBall.dat",FileMode.OpenOrCreate);	
    bf.Serialize(archivo,datosJuego);
    archivo.Close();
}
internal static void almacenarNuevoJugador(DatosJuego datosJuego, string nombreJugador, ModosEnum modo){
    //ALTERNATIVAS QUE CONTEMPLA EL SIGUIENTE CÓDIGO
    //1) Que no haya encontrado el archivo y por lo tanto el objeto datosJuego sea igual a nulo
    //	1.1)Además si no se definió un nombre de jugador crea un Usuario Random
    //  con los escrito en nombreJugador se crea una nueva estructura de Juego para almacenar en el 
    //  archivo
    //2) Si el archivo de datos fue encontrado
    //	2.1) si no está seleccionado ningún jugador en el Drop Down
    //		2.1.1) si no se definió un nombre de jugador crea un Usuario Random y lo coloca en 
    //             TxtNombreJugador, con esa info crea un Jugador nuevo
    if (nombreJugador.Trim().Length==0)
        nombreJugador="Usuario"+(int)Random.Range(1,1000);    
    if (datosJuego==null){
        //Debug.Log("CReando la instancia"+txtNombreJugador.text.Trim().Length.ToString());
        datosJuego=new DatosJuego(nombreJugador,modo);
    }else{
        datosJuego.crearJugador(nombreJugador,modo);
    }

    BinaryFormatter bf= new BinaryFormatter();
    FileStream archivo=File.Open(Application.persistentDataPath+"/DatosWhatsBall.dat",FileMode.OpenOrCreate);	
    bf.Serialize(archivo,datosJuego);
    archivo.Close();
}

}