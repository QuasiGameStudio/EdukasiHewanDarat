using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalObject : MonoBehaviour {

	private int index;

	private float movingSpeed = 0.5f;
	private int direction;

	private Transform spawnerTransform;

	[SerializeField]
	private AudioClip clip;

	
	private AudioClip animalClip;

	// Use this for initialization
	void Start () {
		movingSpeed = Random.Range(1,1.2f);
	}
	
	// Update is called once per frame
	void Update () {

		transform.Translate(movingSpeed * Time.deltaTime * direction,0,0);
		if(!AnimalSpawner.Instance.GetIsPlaying())
			Destroy (this.gameObject);
	}

	void OnMouseDown(){
        // this object was clicked - do something
     	Catch();
  	}   

	void Catch(){
		
		AnimalSpawner.Instance.ShoutClip(clip);

		AnimalSpawner.Instance.ShoutClip(animalClip);

		if(index < 30){
			AnimalSpawner.Instance.Spawning();
			
			Destroy (this.gameObject);
		} else{
			AnimalSpawner.Instance.Fail();
		}

		if(!AnimalSpawner.Instance.GetIsPlaying())
			Destroy (this.gameObject);
		

	}

	public void SetAnimal(int index, Sprite sprite, int dir,Transform spawnerTransform,AudioClip clip){
		this.index = index;
		transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprite;
		direction = dir;
		this.spawnerTransform = spawnerTransform;
		animalClip = clip;
	}

	void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Border"){
			GetComponent<SpriteRenderer>().enabled = false;
			transform.position = spawnerTransform.position;
			GetComponent<SpriteRenderer>().enabled = true;
		}
    }

}
