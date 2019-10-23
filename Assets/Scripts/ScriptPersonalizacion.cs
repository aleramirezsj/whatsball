using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScriptPersonalizacion : MonoBehaviour
{
    public GameObject pelota;
    public Text lblJugador;
    private DatosJuego datosJuego;
    public Slider sldVelocidadPelotas;
    public Text txtVelocidadPelotas; 
    public Slider sldTiempoDeInicio;
	public Text TxtTiempoDeInicio;
	public Text TxtTiempoDeColor;
    public Slider sldTiempoDeColor;
    public Slider sldCantidadPelotas;
    public Text txtCantidadPelotas;
    public Slider sldCantidadResaltadas;
    public Text txtCantidadResaltadas;
    public Slider sldTamanioPelota;
    public Text txtTamanioPelota;
    public Dropdown dropSelectorNivel;
    public Button btnGuardarVolver;

    

    public void GuardarSeteos(){
        //Obtengo en nivel actual para saber en que posición debe almacenarse la config actual
        int nivelActualJugador=datosJuego.jugadorActual.nivelActual;   
        //construyo un objeto nivel de juego     
        NivelDeJuego nivelDeJuego=new NivelDeJuego(nivelActualJugador);
        //leo cada uno de los seteos en pantalla y los almaceno en las propiedades del objeto creado
        nivelDeJuego.cantidadResaltadas=(int)sldCantidadResaltadas.value;
        nivelDeJuego.cantidadTotalPelotas=(int)sldCantidadPelotas.value;
        nivelDeJuego.tamanioPelota=(int)sldTamanioPelota.value;
        nivelDeJuego.tiempoDeColor=(int)sldTiempoDeColor.value;
        nivelDeJuego.tiempoDeInicio=(int)sldTiempoDeInicio.value;
        nivelDeJuego.velocidadActualPelotas=(int)sldVelocidadPelotas.value;
        //almaceno el objeto creado en el diccionario de niveles que tiene el jugador actual
        datosJuego.jugadorActual.niveles[nivelActualJugador]=nivelDeJuego;
    }
    public void CambiarEscenaA(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);

    }
    public void salir()
    {
        Application.Quit();
    }
    void Start()
    {
        //si existe el archivo con la configuración del juego lo recupera y setea todas las configuraciones de la pantalla con los valores recuperados		
        Screen.fullScreen = false;
        if (File.Exists(Application.persistentDataPath + "/DatosWhatsBall.dat"))
        {
            Debug.Log("si encontró el archivo");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream archivo = File.Open(Application.persistentDataPath + "/DatosWhatsBall.dat", FileMode.OpenOrCreate);
            datosJuego = (DatosJuego)bf.Deserialize(archivo);

            //datosJuego = new DatosJuego("hacha");
            archivo.Close();


            recuperarSeteosJugador();
        }
        else
        {
            Debug.Log("No encontró el archivo");
        }
    }

    void OnDisable()
    {
        Debug.Log(datosJuego);
        /*if (datosJuego==null){
			Debug.Log("CReando la instancia");
			datosJuego=new DatosJuego(lblJugador.text);
		}else{
			Debug.Log("Creando el jugador");
			if(datosJuego.jugadorActual.nombre.ToUpper()!=lblJugador.text.ToUpper()){
				datosJuego.recuperarOCrearJugador(lblJugador.text);
			}
		}*/

        BinaryFormatter bf = new BinaryFormatter();
        FileStream archivo = File.Open(Application.persistentDataPath + "/DatosWhatsBall.dat", FileMode.OpenOrCreate);
        bf.Serialize(archivo, datosJuego);
        archivo.Close();


        PlayerPrefs.SetString("nombreJugador", lblJugador.text);
        PlayerPrefs.SetInt("nivelActual", datosJuego.jugadorActual.nivelActual);



    }
    // Update is called once per frame
    void Update()
    {
        //musicPlayer.Play();
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public void dropSelectorNivelChanged()
    {
        int nivelSeleccionado = dropSelectorNivel.value + 1;
        datosJuego.jugadorActual.definirNivelDeJuego(nivelSeleccionado);
        recuperarSeteosJugador();

    }

	public void sliderTiempoDeInicioChanged(){
        
        //tiempoDeColor=(int)sldTiempoDeColor.value;
        TxtTiempoDeInicio.text = sldTiempoDeInicio.value.ToString();
	}

    public void sliderTiempoDeColorChanged()
    {

        TxtTiempoDeColor.text= sldTiempoDeColor.value.ToString();
        //TxtTiempoDeInicio.text = sldTiempoDeInicio.value.ToString();
    }
    public void sliderCantidadPelotasChanged()
    {

        txtCantidadPelotas.text= sldCantidadPelotas.value.ToString();
        //TxtTiempoDeInicio.text = sldTiempoDeInicio.value.ToString();      
    }
    public void sliderTamanioPelotaChanged(){
        txtTamanioPelota.text=sldTamanioPelota.value.ToString();
        pelota.transform.localScale=new Vector3(sldTamanioPelota.value/2,sldTamanioPelota.value/2,sldTamanioPelota.value/2);  
    }

    public void sliderVelocidadPelotasChanged(){
        txtVelocidadPelotas.text=sldVelocidadPelotas.value.ToString();
    }
    public void sliderCantidadResaltadasChanged(){
        txtCantidadResaltadas.text=sldCantidadResaltadas.value.ToString();
    }
    void recuperarSeteosJugador()
    {
        //coloco los valores recuperados en la pantalla
        lblJugador.text = datosJuego.jugadorActual.nombre;
        // coloco los parametros recuperados en cada lugar que le corresponde
        NivelDeJuego nivelDeJuego = datosJuego.jugadorActual.obtenerNivelDeJuego();

        //TxtVelocidadPelotas.text = nivelDeJuego.velocidadActualPelotas.ToString();
		sldVelocidadPelotas.value = nivelDeJuego.velocidadActualPelotas;
        //TxtCantidadPelotas.text = nivelDeJuego.cantidadTotalPelotas.ToString();
        sldCantidadPelotas.value = nivelDeJuego.cantidadTotalPelotas;
        //TxtCantidadResaltadas.text = nivelDeJuego.cantidadResaltadas.ToString();
        sldCantidadResaltadas.value = nivelDeJuego.cantidadResaltadas;
        //TxtTamanioPelota.text = nivelDeJuego.tamanioPelota.ToString();
        sldTamanioPelota.value = nivelDeJuego.tamanioPelota;
        //TxtTiempoDeColor.text = nivelDeJuego.tiempoDeColor.ToString();
        sldTiempoDeColor.value = nivelDeJuego.tiempoDeColor;
        //TxtTiempoDeInicio.text = nivelDeJuego.tiempoDeInicio.ToString();
        sldTiempoDeInicio.value = nivelDeJuego.tiempoDeInicio;
        dropSelectorNivel.value = datosJuego.jugadorActual.nivelActual - 1;
    }
}
