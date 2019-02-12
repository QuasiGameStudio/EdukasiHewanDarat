using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneController : Singleton<SceneController> {

	void Update(){
		if(Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "LevelSelector"){
			QuitGame();
		} else if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "LevelSelector")
		{
			GoToScene("LevelSelector");
		}
	}

	public void RestartScene (){
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}

	public void GoToScene(string sceneName){
		SceneManager.LoadScene (sceneName);
	}

	public void GoToSceneWithAd(string sceneName){
		AdMobmanager.Instance.ShowInterstitial(sceneName);
	}

	public void QuitGame(){
		Application.Quit ();
	}
}
