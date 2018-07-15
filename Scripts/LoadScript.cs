using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScript : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Escape)){
			Application.Quit();
		}
	}

    public void StartGame(){
        SceneManager.LoadScene(1);
    }

    public void Restart(){
    	SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
	
	public void GoToInit(){
		UIManager.instance.ExitIngameCanvas();
        SceneManager.LoadScene(0);
    }
}
