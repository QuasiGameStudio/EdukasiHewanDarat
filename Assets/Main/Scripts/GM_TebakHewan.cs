using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM_TebakHewan : MonoBehaviour {

	private bool isPlaying = true;

	private float time;

	[SerializeField]
	private Sprite[] animalSprites;

	[SerializeField]
	private AudioClip[] animalClips;

	private string[] animalNames = new string[30];

	[SerializeField]
	private Image[] answerButtons;

	[SerializeField]
	private Text indexText;

	[SerializeField]
	private Text questionText;

	private	int[] indexArray = new int[30];
	private int index;

	private int trueAnswer;

	[SerializeField]
	private Text n_timeText;
	[SerializeField]
	private Text h_timeText;

	//Complete Panel
	[SerializeField]
	private GameObject completePanel;
	
	[SerializeField]
	private Text totalStarText;
	[SerializeField]
	private Text completedClockText;

	[SerializeField]
	private AudioClip[] gameClips;

	// Use this for initialization
	void Start () {	

		

		//Assign Index Array
		for (int i_ = 0; i_ < 30; i_++){
			indexArray[i_] = i_;
		}
	
		RandomArrayOrder();	

		int index_ = indexArray[index];

		GetComponent<AudioSource>().PlayOneShot(animalClips[index_]);
		
		//Set High Score to Text
		if(GameData.Instance.TebakHewan_HTime == 0)		
			h_timeText.text = "-";
		else
			h_timeText.text = TimeManager.Instance.FloatTimeToString(GameData.Instance.TebakHewan_HTime);		

		indexText.text = (index+1) + "/30";

		trueAnswer = Random.Range(0,3);

		questionText.text = "" + animalSprites[index_].name.ToUpper();//"Question: " + trueAnswer;
				
		//
		int i = Random.Range(0,animalSprites.Length);
		int j = Random.Range(0,animalSprites.Length);
		while(i == index_){
			i = Random.Range(0, animalSprites.Length);					
		}	
		while(j == index_ || i == j ){
			Debug.Log(i + " == " + j);		
			j = Random.Range(0, animalSprites.Length);		
		}

		
		answerButtons[trueAnswer].transform.GetChild(0).GetComponent<Image>().sprite = animalSprites[index_];

		switch (trueAnswer)
		{
			case 0:
				answerButtons[1].transform.GetChild(0).GetComponent<Image>().sprite = animalSprites[i];
				answerButtons[2].transform.GetChild(0).GetComponent<Image>().sprite = animalSprites[j];
				break;
			case 1:
				answerButtons[0].transform.GetChild(0).GetComponent<Image>().sprite = animalSprites[i];
				answerButtons[2].transform.GetChild(0).GetComponent<Image>().sprite = animalSprites[j];
				break;
			case 2:
				answerButtons[0].transform.GetChild(0).GetComponent<Image>().sprite = animalSprites[i];
				answerButtons[1].transform.GetChild(0).GetComponent<Image>().sprite = animalSprites[j];
				break;
			default:
				
				break;
		}

		AdMobmanager.Instance.Set();		
		AdMobmanager.Instance.ShowBanner();
		
	}
	
	void RandomArrayOrder(){
		for (int i = 0; i < 50; i++){
			int r = Random.Range(0,30);
			int r_ = Random.Range(0,30);
			

			int tempIndex = indexArray[r];
			indexArray[r] = indexArray[r_];
			indexArray[r_] = tempIndex;
		}

		//Show array value
		for (int i = 0; i < indexArray.Length; i++){
			Debug.Log(indexArray[i]);
		}
	}

	// Update is called once per frame
	void Update () {

		if(isPlaying)
			n_timeText.text = TimeManager.Instance.GetTimeString();			
	}

	void NextQuestion(){

		

		if(index < animalSprites.Length - 1){
			index++;


			int index_ = indexArray[index];

			GetComponent<AudioSource>().PlayOneShot(animalClips[index_]);

			indexText.text = (index+1) + "/30";

			trueAnswer = Random.Range(0,3);

			questionText.text = "" + animalSprites[index_].name.ToUpper();//"Question: " + trueAnswer;		
			
			//
			int i = Random.Range(0,animalSprites.Length);
			int j = Random.Range(0,animalSprites.Length);
			while(i == index_){
				i = Random.Range(0,animalSprites.Length);	
				// Debug.Log("i: " + i);		
			}	
			while(j == index_ || i == j ){
				j = Random.Range(0,animalSprites.Length);		
				// Debug.Log("j: " + j);		
			}

			answerButtons[trueAnswer].transform.GetChild(0).GetComponent<Image>().sprite = animalSprites[index_];

			switch (trueAnswer)
			{
				case 0:
					answerButtons[1].transform.GetChild(0).GetComponent<Image>().sprite = animalSprites[i];
					answerButtons[2].transform.GetChild(0).GetComponent<Image>().sprite = animalSprites[j];
					break;
				case 1:
					answerButtons[0].transform.GetChild(0).GetComponent<Image>().sprite = animalSprites[i];
					answerButtons[2].transform.GetChild(0).GetComponent<Image>().sprite = animalSprites[j];
					break;
				case 2:
					answerButtons[0].transform.GetChild(0).GetComponent<Image>().sprite = animalSprites[i];
					answerButtons[1].transform.GetChild(0).GetComponent<Image>().sprite = animalSprites[j];
					break;
				default:
					
					break;
			}

		}else{
			Finish();
		}
	}

	public void Answer(int answer){
		if(answer == trueAnswer){
			// Debug.Log(answer + "::" + trueAnswer);
			GetComponent<AudioSource>().PlayOneShot(gameClips[1]);
			NextQuestion();
		}else{
			GetComponent<AudioSource>().PlayOneShot(gameClips[0]);
		}
	}

	void Finish(){
		Debug.Log("Finish");
		isPlaying = false;
		
		GameData.Instance.TebakHewan_TotalStars +=3;

		completePanel.SetActive(true);
		totalStarText.text = GameData.Instance.TebakHewan_TotalStars + "";
		completedClockText.text = n_timeText.text;		

		completePanel.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
		completePanel.transform.GetChild(0).GetChild(3).gameObject.SetActive(true);


		if (TimeManager.Instance.GetTime() < GameData.Instance.TebakHewan_HTime || GameData.Instance.TebakHewan_HTime == 0) {						
			GameData.Instance.TebakHewan_HTime = TimeManager.Instance.GetTime();
		}			
	}
}
