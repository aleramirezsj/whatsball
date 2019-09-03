using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class ScriptJuego : MonoBehaviour {

	// Propiedades vinculada con elementos de la pantalla
	public Text lblNivel;
	public Text lblJugador;
	public Text txtTiempoDeInicio;
	public GameObject pelota;
	public Rigidbody2D rbBall;
	public Text lblToqueParaContinuar;
	// fin propiedades de pantalla
	public DatosJuego datosJuego;
	private int cantidadTotalPelotas;
	private float tamanioActualPelota;
	private int velocidadPelotasActual;
	public static int tiempoDeColor;
	public static int tiempoDeInicio;
	public static int instancias;	
	public static Color colorOriginal;
	int segundos=0;
	float contadorDeSegundos=0;
	float medidaDeTiempo=1;
	bool iniciarInmediatamente;
	static public bool finalizarJuego;
	bool continuarRebotes;
	static public int cantidadResaltadas;
	static public int cantidadEncontradas;
	public static List<GameObject> pelotasEnElJuego=new List<GameObject>();
	public static bool esNecesarioVolver=false;
	public static bool juegoIniciado= false;


	void Start () {
		//Apagamos la etiqueta Toque para continuar
		lblToqueParaContinuar.enabled=false;
		//almaceno el color original en la propiedad estática y la pelota original en la lista de pelotas
		if(instancias==0){
			colorOriginal=GetComponent<Renderer>().material.color;
			pelotasEnElJuego.Add(pelota);
		}
		
		if (File.Exists(Application.persistentDataPath+"/DatosWhatsBall.dat")){
			BinaryFormatter bf= new BinaryFormatter();
			FileStream archivo=File.Open(Application.persistentDataPath+"/DatosWhatsBall.dat",FileMode.OpenOrCreate);	
			datosJuego= (DatosJuego)bf.Deserialize(archivo);
			archivo.Close();
			//coloco los valores recuperados en la pantalla
			lblJugador.text=datosJuego.jugadorActual.nombre;	
			lblNivel.text="Nivel "+datosJuego.jugadorActual.nivelActual.ToString();
		}
		NivelDeJuego nivelDeJuego=datosJuego.jugadorActual.obtenerNivelDeJuego();
		Debug.Log("cantidad de pelotas Nivel obtenido="+nivelDeJuego.cantidadTotalPelotas.ToString());
		cantidadTotalPelotas=nivelDeJuego.cantidadTotalPelotas;
		tamanioActualPelota=nivelDeJuego.tamanioPelota;
		velocidadPelotasActual=nivelDeJuego.velocidadActualPelotas;
		iniciarInmediatamente=nivelDeJuego.iniciarInmediatamente;
		tiempoDeColor=nivelDeJuego.tiempoDeColor;
		tiempoDeInicio=nivelDeJuego.tiempoDeInicio;
		continuarRebotes=nivelDeJuego.continuarRebotes;
		cantidadResaltadas=nivelDeJuego.cantidadResaltadas;

		//Construimos todas las pelotas
		//Creamos las pelotas comunes y resaltadas
	/* 	GameObject pelotaInstanciada;
		//transform.position=Camera.main.ScreenToWorldPoint(posicionAleatoria);
		transform.position=obtenerPosicionAleatoria();
		transform.localScale=new Vector3(tamanioActualPelota/2,tamanioActualPelota/2,tamanioActualPelota/2);
		if(instancias<cantidadTotalPelotas-1){
			for(int i=0;i<cantidadTotalPelotas-1;i++){
				pelotaInstanciada=Instantiate(pelota, obtenerPosicionAleatoria(), transform.rotation) as GameObject;
				instancias++;
				if(cantidadTotalPelotas-instancias<=cantidadResaltadas){
					pelotaInstanciada.tag="Resaltada";
					pelotaInstanciada.GetComponent<Renderer>().material.color = Color.red;
				}
				pelotasEnElJuego.Add(pelotaInstanciada);
			}
		}*/
		creacionDePelotas();


	}
	 void OnEnable()
	{

		//Debug.Log("llega el valor "+PlayerPrefs.GetInt("iniciarInmediatamente").ToString());

	}	
	// Update is called once per frame
	void FixedUpdate () {
		if (!juegoIniciado && !esNecesarioVolver)
		{
			if(Input.GetMouseButtonDown(0))
			{
				lblToqueParaContinuar.enabled=false;

				txtTiempoDeInicio.enabled=true;
				foreach(GameObject pelo in pelotasEnElJuego){
					int multiX=UnityEngine.Random.Range(1,3);
					if (multiX==2)
						multiX=-1;
					int multiY=UnityEngine.Random.Range(1,3);
					if(multiY==2)
						multiY=-1;
					//saco el valor 0 y el valor máximo como posibles resultados porque el movimiento de la pelota no tendría ninguna 
					//inclinación (el valor máximo siempre es excluido de los resultados en Random.Range)
					int velocidadXAleatoria=UnityEngine.Random.Range(1,velocidadPelotasActual);	
					int velocidadYAleatoria=velocidadPelotasActual-velocidadXAleatoria;
					pelo.GetComponent<Rigidbody2D>().velocity=new Vector2(velocidadXAleatoria*multiX,velocidadYAleatoria*multiY);
				}				
				juegoIniciado=true;
				esNecesarioVolver=true;
			}
		}
		if(juegoIniciado)
		{
			contadorDeSegundos += Time.deltaTime;
			if (contadorDeSegundos >= medidaDeTiempo){
				contadorDeSegundos=0;
				segundos++;
				
			}
			if(segundos==tiempoDeColor){
				GameObject pelotaActual=gameObject.GetComponent<ScriptJuego>().pelota;
				pelotaActual.GetComponent<Renderer>().material.color=colorOriginal;

			}
			if(tiempoDeInicio+tiempoDeColor==segundos-1){
				txtTiempoDeInicio.enabled=false;
				//lblJugador.enabled=false;
				if(continuarRebotes==false)
					rbBall.velocity=new Vector2(0,0);
			}
		}

		if(tiempoDeInicio+tiempoDeColor>=segundos)
			txtTiempoDeInicio.text=(tiempoDeInicio+tiempoDeColor-segundos).ToString();
		else
		{
			string lcCero=(contadorDeSegundos*100)<10?"0":"";
			txtTiempoDeInicio.text=((segundos-1)-(tiempoDeInicio+tiempoDeColor)).ToString()+":"+lcCero+((int)(contadorDeSegundos*100)).ToString();

		}
		//Debug.Log(GetComponent<Renderer>().material.color);
	}

	void OnMouseDown ()
    {
		GameObject pelotaPresionada=gameObject.GetComponent<ScriptJuego>().pelota;
		if (pelotaPresionada.tag=="Resaltada"&& juegoIniciado && segundos>=tiempoDeColor+tiempoDeInicio)
		{
        	//rbBall.velocity=new Vector2(0,0);
			pelotaPresionada.GetComponent<Renderer>().material.color=Color.red;			
			cantidadEncontradas++;
			//Debug.Log("Encontradas:"+cantidadEncontradas.ToString());
			//Debug.Log("Cantidad a resaltar:"+cantidadResaltadas.ToString());
			if (cantidadEncontradas==cantidadResaltadas)
			{
				foreach(GameObject pelo in pelotasEnElJuego){
					pelo.GetComponent<Rigidbody2D>().velocity=new Vector2(0,0);
				}
				txtTiempoDeInicio.enabled=true;
				lblToqueParaContinuar.enabled=true;
				lblJugador.enabled=true;
				juegoIniciado=false;
				finalizarJuego=true;
				esNecesarioVolver=false;
				instancias=0;
				//pelota=null;
				reiniciarPelotas();
				segundos=0;
			}
				
		}else if(juegoIniciado && segundos>=tiempoDeColor+tiempoDeInicio){
			pelotaPresionada.GetComponent<Renderer>().material.color=Color.gray;	
		}
    }

	private Vector3 obtenerPosicionAleatoria()
    {
		float x=UnityEngine.Random.Range(-7,7);
		float y=UnityEngine.Random.Range(-4,4);
		Vector3 posicionAleatoria = new Vector3(x,y,transform.position.z);    
		return posicionAleatoria;
	}

	private void creacionDePelotas()
	{
	 
	/*
		//almaceno el color original en la propiedad estática y la pelota original en la lista de pelotas
		if(instancias==0){
			colorOriginal=GetComponent<Renderer>().material.color;
			pelotasEnElJuego.Add(pelota);
		}*/
		//Creamos las pelotas comunes y resaltadas
		GameObject pelotaInstanciada;
		//transform.position=Camera.main.ScreenToWorldPoint(posicionAleatoria);
		transform.position=obtenerPosicionAleatoria();
		transform.localScale=new Vector3(tamanioActualPelota/2,tamanioActualPelota/2,tamanioActualPelota/2);
		if(instancias<cantidadTotalPelotas-1){
			for(int i=0;i<cantidadTotalPelotas-1;i++){
				pelotaInstanciada=Instantiate(pelota, obtenerPosicionAleatoria(), transform.rotation) as GameObject;
				instancias++;
				if(cantidadTotalPelotas-instancias<=cantidadResaltadas){
					pelotaInstanciada.tag="Resaltada";
					pelotaInstanciada.GetComponent<Renderer>().material.color = Color.red;
				}
				pelotasEnElJuego.Add(pelotaInstanciada);
			}
		}
	}
	 private void reiniciarPelotas()
	{
		foreach(GameObject pelo in pelotasEnElJuego)
		{
		pelo.transform.position=obtenerPosicionAleatoria();
		}
	}
}
