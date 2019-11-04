using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


public static class DatosJuegoHelper
{
    internal static DatosJuego obtenerDesdeArchivo()
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
}