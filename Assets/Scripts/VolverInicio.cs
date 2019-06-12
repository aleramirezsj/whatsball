using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class VolverInicio : MonoBehaviour {
	
    public Slider sldTamanioPelota;
	public Slider sldVelocidadPelotas;
	public Slider sldCantidadPelotas;
	public Slider sldCantidadResaltadas;
	public Slider sldTiempoDeColor;
	public Slider sldTiempoDeInicio;
	public TextMeshProUGUI infNombreJugador;
	public Component lstJugadores;
	

	public void VolverGuardando(string nombreEscena)
	{
		SceneManager.LoadScene(nombreEscena);
		PlayerPrefs.SetString("nombreJugador", infNombreJugador.text);
		PlayerPrefs.SetInt("cantidadTotalPelotas", (int)sldCantidadPelotas.value);
		PlayerPrefs.SetInt("cantidadResaltadas",(int)sldCantidadResaltadas.value);
		PlayerPrefs.SetFloat("escalaActualPelota", sldTamanioPelota.value);
		PlayerPrefs.SetInt("velocidadPelotasActual", (int)sldVelocidadPelotas.value);
		//PlayerPrefs.SetInt("iniciarInmediatamente",iniciaInmediatamente.isOn?1:0);
		PlayerPrefs.SetInt("tiempoDeColor",(int)sldTiempoDeColor.value);
		PlayerPrefs.SetInt("tiempoDeInicio",(int)sldTiempoDeInicio.value);
		//PlayerPrefs.SetInt("chkContinuarRebotes",chkContinuarRebotes.isOn?1:0);

        
		 //prepara elementos para guardar en disco las configuraciones actuales del juego para poder recuperarlas en la próxima ejecución
		BinaryFormatter bf= new BinaryFormatter();
		FileStream archivo=File.Open(Application.persistentDataPath+"/DatosJuego.dat",FileMode.OpenOrCreate);

		ParametrosJuego parametros=new ParametrosJuego();	
		parametros.cantidadTotalPelotas=(int)sldCantidadPelotas.value;	
		parametros.cantidadResaltadas=(int)sldCantidadResaltadas.value;
		parametros.tamanioActualPelota=sldTamanioPelota.value;
		parametros.velocidadActualPelotas=(int)sldVelocidadPelotas.value;
		parametros.jugadorActual=infNombreJugador.text;
		//parametros.jugadores.Add(infNombreJugador.text);
		//parametros.iniciarInmediatamente=iniciaInmediatamente.isOn;
		parametros.tiempoDeColor=(int)sldTiempoDeColor.value;
		parametros.tiempoDeInicio=(int)sldTiempoDeInicio.value;
		//parametros.continuarRebotes=chkContinuarRebotes.isOn;
		bf.Serialize(archivo,parametros);
		archivo.Close();
	}

    public void VolverSinGuardar(string nombreEscena)
	{
		SceneManager.LoadScene(nombreEscena);
		
	}
	void Start () {
		//si existe el archivo con la configuración del juego lo recupera y setea todas las configuraciones de la pantalla con los valores
		//recuperados		
		if (File.Exists(Application.persistentDataPath+"/DatosJuego.dat")){
			BinaryFormatter bf= new BinaryFormatter();
			FileStream archivo=File.Open(Application.persistentDataPath+"/DatosJuego.dat",FileMode.OpenOrCreate);	
			ParametrosJuego parametros= (ParametrosJuego)bf.Deserialize(archivo);
			archivo.Close();
			sldTamanioPelota.value=parametros.tamanioActualPelota;
			sldCantidadPelotas.value=parametros.cantidadTotalPelotas;
			sldCantidadResaltadas.value=parametros.cantidadResaltadas;
			Debug.Log("Resaltadas:"+parametros.cantidadResaltadas);
			sldVelocidadPelotas.value=parametros.velocidadActualPelotas;	
			infNombreJugador.text=parametros.jugadorActual;	
			//iniciaInmediatamente.isOn=parametros.iniciarInmediatamente;
			sldTiempoDeColor.value=parametros.tiempoDeColor;
			sldTiempoDeInicio.value=parametros.tiempoDeInicio;
			//chkContinuarRebotes.isOn=parametros.continuarRebotes;
		}
	}

	void OnDisable()
	{
		

	}
}
