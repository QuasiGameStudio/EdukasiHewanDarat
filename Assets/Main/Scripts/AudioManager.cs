using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	private static AudioManager Instance;
	
	void Awake(){
		DontDestroyOnLoad (this);
			
		if (Instance == null) {
			Instance = this;
		} else {
			DestroyObject(gameObject);
		}
 	}
}
