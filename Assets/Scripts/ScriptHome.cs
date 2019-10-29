using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ScriptHome : MonoBehaviour {
	/*public Text txtTamanioPelota;
	public Text txtVelocidadPelotas;
	public Text txtCantidadPelotas;
	public Text txtCantidadResaltadas;
	public Text txtTiempoDeColor;
	public Text txtTiempoDeInicio;
	
	public Toggle ChkIniciarInmediatamente;
	public Component lstJugadores;
	public Toggle chkContinuarRebotes;
	public GameObject pelota;*/
	public TextMeshProUGUI txtNombreJugador;
	public Text lblNivel;
	private DatosJuego datosJuego;
	

	public void CambiarEscenaA(string nombreEscena)
	{
		SceneManager.LoadScene(nombreEscena);

		/* //prepara elementos para guardar en disco las configuraciones actuales del juego para poder recuperarlas en la próxima ejecución
		BinaryFormatter bf= new BinaryFormatter();
		FileStream archivo=File.Open(Application.persistentDataPath+"/DatosJuego.dat",FileMode.OpenOrCreate);

		ParametrosJuego parametros=new ParametrosJuego();	
		parametros.cantidadTotalPelotas=(int)sldCantidadPelotas.value;	
		parametros.cantidadResaltadas=(int)sldCantidadResaltadas.value;
		parametros.tamanioActualPelota=sldTamanioPelota.value;
		parametros.velocidadActualPelotas=(int)sldVelocidadPelotas.value;
		parametros.jugadorActual=infNombreJugador.text;
		//parametros.jugadores.Add(infNombreJugador.text);
		parametros.iniciarInmediatamente=iniciaInmediatamente.isOn;
		parametros.tiempoDeColor=(int)sldTiempoDeColor.value;
		parametros.tiempoDeInicio=(int)sldTiempoDeInicio.value;
		parametros.continuarRebotes=chkContinuarRebotes.isOn;
		bf.Serialize(archivo,parametros);
		archivo.Close();*/
	}

	public void salir(){
	    Application.Quit(); 
	}
	void Start () {
		// File.Delete(Application.persistentDataPath+"/DatosWhatsBall.dat");
		 //si existe el archivo con la configuración del juego lo recupera y setea todas las configuraciones de la pantalla con los valores recuperados		
		//txtVelocidadPelotas.text=Application.persistentDataPath.ToString();
		Screen.fullScreen = false;
		if (File.Exists(Application.persistentDataPath+"/DatosWhatsBall.dat")){
			Debug.Log("si encontró el archivo");
			BinaryFormatter bf= new BinaryFormatter();
			FileStream archivo=File.Open(Application.persistentDataPath+"/DatosWhatsBall.dat",FileMode.OpenOrCreate);
			datosJuego= (DatosJuego)bf.Deserialize(archivo);
			//datosJuego = new DatosJuego("hacha");
			archivo.Close();
			//coloco los valores recuperados en la pantalla
			txtNombreJugador.text=datosJuego.jugadorActual.nombre;	
			lblNivel.text="Nivel "+datosJuego.jugadorActual.nivelActual.ToString();
			
			//dropSelectorNivel.value=datosJuego.jugadorActual.nivelActual-1;
			//Debug.Log("Encontró el archivo "+datosJuego.jugadores.Count);
			/* txtTamanioPelota.text=parametros.tamanioActualPelota.ToString();
			txtCantidadPelotas.text=parametros.cantidadTotalPelotas.ToString();
			txtCantidadResaltadas.text=parametros.cantidadResaltadas.ToString();
			Debug.Log("Resaltadas:"+parametros.cantidadResaltadas);
			txtVelocidadPelotas.text=parametros.velocidadActualPelotas.ToString();	
			lblNombreJugador.text=parametros.jugadorActual;	
			ChkIniciarInmediatamente.isOn=parametros.iniciarInmediatamente;
			txtTiempoDeColor.text=parametros.tiempoDeColor.ToString();
			txtTiempoDeInicio.text=(parametros.tiempoDeInicio+parametros.tiempoDeColor).ToString();
			chkContinuarRebotes.isOn=parametros.continuarRebotes;
			pelota.transform.localScale=new Vector3(parametros.tamanioActualPelota/2,parametros.tamanioActualPelota/2,parametros.tamanioActualPelota/2);
			//txtVelocidadPelotas.text="SI";*/
		}else{
			Debug.Log("No encontró el archivo");
			/*txtTamanioPelota.text="5";
			txtCantidadResaltadas.text="1";
			txtCantidadPelotas.text="5";
			txtVelocidadPelotas.text="5";
			txtTiempoDeInicio.text="5";
			txtTiempoDeColor.text="5";

			PlayerPrefs.SetString("nombreJugador","Jugador");
			PlayerPrefs.SetInt("cantidadTotalPelotas", 5);
			PlayerPrefs.SetInt("cantidadResaltadas",1);
			PlayerPrefs.SetFloat("escalaActualPelota",5);
			PlayerPrefs.SetInt("velocidadPelotasActual", 5);
			PlayerPrefs.SetInt("tiempoDeColor",5);
			PlayerPrefs.SetInt("tiempoDeInicio",5);*/
		}
		
	}
	

	void OnDisable()
	{
		Debug.Log(datosJuego);
		if (datosJuego==null){
			Debug.Log("CReando la instancia");
			datosJuego=new DatosJuego(txtNombreJugador.text);
		}else{
			Debug.Log("Creando el jugador");
			if(datosJuego.jugadorActual.nombre.ToUpper()!=txtNombreJugador.text.ToUpper()){
				datosJuego.recuperarOCrearJugador(txtNombreJugador.text);
			}
		}
		BinaryFormatter bf= new BinaryFormatter();
		FileStream archivo=File.Open(Application.persistentDataPath+"/DatosWhatsBall.dat",FileMode.OpenOrCreate);	
		bf.Serialize(archivo,datosJuego);
		archivo.Close();
					
		PlayerPrefs.SetString("nombreJugador",txtNombreJugador.text);
		PlayerPrefs.SetInt("nivelActual",datosJuego.jugadorActual.nivelActual);

	}
	 void Update(){
		//musicPlayer.Play();
		if (Input.GetKeyDown(KeyCode.Escape)) 
			Application.Quit(); 
		}
}
