using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ScriptHome : MonoBehaviour {

	public TextMeshProUGUI txtNombreJugador;
	public Text lblNivel;

	public Dropdown dropSelectorJugador;
	private DatosJuego datosJuego;
	private int indexJugadorSeleccionado;
	

	public void CambiarEscenaA(string nombreEscena)
	{
		almacenarArchivoDeDatos();
		SceneManager.LoadScene(nombreEscena);
	}

	public void salir(){
	    Application.Quit(); 
	}
	void Start () {
		txtNombreJugador.enabled=false;
		//File.Delete(Application.persistentDataPath+"/DatosWhatsBall.dat");
		 //si existe el archivo con la configuración del juego lo recupera y setea todas las configuraciones de la pantalla con los valores recuperados		
		Screen.fullScreen = false;
		if (File.Exists(Application.persistentDataPath+"/DatosWhatsBall.dat")){
			Debug.Log("si encontró el archivo");
			BinaryFormatter bf= new BinaryFormatter();
			FileStream archivo=File.Open(Application.persistentDataPath+"/DatosWhatsBall.dat",FileMode.OpenOrCreate);
			datosJuego= (DatosJuego)bf.Deserialize(archivo);
			archivo.Close();	
			lblNivel.text="Nivel "+datosJuego.jugadorActual.nivelActual.ToString();
			int indexActual=1;
			dropSelectorJugador.ClearOptions();
			dropSelectorJugador.options.Add(new Dropdown.OptionData());
			if(datosJuego.jugadores.Count>0){
				foreach(KeyValuePair<string,DatosJugador> jugador in datosJuego.jugadores){	
					dropSelectorJugador.options.Add(new Dropdown.OptionData(jugador.Key));
					if(jugador.Key==datosJuego.jugadorActual.nombre.ToUpper())
						this.indexJugadorSeleccionado=indexActual;
					indexActual++;
				}
				dropSelectorJugador.value=this.indexJugadorSeleccionado;
				
			}

		}else{
			txtNombreJugador.enabled=true;
			Debug.Log("No encontró el archivo");
		}
		
	}

	void OnEnable(){
		dropSelectorJugador.value=this.indexJugadorSeleccionado;
	}

	void OnDisable()
	{


	}
	void almacenarArchivoDeDatos(){
		//ALTERNATIVAS QUE CONTEMPLA EL SIGUIENTE CÓDIGO
		//1) Que no haya encontrado el archivo y por lo tanto el objeto datosJuego se igual a nulo
		//	1.1)Además si no se definió un nombre de jugador crea un Usuario Random
		//  con los escrito en TxtNombreJugador se crea una nueva estructura de Juego para almacenar en el 
		//  archivo
		//2) Si el archivo de datos fue encontrado
		//	2.1) si no está seleccionado ningún jugador en el Drop Down
		//		2.1.1) si no se definió un nombre de jugador crea un Usuario Random y lo coloca en 
		//             TxtNombreJugador, con esa info crea un Jugador nuevo
		if (datosJuego==null){
			//Debug.Log("CReando la instancia"+txtNombreJugador.text.Trim().Length.ToString());
			if(txtNombreJugador.text.Trim().Length==0)
				txtNombreJugador.text="Usuario"+(int)Random.Range(1,1000);
			datosJuego=new DatosJuego(txtNombreJugador.text);
		}else{
			Debug.Log("Recuperando o creando al jugador");
			Debug.Log("JUGADOR SELECCIONADO"+dropSelectorJugador.captionText.text.Trim().Length.ToString());
			if (dropSelectorJugador.captionText.text.Trim().Length==0){
				if(txtNombreJugador.text.Trim().Length==0)
					txtNombreJugador.text="Usuario"+(int)Random.Range(1,1000);
				datosJuego.recuperarOCrearJugador(txtNombreJugador.text);
			}
		}
		BinaryFormatter bf= new BinaryFormatter();
		FileStream archivo=File.Open(Application.persistentDataPath+"/DatosWhatsBall.dat",FileMode.OpenOrCreate);	
		bf.Serialize(archivo,datosJuego);
		archivo.Close();
	}
	void Update(){
		//musicPlayer.Play();
		if (Input.GetKeyDown(KeyCode.Escape)) 
			Application.Quit(); 
	}

	public void dropSelectorJugadorChanged(){
		if(dropSelectorJugador.captionText.text.Trim().Length!=0){
			datosJuego.recuperarOCrearJugador(dropSelectorJugador.captionText.text);
			lblNivel.text="Nivel "+datosJuego.jugadorActual.nivelActual.ToString();
			txtNombreJugador.enabled=false;
			txtNombreJugador.text="";
		}else{
			txtNombreJugador.enabled=true;
			//txtNombreJugador
		}
	}
}
