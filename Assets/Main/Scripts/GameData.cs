using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : Singleton<GameData> {

	[SerializeField]
	private bool resetData;

	// Use this for initialization
	void Start () {
		if(resetData)
			ResetData();
	}

	void ResetData(){
		if(resetData)
			PlayerPrefs.DeleteAll();
	}

	public string PlayerName{		
		get { return PlayerPrefs.GetString("PrefPlayerName","_Player");}
		set { PlayerPrefs.SetString("PrefPlayerName",value);}
	}

	public float TebakHewan_HTime	
	{
		get { return PlayerPrefs.GetFloat("TebahHewan_HTime",0);}
		set { PlayerPrefs.SetFloat("TebahHewan_HTime",value);}
	}

	public int TebakHewan_TotalStars
	{
		get { return PlayerPrefs.GetInt("TebakHewan_TotalStars",0);}
		set { PlayerPrefs.SetInt("TebakHewan_TotalStars",value);}
	}
	
	public int Puzzle_TotalStars
	{
		get { return PlayerPrefs.GetInt("Puzzle_TotalStars",0);}
		set { PlayerPrefs.SetInt("Puzzle_TotalStars",value);}
	}

	public float TebakBentuk_HTime	
	{
		get { return PlayerPrefs.GetFloat("TebakBentuk_HTime",0);}
		set { PlayerPrefs.SetFloat("TebakBentuk_HTime",value);}
	}

	public int TebakBentuk_TotalStars
	{
		get { return PlayerPrefs.GetInt("TebakBentuk_TotalStars",0);}
		set { PlayerPrefs.SetInt("TebakBentuk_TotalStars",value);}
	}

	public int TangkapHewan_TotalStars
	{
		get { return PlayerPrefs.GetInt("TangkapHewan_TotalStars",0);}
		set { PlayerPrefs.SetInt("TangkapHewan_TotalStars",value);}
	}

	public float TangkapHewan_HTime	
	{
		get { return PlayerPrefs.GetFloat("TangkapHewan_HTime",0);}
		set { PlayerPrefs.SetFloat("TangkapHewan_HTime",value);}
	}

	
	
	
	
}
