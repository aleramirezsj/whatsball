using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VolverMenu : MonoBehaviour {
	public void CambiarEscenaA(string nombreEscena)
	{
		SceneManager.LoadScene(nombreEscena);
    }
	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)) 
			CambiarEscenaA("Home"); 

	}	
}
