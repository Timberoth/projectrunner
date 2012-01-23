using UnityEngine;
using System.Collections;

/// <summary>
/// The FinishLine object is what the player needs to touch to end the level.
/// </summary>

[RequireComponent(typeof(Collider))]

public class FinishLine : MonoBehaviour {
	
	/// <summary>
	/// Use this for initialization 
	/// </summary>	
	void Start () {
	
	}
	
	
	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update () {
	
	}
	
	
	/// <summary>
	/// Fired when object is touched
	/// </summary>
	void OnTriggerEnter( Collider other )
	{
		// Check for player tag
		if( other.gameObject.tag == "Player" )
		{
			// Play particles
		
			// Play sound
		
			// Call the end level function
			LevelManager.instance.FinishLevel();	
		}		
	}
}
