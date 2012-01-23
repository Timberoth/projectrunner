using UnityEngine;
using System.Collections;



/// <summary>
/// Projectiles are deadly objects that can tween between locations.
/// </summary>
public class Projectile : DeadlyObject {
			
	public iTween.EaseType easeType = iTween.EaseType.linear;
	public Vector3 startPosition = Vector3.zero;
	public Vector3 endPosition = Vector3.zero;	
	public float speed = 0.0f;
	
	// TODO GET RID OF THIS since all projectiles will be created at runtime.
	void Start()
	{	
		// Calculate angle between start and end positions to properly point projectile.
		float angle = Vector3.Angle( startPosition, endPosition ) + 90.0f;		
		this.gameObject.transform.RotateAround( startPosition, Vector3.forward, angle);
		this.gameObject.transform.position = startPosition;		
			
		iTween.MoveTo( this.gameObject, iTween.Hash("position", endPosition,		                                      
		                                      "speed", this.speed,
		                                      "easetype", easeType,
		                                      "oncomplete", "EndReached"));	
	}
		
	/// <summary>
	/// Get the projectile moving with a start and end position along with speed value 
	/// </summary>
	/// <param name="start">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="end">
	/// A <see cref="Vector3"/>
	/// </param>
	/// <param name="speed">
	/// A <see cref="System.Single"/>
	/// </param>
	void Fire( Vector3 start, Vector3 end, float speed )
	{
		startPosition = start;
		endPosition = end;		
		this.speed = speed;
				
		// Calculate angle between start and end positions to properly point projectile.
		float angle = Vector3.Angle( startPosition, endPosition ) + 90.0f;		
		this.gameObject.transform.RotateAround( startPosition, Vector3.forward, angle);
		this.gameObject.transform.position = startPosition;		
				
		// Start moving toward the endpoint.
		iTween.MoveTo( this.gameObject, iTween.Hash("position", endPosition,		                                      
		                                      "speed", this.speed,
		                                      "easetype", easeType,
		                                      "oncomplete", "EndReached"));
		
		// Start particle effects
		
		// Play sound
	}
	
	/// <summary>
	/// Collision callback 
	/// </summary>
	/// <param name="collider">
	/// A <see cref="Collider"/>
	/// </param>
	void OnTriggerEnter( Collider collider )
	{	
		// Check if we've hit the player or environment, since we don't want to blow up if we've hit other projecticles.				
		if( collider.gameObject.tag == "Player" || collider.gameObject.tag == "Environment" )
		{
			// Play particle
		
			// Play sound effect
		
			// Restart the level.			
			
			// Kill the projectile
			GameObject.Destroy( this.gameObject );
		}				
	}
	
	/// <summary>
	/// The projectile has reached the end of the line without hitting anything and should be destroyed. 
	/// </summary>
	void EndReached()
	{			
		// Kill this object with no fanfare since nothing was hit.
		GameObject.Destroy( this.gameObject );
	}
}
