using UnityEngine;
using System.Collections;

[RequireComponent(typeof( Collider ))]

/// <summary>
/// Player is "dead" if it hits any part of this object.
/// </summary>
public class DeadlyObject : MonoBehaviour {

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
		
		// Kill the player
		
		// Spawn the player at the last check point		
		LevelManager.instance.RestartLevel();
	}
}
