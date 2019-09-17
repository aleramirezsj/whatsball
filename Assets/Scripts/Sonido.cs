using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Sonido : MonoBehaviour {

	public GameObject reproducirMusica;
	public GameObject detenerMusica;
	AudioSource reproductor;
	
	void Start () {
		reproductor = this.gameObject.GetComponent<AudioSource>();

		if(SettingsGame.sound==true){
			Reproducir();
			SettingsGame.sound=false;
		}
		else{
			Detener();
			SettingsGame.sound=true;
		}
		
	}
	
	void Update(){

	}

	public void Reproducir(){
		reproducirMusica.SetActive(false);
		detenerMusica.SetActive(true);

		reproductor.Play();
	}
	public void Detener(){
		reproducirMusica.SetActive(true);
		detenerMusica.SetActive(false);

		reproductor.Pause();
	}

}
	


