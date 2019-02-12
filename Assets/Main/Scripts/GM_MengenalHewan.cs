using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM_MengenalHewan : MonoBehaviour {

	[SerializeField]
	private AudioClip[] animalClips;

	[SerializeField]
	private Sprite[] animalSprites;
	private string[] animalNames = new string[30];

	[SerializeField]
	private Image animalImage;

	[SerializeField]
	private Text animalNameText;

	private int index;

	// Use this for initialization

	void Awake(){
		
	}
	
	void Start () {
		AssignAnimalNames();
		animalNameText.text = animalSprites[index].name.ToUpper();
		GetComponent<AudioSource>().PlayOneShot(animalClips[index]);


		
		AdMobmanager.Instance.Set();		
		AdMobmanager.Instance.ShowBanner();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Next(){
		if(index < animalSprites.Length - 1){
			index++;
			GetComponent<AudioSource>().PlayOneShot(animalClips[index]);
			animalImage.sprite = animalSprites[index];		
			animalNameText.text = animalSprites[index].name.ToUpper();
		}
	}
	public void Prev(){
		if(index > 0){
			index--;
			GetComponent<AudioSource>().PlayOneShot(animalClips[index]);
			animalImage.sprite = animalSprites[index];			
			animalNameText.text = animalSprites[index].name.ToUpper();
		}
	}

	void AssignAnimalNames(){

		animalNames[0] = "";
		animalNames[1] = "";
		animalNames[2] = "";
		animalNames[3] = "";
		animalNames[4] = "";
		animalNames[5] = "";
		animalNames[6] = "";
		animalNames[7] = "";
		animalNames[8] = "";
		animalNames[9] = "";
		
		animalNames[10] = "";
		animalNames[11] = "";
		animalNames[12] = "";
		animalNames[13] = "";
		animalNames[14] = "";
		animalNames[15] = "";
		animalNames[16] = "";
		animalNames[17] = "";
		animalNames[18] = "";
		animalNames[19] = "";

		animalNames[20] = "";
		animalNames[21] = "";
		animalNames[22] = "";
		animalNames[23] = "";
		animalNames[24] = "";
		animalNames[25] = "";
		animalNames[26] = "";
		animalNames[27] = "";
		animalNames[28] = "";
		animalNames[29] = "";

	}
}
