using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM_TebakBentuk : Singleton<GM_TebakBentuk> {

	private bool isPlaying = true;

	[SerializeField]
	private Text timeText;

	[SerializeField]
	private Sprite[] animalSprites;
	
	[SerializeField]
	private GameObject targetTile;

	[SerializeField]
	private GameObject[] sourceTiles;

	[SerializeField]
	private Text indexText;
	private	int[] indexArray = new int[30];
	private int index = -1;

	private int trueAnswer;

	//Complete Panel
	[SerializeField]
	private GameObject completePanel;
	
	[SerializeField]
	private Text totalStarText;
	[SerializeField]
	private Text completedClockText;


	// Use this for initialization
	void Start () {

		//Assign Index Array
		for (int i = 0; i < 30; i++){
			indexArray[i] = i;
		}
		
		RandomArrayOrder();		

		StartCoroutine("SetQuestion");

		AdMobmanager.Instance.Set();		
		AdMobmanager.Instance.ShowBanner();
	}

	IEnumerator SetQuestion(){

		for (int i = 0; i < sourceTiles.Length; i++){
			sourceTiles[i].GetComponent<Image>().raycastTarget = false;
		}

		if(index == -1)
			yield return new WaitForSeconds(0);
		else
			yield return new WaitForSeconds(2);
		
		if(index < animalSprites.Length - 1){
			index++;

			int index_ = indexArray[index];

			indexText.text = (index+1) + "/30";

			trueAnswer = Random.Range(0,3);	

			for (int i_ = 0; i_ < sourceTiles.Length; i_++){
				sourceTiles[i_].GetComponent<Image>().raycastTarget = true;
			}	
			
			//
			int i = Random.Range(0,animalSprites.Length);
			int j = Random.Range(0,animalSprites.Length);
			while(i == index_){
				i = Random.Range(0,animalSprites.Length);						
			}	
			while(j == index_ || i == j ){
				j = Random.Range(0,animalSprites.Length);						
			}

			targetTile.GetComponent<Image>().color = new Color(0,0,0,225);
			targetTile.GetComponent<Image>().sprite = animalSprites[index_];
			targetTile.GetComponent<Tile_TebakBentuk>().SetNumber(trueAnswer);

			sourceTiles[trueAnswer].transform.GetChild(0).GetComponent<Image>().sprite = animalSprites[index_];

			switch (trueAnswer)
			{
				case 0:
					sourceTiles[1].transform.GetChild(0).GetComponent<Image>().sprite = animalSprites[i];
					sourceTiles[2].transform.GetChild(0).GetComponent<Image>().sprite = animalSprites[j];
					break;
				case 1:
					sourceTiles[0].transform.GetChild(0).GetComponent<Image>().sprite = animalSprites[i];
					sourceTiles[2].transform.GetChild(0).GetComponent<Image>().sprite = animalSprites[j];
					break;
				case 2:
					sourceTiles[0].transform.GetChild(0).GetComponent<Image>().sprite = animalSprites[i];
					sourceTiles[1].transform.GetChild(0).GetComponent<Image>().sprite = animalSprites[j];
					break;
				default:
					
					break;
			}

		}else{
			Finish();
		}
	

	}

	void Finish(){
		Debug.Log("Finish");

		isPlaying = false;
		
		GameData.Instance.TebakBentuk_TotalStars +=3;

		completePanel.SetActive(true);
		totalStarText.text = GameData.Instance.TebakBentuk_TotalStars + "";
		completedClockText.text = timeText.text;		

		completePanel.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
		completePanel.transform.GetChild(0).GetChild(3).gameObject.SetActive(true);


		if (TimeManager.Instance.GetTime() < GameData.Instance.TebakBentuk_HTime || GameData.Instance.TebakBentuk_HTime == 0) {						
			GameData.Instance.TebakBentuk_HTime = TimeManager.Instance.GetTime();
		}	
		
	}

	void RandomArrayOrder(){
		for (int i = 0; i < 50; i++){
			int r = Random.Range(0,30);
			int r_ = Random.Range(0,30);
			
			int tempIndex = indexArray[r];
			indexArray[r] = indexArray[r_];
			indexArray[r_] = tempIndex;
		}
		// //Show array value
		// for (int i = 0; i < indexArray.Length; i++){
		// 	Debug.Log(indexArray[i]);
		// }
	}
	
	// Update is called once per frame
	void Update () {
		if(isPlaying)
			timeText.text = TimeManager.Instance.GetTimeString();	
	}

	public void Match(){
		targetTile.GetComponent<Image>().color = new Color(225,225,225,225);
		StartCoroutine("SetQuestion");
	}

	public void UnMatch(){

	}
}
