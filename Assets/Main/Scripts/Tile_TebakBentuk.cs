using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_TebakBentuk : MonoBehaviour {

	[SerializeField]
	private bool isTarget;
	
	[SerializeField]
	private int number;

	private Vector2 originalTransform;

	// Use this for initialization
	void Start () {
		originalTransform = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int GetNumber(){
		return number;
	}

	public void SetNumber(int number){
		this.number = number;
	}

	public void ResetTransform(){
		transform.position = originalTransform;
	}

	public bool GetIsTarget(){
		return isTarget;
	}
}
