using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		AdMobmanager.Instance.Set();		
		AdMobmanager.Instance.ShowBanner();						
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
