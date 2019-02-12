﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// #if GOOGLE_MOBILE_ADS
using GoogleMobileAds;
using GoogleMobileAds.Api;
// #endif


public class AdMobmanager : Singleton<AdMobmanager> {



	#region
// #if GOOGLE_MOBILE_ADS

	
	bool testingMode = false;
	BannerView bannerView;
	InterstitialAd interstitial;
	AdRequest requestBanner;
	AdRequest requestInterstitial;

	string goToSceneWithAdName = "null";
// #endif

// #if GOOGLE_MOBILE_ADS
	private string appId = "ca-app-pub-3711064669743525~2980405530";	
	private string bannerId = "ca-app-pub-3711064669743525/7849588838";	
	private string interstitialId = "ca-app-pub-3711064669743525/7658017140";

	//Edit with your device id
	private string testDeviceId = "81A5D70CE479330C99C85E799E15DA1A";
	//J D93FCD789EBA4F17
	//Z 2853274C00C13F2D
	
// #endif

	private static AdMobmanager Instance;

	int tryingToShowInterstitial;

	void Awake(){

		DontDestroyOnLoad (this);
			
		if (Instance == null) {
			Instance = this;
		} else {
			DestroyObject(gameObject);
		}

// #if GOOGLE_MOBILE_ADS
		MobileAds.Initialize(appId);
// #endif

		Set();
	
	}

	public void Set(){

		if(bannerView != null){
			bannerView.Hide();
		}
		
// #if GOOGLE_MOBILE_ADS
		if(SceneManager.GetActiveScene().name == "MewarnaiHewan")
			bannerView = new BannerView(bannerId, AdSize.SmartBanner, AdPosition.Top);
		else
			bannerView = new BannerView(bannerId, AdSize.SmartBanner, AdPosition.Bottom);
		
		if(testingMode)
			requestBanner = new AdRequest.Builder().AddTestDevice(testDeviceId).Build();
		else
			requestBanner = new AdRequest.Builder().Build();

		interstitial = new InterstitialAd(interstitialId);

		bannerView.Show();

		
// #endif
		RequestInterstitial();


	}

	public void DestroyBanner(){
		bannerView.Destroy();
	}

	public void DestoryInterstitial(){
		interstitial.Destroy();
	}

	void RequestInterstitial()
	{
// #if GOOGLE_MOBILE_ADS
		
		if(testingMode)
			requestInterstitial = new AdRequest.Builder().AddTestDevice(testDeviceId).Build();
		else
			requestInterstitial = new AdRequest.Builder().Build();
		

		interstitial.OnAdClosed += HandleOnAdClosed;
		interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;

		interstitial.LoadAd(requestInterstitial);
		
// #endif
	}

// #if GOOGLE_MOBILE_ADS
	// Called when an ad request has successfully loaded.
	public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
	
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
		if(goToSceneWithAdName != "null" && SceneManager.GetActiveScene().name != "TangkapHewan")
			SceneController.Instance.GoToScene(goToSceneWithAdName);			
	}

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        
    }
	// Called when the user is about to return to the app after an ad click.
	
	
	
// #endif

	// Use this for initialization
	void Start () {

		//MobileAds.Initialize(appId);

		//ShowBanner();
		//ShowInterstitial();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowBanner(){
// #if GOOGLE_MOBILE_ADS
		bannerView.LoadAd(requestBanner);	

		bannerView.OnAdLoaded -= HandleOnAdLoaded;
		bannerView.OnAdFailedToLoad -= HandleOnAdFailedToLoad;
		bannerView.OnAdOpening -= HandleOnAdOpened;
		bannerView.OnAdClosed -= HandleOnAdClosed;

		// Called when an ad request has successfully loaded.
		bannerView.OnAdLoaded += HandleOnAdLoaded;
		// Called when an ad request failed to load.
		bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
		// Called when an ad is clicked.
		bannerView.OnAdOpening += HandleOnAdOpened;
		// Called when the user returned from the app after an ad click.
		bannerView.OnAdClosed += HandleOnAdClosed;
		
// #endif
	}

	public void ShowInterstitial(string sceneName){
// #if GOOGLE_MOBILE_ADS
		goToSceneWithAdName = sceneName;

		if (interstitial.IsLoaded())
		{
			interstitial.Show();
			
		}
		else
		{
			RequestInterstitial();

			ShowInterstitial(goToSceneWithAdName);

			tryingToShowInterstitial++;
			
			if(goToSceneWithAdName != "null" && tryingToShowInterstitial == 3){
				tryingToShowInterstitial = 0;
				SceneController.Instance.GoToScene(goToSceneWithAdName);
			}



		}
// #endif
	}

	public void ShowInterstitial(){
// #if GOOGLE_MOBILE_ADS
		if (interstitial.IsLoaded())
		{
			interstitial.Show();
		}
		else
		{
			RequestInterstitial();
		}
// #endif
	}



	#endregion
}
