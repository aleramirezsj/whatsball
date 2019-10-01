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
		Debug.Log(SettingsGame.sound.ToString());
		if(SettingsGame.sound==true){
			Reproducir();
			//
		}
		else{
			Detener();
			//
		}
		
	}
	
	void Update(){

	}

	public void Reproducir(){
		reproducirMusica.SetActive(false);
		detenerMusica.SetActive(true);
		SettingsGame.sound=true;
		reproductor.Play();
	}
	public void Detener(){
		reproducirMusica.SetActive(true);
		detenerMusica.SetActive(false);
		SettingsGame.sound=false;
		reproductor.Pause();
	}

}
	


