using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalSpawner : Singleton<AnimalSpawner> {

	private bool isPlaying = true; 

	[SerializeField]
	private GameObject[] spawners;

	[SerializeField]
	private GameObject animalObject;

	[SerializeField]
	private Sprite[] animalSprites;

	[SerializeField]
	private AudioClip[] animalClips;

	[SerializeField]
	private Text indexText;
	private	int[] indexArray = new int[32];
	private int index = -1;

	//Complete Panel
	[SerializeField]
	private GameObject failPanel;
	[SerializeField]
	private Text failTotalStarText;
	[SerializeField]
	private Text failClockText;	

	//Complete Panel
	[SerializeField]
	private GameObject completePanel;
	
	[SerializeField]
	private Text totalStarText;
	[SerializeField]
	private Text completedClockText;

	[SerializeField]
	private Text timeText;

	// Use this for initialization
	void Start () {

		//Assign Index Array
		for (int i = 0; i < 32; i++){
			indexArray[i] = i;
		}
		
		RandomArrayOrder();

		Spawning();

		AdMobmanager.Instance.Set();		
		AdMobmanager.Instance.ShowBanner();
	}

	void Finish(){
		Debug.Log("Finish");

		isPlaying = false;
		
		GameData.Instance.TangkapHewan_TotalStars +=3;

		completePanel.SetActive(true);
		totalStarText.text = GameData.Instance.TangkapHewan_TotalStars + "";
		completedClockText.text = timeText.text;		

		completePanel.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
		completePanel.transform.GetChild(0).GetChild(3).gameObject.SetActive(true);


		if (TimeManager.Instance.GetTime() < GameData.Instance.TangkapHewan_HTime || GameData.Instance.TangkapHewan_HTime == 0) {						
			GameData.Instance.TangkapHewan_HTime = TimeManager.Instance.GetTime();
		}	
		
	}

	void RandomArrayOrder(){
		for (int i = 0; i < 50; i++){
			int r = Random.Range(0,32);
			int r_ = Random.Range(0,32);
			
			int tempIndex = indexArray[r];
			indexArray[r] = indexArray[r_];
			indexArray[r_] = tempIndex;
		}
		// //Show array value
		// for (int i = 0; i < indexArray.Length; i++){
		// 	Debug.Log(indexArray[i]);
		// }

		StartCoroutine("FirstSpawn");
	}
	
	// Update is called once per frame
	void Update () {
		if(isPlaying)
			timeText.text = TimeManager.Instance.GetTimeString();	
	}

	IEnumerator FirstSpawn(){
		

		for (int i = 0; i < 3; i++)
		{
			yield return new WaitForSeconds(5);		
			Spawning();
		}
	}

	public void ShoutClip(AudioClip clip){
		GetComponent<AudioSource>().PlayOneShot(clip);
	}

	public void Spawning(){

		if(index < 30 - 1){
			index++;

			indexText.text = (index+1) + "/30";

			int index_ = indexArray[index];

			int r_spawn = Random.Range(0,spawners.Length);		

			GameObject obj = Instantiate(animalObject,spawners[r_spawn].transform.position,this.transform.rotation);

			if(r_spawn < 3)
				obj.GetComponent<AnimalObject>().SetAnimal(index_,animalSprites[index_],1,spawners[r_spawn].transform,
				animalClips[index_]);
			else
				obj.GetComponent<AnimalObject>().SetAnimal(index_,animalSprites[index_],-1,spawners[r_spawn].transform,
				animalClips[index_]);
		}	
		
		else{
			Finish();
		}	
	}

	public void Fail(){
		failPanel.SetActive(true);	

		isPlaying = false;				
		
		failTotalStarText.text = GameData.Instance.TangkapHewan_TotalStars + "";
		failClockText.text = timeText.text;		

		failPanel.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);

		AdMobmanager.Instance.ShowInterstitial();
				
	}

	public bool GetIsPlaying(){
		return isPlaying;
	}



	
}
