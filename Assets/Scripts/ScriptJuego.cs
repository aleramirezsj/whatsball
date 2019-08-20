﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class ScriptJuego : MonoBehaviour {

	// Use this for initialization
	public DatosJuego datosJuego;
	public Text lblNivel;
	public Rigidbody2D rbBall;
	private int cantidadTotalPelotas;
	private float tamanioActualPelota;
	private int velocidadPelotasActual;
	public static int tiempoDeColor;
	public static int tiempoDeInicio;
	public static int instancias;
	public Text lblJugador;
	public Text txtTiempoDeInicio;
	public static Color colorOriginal;
	int segundos=0;
	float contadorDeSegundos=0;
	float medidaDeTiempo=1;
	bool iniciarInmediatamente;
	static public bool finalizarJuego;
	bool continuarRebotes;
	static public int cantidadResaltadas;
	static public int cantidadEncontradas;
	public GameObject pelota;
	public static List<GameObject> pelotasEnElJuego=new List<GameObject>();
	public static bool esNecesarioVolver=false;
	public static bool juegoIniciado= false;

	void Start () {
		if (File.Exists(Application.persistentDataPath+"/DatosWhatsBall.dat")){
			BinaryFormatter bf= new BinaryFormatter();
			FileStream archivo=File.Open(Application.persistentDataPath+"/DatosWhatsBall.dat",FileMode.OpenOrCreate);	
			datosJuego= (DatosJuego)bf.Deserialize(archivo);
			archivo.Close();
			//coloco los valores recuperados en la pantalla
			lblJugador.text=datosJuego.jugadorActual.nombre;	
			lblNivel.text="Nivel "+datosJuego.jugadorActual.nivel.ToString();
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
	}
	 void OnEnable()
	{

		//Debug.Log("llega el valor "+PlayerPrefs.GetInt("iniciarInmediatamente").ToString());

	}	
	// Update is called once per frame
	void FixedUpdate () {
		if (!juegoIniciado && !esNecesarioVolver)
		{
			if(Input.GetMouseButtonDown(0)||iniciarInmediatamente)
			{
				
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
				lblJugador.enabled=false;
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
			pelotaPresionada.GetComponent<Renderer>().material.color=Color.green;			
			cantidadEncontradas++;
			//Debug.Log("Encontradas:"+cantidadEncontradas.ToString());
			//Debug.Log("Cantidad a resaltar:"+cantidadResaltadas.ToString());
			if (cantidadEncontradas==cantidadResaltadas)
			{
				foreach(GameObject pelo in pelotasEnElJuego){
					pelo.GetComponent<Rigidbody2D>().velocity=new Vector2(0,0);
				}
				txtTiempoDeInicio.enabled=true;
				lblJugador.enabled=true;
				juegoIniciado=false;
				finalizarJuego=true;
				esNecesarioVolver=true;
			}
				
		}
    }

	private Vector3 obtenerPosicionAleatoria()
    {
		float x=UnityEngine.Random.Range(-7,7);
		float y=UnityEngine.Random.Range(-4,4);
		Vector3 posicionAleatoria = new Vector3(x,y,transform.position.z);    
		return posicionAleatoria;
	}
}
