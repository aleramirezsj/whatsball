using System.Linq;
using System.Collections.Generic;
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

    Dictionary<string, int> dictionarySeteos = new Dictionary<string, int>();

    Dictionary<string, int> dictionaryActual = new Dictionary<string, int>();

    public void GuardarSeteos()
    {
        //Obtengo en nivel actual para saber en que posición debe almacenarse la config actual
        int nivelActualJugador = datosJuego.jugadorActual.nivelActual;
        int modoActualJugador=(int)datosJuego.jugadorActual.modoActual;
        //construyo un objeto nivel de juego     
        NivelDeJuego nivelDeJuego = new NivelDeJuego();
        //leo cada uno de los seteos en pantalla y los almaceno en las propiedades del objeto creado
        nivelDeJuego.cantidadResaltadas = (int)sldCantidadResaltadas.value;
        nivelDeJuego.cantidadTotalPelotas = (int)sldCantidadPelotas.value;
        nivelDeJuego.tamanioPelota = (int)sldTamanioPelota.value;
        nivelDeJuego.tiempoDeColor = (int)sldTiempoDeColor.value;
        nivelDeJuego.tiempoDeInicio = (int)sldTiempoDeInicio.value;
        nivelDeJuego.velocidadActualPelotas = (int)sldVelocidadPelotas.value;
        //almaceno el objeto creado en el diccionario de niveles que tiene el jugador actual
        datosJuego.jugadorActual.niveles[modoActualJugador][nivelActualJugador] = nivelDeJuego;
        btnGuardarVolver.interactable = false;
        recuperarSeteosJugador();
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
            ListenerControls();
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

    public void sliderTiempoDeInicioChanged()
    {

        //tiempoDeColor=(int)sldTiempoDeColor.value;
        TxtTiempoDeInicio.text = sldTiempoDeInicio.value.ToString();
    }

    public void sliderTiempoDeColorChanged()
    {

        TxtTiempoDeColor.text = sldTiempoDeColor.value.ToString();
        //TxtTiempoDeInicio.text = sldTiempoDeInicio.value.ToString();
    }
    public void sliderCantidadPelotasChanged()
    {

        txtCantidadPelotas.text = sldCantidadPelotas.value.ToString();
        //TxtTiempoDeInicio.text = sldTiempoDeInicio.value.ToString();      
    }
    public void sliderTamanioPelotaChanged()
    {
        txtTamanioPelota.text = sldTamanioPelota.value.ToString();
        pelota.transform.localScale = new Vector3(sldTamanioPelota.value / 2, sldTamanioPelota.value / 2, sldTamanioPelota.value / 2);
    }

    public void sliderVelocidadPelotasChanged()
    {
        txtVelocidadPelotas.text = sldVelocidadPelotas.value.ToString();
    }
    public void sliderCantidadResaltadasChanged()
    {
        txtCantidadResaltadas.text = sldCantidadResaltadas.value.ToString();
    }
    void recuperarSeteosJugador()
    {
        //coloco los valores recuperados en la pantalla
        lblJugador.text = datosJuego.jugadorActual.nombre;
        // coloco los parametros recuperados en cada lugar que le corresponde
        NivelDeJuego nivelDeJuego = datosJuego.jugadorActual.obtenerNivelDeJuego();

        sldVelocidadPelotas.value = nivelDeJuego.velocidadActualPelotas;
        sldCantidadPelotas.value = nivelDeJuego.cantidadTotalPelotas;
        sldCantidadResaltadas.value = nivelDeJuego.cantidadResaltadas;
        sldTamanioPelota.value = nivelDeJuego.tamanioPelota;
        sldTiempoDeColor.value = nivelDeJuego.tiempoDeColor;
        sldTiempoDeInicio.value = nivelDeJuego.tiempoDeInicio;
        dropSelectorNivel.value = datosJuego.jugadorActual.nivelActual - 1;


        //
        dictionarySeteos.Clear();
        dictionarySeteos.Add(sldVelocidadPelotas.name, (int)sldVelocidadPelotas.value);
        dictionarySeteos.Add(sldCantidadPelotas.name, (int)sldCantidadPelotas.value);
        dictionarySeteos.Add(sldCantidadResaltadas.name, (int)sldCantidadResaltadas.value);
        dictionarySeteos.Add(sldTamanioPelota.name, (int)sldTamanioPelota.value);
        dictionarySeteos.Add(sldTiempoDeColor.name, (int)sldTiempoDeColor.value);
        dictionarySeteos.Add(sldTiempoDeInicio.name, (int)sldTiempoDeInicio.value);
    }

    /*Este método se dispara cada vez que un elemento de la pantalla Personalización cambia.*/
    void ValueChangeCheck()
    {
        dictionaryActual.Clear();
        dictionaryActual.Add(sldVelocidadPelotas.name, (int)sldVelocidadPelotas.value);
        dictionaryActual.Add(sldCantidadPelotas.name, (int)sldCantidadPelotas.value);
        dictionaryActual.Add(sldCantidadResaltadas.name, (int)sldCantidadResaltadas.value);
        dictionaryActual.Add(sldTamanioPelota.name, (int)sldTamanioPelota.value);
        dictionaryActual.Add(sldTiempoDeColor.name, (int)sldTiempoDeColor.value);
        dictionaryActual.Add(sldTiempoDeInicio.name, (int)sldTiempoDeInicio.value);

        int c = 0;
        
        foreach (KeyValuePair<string, int> kvp in dictionarySeteos)
        {
            //Preguntamos si los valores de los diccionarios son iguales
            if (kvp.Value != dictionaryActual[kvp.Key])
            {
                c++;
            }
        }

        if (c > 0)
        {
            btnGuardarVolver.interactable = true;
        }
        else
        {
            btnGuardarVolver.interactable = false;
        }

    }

    void ListenerControls()
    {
        sldVelocidadPelotas.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        sldCantidadPelotas.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        sldCantidadResaltadas.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        sldTamanioPelota.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        sldTiempoDeColor.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        sldTiempoDeInicio.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        dropSelectorNivel.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }
}
