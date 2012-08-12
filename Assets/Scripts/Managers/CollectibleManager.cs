using UnityEngine;
using System.Collections;


public sealed class CollectibleManager {

	/*
	 * Singleton Code
	 */	
	private static readonly CollectibleManager instance = new CollectibleManager();
	
	private CollectibleManager(){}
	
	public static CollectibleManager Instance{ get { return instance; } }
		
		
	// Number of stars collected.
	private int starsCollected = 0;
	private string starsCollectedKey = "StarsCollected";
	
	
	public void Initialize()
	{
		// Load data
		Load();
	}
		
	
	public void Save()
	{
		PlayerPrefs.SetInt( starsCollectedKey, starsCollected );
		PlayerPrefs.Save();
	}
	
	
	public void Load()
	{
		// If this is the first time the CollectibleManager has been loaded, create the necessary keys
		if( !PlayerPrefs.HasKey( starsCollectedKey ) )
		{
			PlayerPrefs.SetInt( starsCollectedKey, 0 );
			PlayerPrefs.Save();
			
			starsCollected = 0;
		}
		
		// Load the save data from the PlayerPrefs
		else
		{
			starsCollected = PlayerPrefs.GetInt( starsCollectedKey );
		}		
	}
	
	
	public void SetStarsCollected( int newValue, bool saveGame = false )
	{				
		starsCollected = newValue;
		
		//LevelManager.instance.collectibleText.Text = "Collectibles: "+starsCollected;
		
		if( saveGame )
			Save();
	}
	
	
	public int GetStarsCollected()
	{
		return starsCollected;
	}
	
	
	public void IncrementStarsCollected()
	{
		starsCollected++;
		
		//LevelManager.instance.collectibleText.Text = "Collectibles: "+starsCollected;
	}
}
