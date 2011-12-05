using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {
	
	// Only activate once
	private bool active = true;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	/// <summary>
	/// Track that this is where the player respawns after touching the killzone. 
	/// </summary>
	/// <param name="other">
	/// A <see cref="Collider"/>
	/// </param>
	void OnTriggerEnter( Collider other )
	{
		// Only activate once a collision.
		if( !active )
			return;
		
		active = false;
		
		print("Checkpoint hit");
		
		// Play sound
		
		// Play animation
		
		// Play particles
		
		// Update the checkPointPosition being tracked in the LevelManager		
		LevelManager.instance.checkPointPosition.x = this.transform.position.x;
		LevelManager.instance.checkPointPosition.y = this.transform.position.y;
		LevelManager.instance.checkPointPosition.z = this.transform.position.z;
				
	}
}
