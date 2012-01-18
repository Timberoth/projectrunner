using UnityEngine;
using System.Collections;

/// <summary>
/// Projectiles are deadly objects that can tween between locations.
/// </summary>
public class Projectile : DeadlyObject {
			
	public iTween.EaseType easeType = iTween.EaseType.linear;
		
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
		this.gameObject.transform.position = start;
		
		// Start moving toward the first waypoint.
		iTween.MoveTo( this.gameObject, iTween.Hash("position", end,		                                      
		                                      "speed", speed,
		                                      "easetype", easeType,
		                                      "oncomplete", "EndReached"));
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
