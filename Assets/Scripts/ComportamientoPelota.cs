using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Sprites;
using System;

public class ComportamientoPelota : MonoBehaviour {
	//public static ComportamientoPelota Instance {get; set;}
	public static bool juegoIniciado= false;
	public Rigidbody2D rbBall;
	public Canvas canvas;
	private int cantidadTotalPelotas;
	private float escalaActualPelota;
	private int velocidadPelotasActual;
	public static int tiempoDeColor;
	public static int tiempoDeInicio;
	public static int instancias;
	public Text txtNombreJugador;
	public Text txtJugador;
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
	

    void Start () {
		Screen.fullScreen = false;
		Logs();
		//almaceno el color original en la propiedad estática y la pelota original en la lista de pelotas
		if(instancias==0){
			colorOriginal=GetComponent<Renderer>().material.color;
			pelotasEnElJuego.Add(pelota);
		}
		GameObject pelotaInstanciada;
		//transform.position=Camera.main.ScreenToWorldPoint(posicionAleatoria);
		transform.position=obtenerPosicionAleatoria();
		transform.localScale=new Vector3(escalaActualPelota/2,escalaActualPelota/2,escalaActualPelota/2);
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



    void OnEnable()
	{
		cantidadTotalPelotas=PlayerPrefs.GetInt("cantidadTotalPelotas");
		escalaActualPelota=PlayerPrefs.GetFloat("escalaActualPelota");
		velocidadPelotasActual=PlayerPrefs.GetInt("velocidadPelotasActual")*2;
		txtNombreJugador.text=PlayerPrefs.GetString("nombreJugador");
		iniciarInmediatamente=(PlayerPrefs.GetInt("iniciarInmediatamente"))==1;
		tiempoDeColor=PlayerPrefs.GetInt("tiempoDeColor");
		tiempoDeInicio=PlayerPrefs.GetInt("tiempoDeInicio");
		continuarRebotes=(PlayerPrefs.GetInt("chkContinuarRebotes"))==1;
		cantidadResaltadas=PlayerPrefs.GetInt("cantidadResaltadas");
		//Debug.Log("llega el valor "+PlayerPrefs.GetInt("iniciarInmediatamente").ToString());

	}	
	
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
				GameObject pelotaActual=gameObject.GetComponent<ComportamientoPelota>().pelota;
				pelotaActual.GetComponent<Renderer>().material.color=colorOriginal;

			}
			if(tiempoDeInicio+tiempoDeColor==segundos-1){
				txtTiempoDeInicio.enabled=false;
				txtNombreJugador.enabled=false;
				txtJugador.enabled=false;
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
		GameObject pelotaPresionada=gameObject.GetComponent<ComportamientoPelota>().pelota;
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
				txtNombreJugador.enabled=true;
				txtJugador.enabled=true;
				juegoIniciado=false;
				finalizarJuego=true;
				esNecesarioVolver=true;
			}
				
		}
    }

	void Logs (){
		//Debug.Log("inicio pelota");
		/* Debug.Log("z de la pelota "+transform.position.z);
		//Debug.Log("z de el canvas "+canvasDimensiones.position.z);
		Debug.Log("x de la pelota "+transform.position.x);
		Debug.Log("y de la pelota "+transform.position.y);*/
		//Debug.Log(iniciarInmediatamente.ToString());
		
		//SEGUIMIENTO DE VALORES DE VELOCIDAD
		//Debug.Log("valor en velocidadPelotasActual:"+velocidadPelotasActual.ToString());	
		//Debug.Log("valor en velocidadXAleatoria:"+velocidadXAleatoria.ToString());
		//Debug.Log("valor en velocidadYAleatoria:"+velocidadYAleatoria.ToString());		
		//Debug.Log("valor en x para velocidad:"+pelo.GetComponent<Rigidbody2D>().velocity.x.ToString());
		//Debug.Log("valor en y para velocidad:"+pelo.GetComponent<Rigidbody2D>().velocity.y.ToString());
		//Debug.Log("multiX:"+multiX.ToString());	
		//Debug.Log("multiY:"+multiY.ToString());			
	}

	private Vector3 obtenerPosicionAleatoria()
    {
		float x=UnityEngine.Random.Range(-7,7);
		float y=UnityEngine.Random.Range(-4,4);
		Vector3 posicionAleatoria = new Vector3(x,y,transform.position.z);    
		return posicionAleatoria;
	}


}
