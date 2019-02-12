using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	[SerializeField]
	private int number;

	[SerializeField]
	private bool canDrag;

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

	public bool GetCanDrag(){
		return canDrag;
	}

	public void SetCanDrag(bool canDrag){
		this.canDrag = canDrag;
	}

	public void ResetTransform(){
		transform.position = originalTransform;
	}
}
