using UnityEngine;
using System.Collections;

[RequireComponent(typeof( Collider ))]

/// <summary>
/// Player is "dead" if it hits any part of the killzone.
/// </summary>
public class Killzone : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter( Collider other )
	{		
		// Play sound
		
		// Play particle
		
		// Reset the level or spawn the player at the last check point		
		LevelManager.instance.RestartLevel();
	}
}
