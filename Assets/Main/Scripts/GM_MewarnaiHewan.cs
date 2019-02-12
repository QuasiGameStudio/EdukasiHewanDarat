using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FreeDraw;

public class GM_MewarnaiHewan : MonoBehaviour {

	private int index;

	[SerializeField]
	private Sprite[] animalSprites;

	[SerializeField]
	private Sprite[] animalLineArtSprites;

	[SerializeField]
	private GameObject[] buttonObjects;

	[SerializeField]
	private Image animalListPanelImage;

	[SerializeField]
	private Image targetImage;

	[SerializeField]
	private GameObject drawable;

	

	// Use this for initialization
	void Start () {
		SetButtonColors();

		AdMobmanager.Instance.Set();		
		AdMobmanager.Instance.ShowBanner();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void NextImage(){
		if(index < animalSprites.Length - 1){
			index++;
			animalListPanelImage.sprite = animalSprites[index];			
		}
	}

	public void PrevImage(){
		if(index > 0){
			index--;
			animalListPanelImage.sprite = animalSprites[index];					
		}
	}

	public void SelectImage(){
			targetImage.sprite = animalLineArtSprites[index];

			//reset canvas
			drawable.GetComponent<Drawable>().ResetCanvas();
	}

	void SetButtonColors(){	

		buttonObjects[0].transform.GetChild(0).GetComponent<Image>().color = Color.black;
		buttonObjects[1].transform.GetChild(0).GetComponent<Image>().color = Color.blue;
		buttonObjects[2].transform.GetChild(0).GetComponent<Image>().color = Color.cyan;
		buttonObjects[3].transform.GetChild(0).GetComponent<Image>().color = Color.gray;
		buttonObjects[4].transform.GetChild(0).GetComponent<Image>().color = Color.green;		
		buttonObjects[5].transform.GetChild(0).GetComponent<Image>().color = Color.magenta;
		buttonObjects[6].transform.GetChild(0).GetComponent<Image>().color = Color.red;
		buttonObjects[7].transform.GetChild(0).GetComponent<Image>().color = Color.yellow;
		buttonObjects[8].transform.GetChild(0).GetComponent<Image>().color = Color.white;
		
	}

	public void ResetCanvas(){
		//reset canvas
		drawable.GetComponent<Drawable>().ResetCanvas();
	}
}
