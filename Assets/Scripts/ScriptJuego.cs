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
	public Text lblModo;
	public Text lblJugador;
	public Text txtTiempoDeInicio;
	public GameObject pelota;
	public Rigidbody2D rbBall;
	public Text lblToqueParaContinuar;
	public Text lblSigaLasRojas;
	// fin propiedades de pantalla
	public static DatosJuego datosJuego;
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
	public static NivelDeJuego nivelDeJuego;
	public static DatosRendimientos datosRendimientos;
	public static List<float> tiemposRegistrados=new List<float>();
	public static List<int> totalErroresRegistrados=new List<int>();
	public static int erroresRegistrados=0;
	public static float tiempoRegistrado=0;
	public static bool establecerColorOriginal;
	public static bool detenerMovimiento;
	public Text lblResumen;
	public Text lblTiempoTotal;
	public Text lblTiempoPromedio;
	public Text lblTotalErrores;
	public Text lblPromedioErrores;
	public Text lblIteracion;
	public Text lblRendimientoAlmacenado;
	public static bool primeraEjecucion=true;

	private static float totalTiempo;
	private static int totalErrores;
	private static List<Vector3> posicionesPelotas=new List<Vector3>();
	public static float anchoPelota;
	public static float altoPelota;
	public Image imagenFondoCancha;
	private Vector2 velocityPelota;

	void Start () {
		Debug.Log("ejecutando Start");
		lblRendimientoAlmacenado.enabled=false;
		tiemposRegistrados.Clear();
		lblIteracion.text=(tiemposRegistrados.Count+1).ToString();
		activarDesactivarResumen(false);
		
		//Apagamos la etiqueta Toque para continuar
		lblSigaLasRojas.enabled=false;
		lblToqueParaContinuar.enabled=false;

		
		if (File.Exists(Application.persistentDataPath+"/DatosWhatsBall.dat")){
			BinaryFormatter bf= new BinaryFormatter();
			FileStream archivo=File.Open(Application.persistentDataPath+"/DatosWhatsBall.dat",FileMode.OpenOrCreate);	
			datosJuego= (DatosJuego)bf.Deserialize(archivo);
			archivo.Close();
			//coloco los valores recuperados en la pantalla
			lblJugador.text=datosJuego.jugadorActual.nombre;	
			lblNivel.text="Nivel "+datosJuego.jugadorActual.nivelActual.ToString();
			lblModo.text="Modo "+datosJuego.jugadorActual.modoActual.ToString();
			imagenFondoCancha.sprite=JuegoHelper.obtenerFondo(datosJuego.jugadorActual.deporteActual);
			pelota.GetComponent<SpriteRenderer>().sprite=JuegoHelper.obtenerPelota(datosJuego.jugadorActual.deporteActual);	
		}
		//Debug.Log("se recupero el archivo rendimientos almacenados= "+datosJuego.jugadorActual.rendimientosNiveles.Count.ToString());
		datosRendimientos=datosJuego.jugadorActual.obtenerRendimientos();		
		Debug.Log("Rendimientos="+datosRendimientos.rendimientos.Count.ToString());
		nivelDeJuego=datosJuego.jugadorActual.obtenerNivelDeJuego();
		
		cantidadTotalPelotas=nivelDeJuego.cantidadTotalPelotas;
		tamanioActualPelota=nivelDeJuego.tamanioPelota;
		velocidadPelotasActual=nivelDeJuego.velocidadActualPelotas;
		iniciarInmediatamente=nivelDeJuego.iniciarInmediatamente;
		tiempoDeColor=nivelDeJuego.tiempoDeColor;
		tiempoDeInicio=nivelDeJuego.tiempoDeInicio;
		continuarRebotes=nivelDeJuego.continuarRebotes;
		cantidadResaltadas=nivelDeJuego.cantidadResaltadas;
		if(primeraEjecucion){
			creacionDePelotas();
			primeraEjecucion=false;
		}
		

	}
	void activarDesactivarResumen(bool value){
		lblResumen.enabled=value;
		lblTiempoTotal.enabled=value;
		lblTiempoPromedio.enabled=value;
		lblTotalErrores.enabled=value;
		lblPromedioErrores.enabled=value;
		totalTiempo=0;
		totalErrores=0;
		if(tiemposRegistrados.Count>0){
			foreach(float valor in tiemposRegistrados){
				totalTiempo+=valor;
			}
		}
		if(totalErroresRegistrados.Count>0){
			foreach(int valor in totalErroresRegistrados){
				totalErrores+=valor;
			}
		}
		lblTiempoTotal.text="Tiempo total:"+string.Format("{000:00.00}", totalTiempo);
		lblTiempoPromedio.text="Tiempo promedio:"+string.Format("{000:00.00}", totalTiempo/10);
		lblTotalErrores.text="Total errores:"+totalErrores.ToString();
		lblPromedioErrores.text="Promedio errores:"+(totalErrores/10).ToString();

	}
	 void OnEnable()
	{

	}	
	// Update is called once per frame
	void FixedUpdate () {
		//Debug.Log("se recupero el archivo rendimientos almacenados= "+datosJuego.jugadorActual.rendimientosNiveles.Count.ToString());
		if (!juegoIniciado && !esNecesarioVolver)
		{
			//detecto la pulsación del mouse que inicia el juego y le doy a cada una de las pelotas
			//direcciones aleatorias 
			segundos=0;
			if(Input.GetMouseButtonDown(0))
			{
				lblRendimientoAlmacenado.enabled=false;
				lblToqueParaContinuar.enabled=false;
				lblSigaLasRojas.enabled=false;
				if(tiemposRegistrados.Count==10){
					activarDesactivarResumen(false);
					tiemposRegistrados.Clear();
					totalErroresRegistrados.Clear();
				}
				lblIteracion.text=(tiemposRegistrados.Count+1).ToString();
				tiempoRegistrado=0;
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
					int velocidadXAleatoria=UnityEngine.Random.Range(1,velocidadPelotasActual*2);	
					int velocidadYAleatoria=velocidadPelotasActual-velocidadXAleatoria;
					pelo.GetComponent<Rigidbody2D>().velocity=new Vector2(velocidadXAleatoria*multiX,velocidadYAleatoria*multiY);
					
				}				
				juegoIniciado=true;
				esNecesarioVolver=true;
			}
		}
		if(juegoIniciado)
		{
			#region registración del tiempo
			contadorDeSegundos += Time.deltaTime;
			if (contadorDeSegundos >= medidaDeTiempo){
				contadorDeSegundos=0;
				segundos++;
			}
			#endregion
			
			if(segundos==tiempoDeColor){
				establecerColorOriginal=true;
				establecerColorOriginalAPelotas();
			}

			if(tiempoDeInicio+tiempoDeColor==segundos-1){
				detenerMovimiento=true;
				detenerMovimientoAPelotas();
			}
			if(velocityPelota!=GetComponent<Rigidbody2D>().velocity){
				float x=GetComponent<Rigidbody2D>().velocity.x;
				float xAnterior=velocityPelota.x;
				if(x<xAnterior){
					if(GetComponent<Rigidbody2D>().angularVelocity<100)
						GetComponent<Rigidbody2D>().AddTorque(50);
				}
				if(x>xAnterior){
					if(GetComponent<Rigidbody2D>().angularVelocity>-100)
						GetComponent<Rigidbody2D>().AddTorque(-50);	
				}			
				Debug.Log("y="+GetComponent<Rigidbody2D>().velocity.y.ToString()+"  x="
							+GetComponent<Rigidbody2D>().velocity.x.ToString());
				velocityPelota=GetComponent<Rigidbody2D>().velocity;
			}
			
		}

		if((tiempoDeInicio+tiempoDeColor>=segundos)&&(tiempoRegistrado==0))
			txtTiempoDeInicio.text=(tiempoDeInicio+tiempoDeColor-segundos).ToString();
		else
		{
			txtTiempoDeInicio.text=string.Format("{000:00.00}", tiempoRegistrado);

		}

	}

	//este método intentará almacenar el rendimiento actual, en realidad  la clase DatosRendimientos tomará la decisión de almacenarlo si es uno de los 10 mejores tiempos del jugador para el nivel actual
	void almacenarElRendimientoActual(){
		Debug.Log("ejecutando almacenamiento Actual");
		
		Rendimiento rendimiento=new Rendimiento();
		rendimiento.tiempo=totalTiempo;
		rendimiento.errores=totalErrores;
		//datosRendimientos.rendimientos.Add(rendimiento);
		if(datosRendimientos.AlmacenarSiCorresponde(rendimiento)){
			//si me devolvió verdadero significa que es un rendimiento que se almacenó, por eso lo guardo en la estructura de objetos que almacena todos los datos del juego*/
			datosJuego.jugadorActual.rendimientosNiveles[(int)datosJuego.jugadorActual.modoActual][datosJuego.jugadorActual.nivelActual]=datosRendimientos;
			lblRendimientoAlmacenado.enabled=true;
			
		}
		Debug.Log("Rendimientos="+datosRendimientos.rendimientos.Count.ToString());
	}
	void OnDisable(){
		primeraEjecucion=true;
		instancias=0;
		juegoIniciado=false;
		esNecesarioVolver=false;
		cantidadEncontradas=0;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream archivo = File.Open(Application.persistentDataPath + "/DatosWhatsBall.dat", FileMode.OpenOrCreate);
        bf.Serialize(archivo, datosJuego);
        archivo.Close();		
	}
	void OnMouseDown ()
    {
		GameObject pelotaPresionada=gameObject.GetComponent<ScriptJuego>().pelota;
		if (pelotaPresionada.tag=="Resaltada"&& juegoIniciado && segundos>tiempoDeColor+tiempoDeInicio && pelotaPresionada.GetComponent<Renderer>().material.color!=Color.red)
		{
			pelotaPresionada.GetComponent<Renderer>().material.color=Color.red;			
			cantidadEncontradas++;
			if (cantidadEncontradas==cantidadResaltadas)
			{
				foreach(GameObject pelo in pelotasEnElJuego){
					pelo.GetComponent<Rigidbody2D>().velocity=new Vector2(0,0);
				}
				tiempoRegistrado=((segundos-1)-(tiempoDeInicio+tiempoDeColor))+contadorDeSegundos;
				tiemposRegistrados.Add(tiempoRegistrado);
				totalErroresRegistrados.Add(erroresRegistrados);
				if (tiemposRegistrados.Count==10){
					activarDesactivarResumen(true);
					//Debug.Log(datosJuego.jugadorActual.rendimientos.Count.ToString());
					almacenarElRendimientoActual();
				}
				erroresRegistrados=0;
				txtTiempoDeInicio.enabled=true;
				lblToqueParaContinuar.enabled=true;
				lblSigaLasRojas.enabled=true;
				lblJugador.enabled=true;
				juegoIniciado=false;
				//finalizarJuego=true;
				esNecesarioVolver=false;
				cantidadEncontradas=0;
				instancias=0;
				segundos=0;
				contadorDeSegundos=0;
				reubicarLasPelotasActuales();	
			}
				
		}else if(juegoIniciado && segundos>=tiempoDeColor+tiempoDeInicio&&pelotaPresionada.tag!="Resaltada"){
			pelotaPresionada.GetComponent<Renderer>().material.color=Color.gray;
			erroresRegistrados++;	
		}
    }

	//agrego estos métodos para que las pelotas tengan un comportamiento más uniforme, dado que cada pelota tiene asociado este script, las ejecuciones no se realizaban de una manera bien sincronizada, con estos métodos logro que una sola de las pelotas pueda afectar al resto, apagandolas, reubicándolas, cambiandoles su color
	#region Metodos que afectan a todas las pelotas
	void reubicarLasPelotasActuales(){
		foreach(GameObject pelotaEnPantalla in pelotasEnElJuego){
			pelotaEnPantalla.transform.position=obtenerPosicionAleatoria();
			if(pelotaEnPantalla.GetComponent<Renderer>().material.color==Color.gray)
				pelotaEnPantalla.GetComponent<Renderer>().material.color=colorOriginal;
		}
	}

	void establecerColorOriginalAPelotas(){
		establecerColorOriginal=false;
		foreach(GameObject pelotaEnPantalla in pelotasEnElJuego){
			if(pelotaEnPantalla.GetComponent<Renderer>().material.color!=colorOriginal)
				pelotaEnPantalla.GetComponent<Renderer>().material.color=colorOriginal;
		}		
	}
	void detenerMovimientoAPelotas(){
		detenerMovimiento=false;
		txtTiempoDeInicio.enabled=false;
		foreach(GameObject pelotaEnPantalla in pelotasEnElJuego){
			
			//lblJugador.enabled=false;
			if(continuarRebotes==false){
				pelotaEnPantalla.GetComponent<Rigidbody2D>().velocity=new Vector2(0,0);
				pelotaEnPantalla.GetComponent<Rigidbody2D>().angularVelocity = 0;
			}
		}		
	}
	#endregion

	private Vector3 obtenerPosicionAleatoria()
    {
		Vector3 posicionAleatoria;
		do{
			float x=UnityEngine.Random.Range(-7,7);
			float y=UnityEngine.Random.Range(-4,4);
			posicionAleatoria = new Vector3(x,y,transform.position.z);    
		}while(EncontrarPosicionSimilar(posicionAleatoria));
		posicionesPelotas.Add(posicionAleatoria);
		return posicionAleatoria;
	}

	private bool EncontrarPosicionSimilar(Vector3 posicionABuscar){
		bool retorno=false;
		foreach(Vector3 vector in posicionesPelotas){
			if(Mathf.Abs(vector.x-posicionABuscar.x)<anchoPelota/4&&
			Mathf.Abs(vector.y-posicionABuscar.y)<altoPelota/4){
				retorno=true;
			}
		}
		Debug.Log(retorno);
		return retorno;
	}
	private void creacionDePelotas()
	{
			
		//almaceno el color original en la propiedad estática y la pelota original en la lista de pelotas
		if(instancias==0){
			Debug.Log("creación de las pelotas");
			pelotasEnElJuego.Clear();	
			posicionesPelotas.Clear();		
			colorOriginal=GetComponent<Renderer>().material.color;
			pelotasEnElJuego.Add(pelota);
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
			CalcularAnchoYAltoPelota();		
		}
		
	}

	private void CalcularAnchoYAltoPelota(){
		var p1 = pelota.gameObject.transform.TransformPoint(0, 0, 0);
		var p2 = pelota.gameObject.transform.TransformPoint(1, 1, 0);
		anchoPelota = p2.x - p1.x;
		altoPelota = p2.y - p1.y;
	}
}
