using UnityEngine;
using System.Collections;

/// <summary>
/// Moving surface
/// </summary>
public class MovingPlatform : Platform {
	
	
	
	/// <summary>
	/// Keep an array of points where this platform will travel between.
	/// </summary>
	private Vector3[] waypoints;
	
	private int NUM_WAYPOINTS;
	
	/// <summary>
	/// Track which waypoint the platforms is moving toward.
	/// </summary>
	private int waypointIndex = 0;
	
	/// <summary>
	/// Track the starting position just in case we need to use it as a waypoint when
	/// looping back.
	/// </summary>
	private Vector3 startingPosition;
	
	
	/// <summary>
	/// After reaching the final waypoint should the platform tween to the starting point.
	/// </summary>
	public bool returnToStart = true;
	
	/// <summary>
	/// After reaching the last waypoint or the starting point, should the platform continue looping.
	/// </summary>
	public bool loop = true;
	
	/// <summary>
	/// How fast the platform moves between waypoints
	/// </summary>
	public float speed = 1.0f;
	
	// Use this for initialization
	void Start () {	
		/*
		if( transform.childCount < 1 )
		{
			print("[ERROR] - Moving platform does not have any waypoints.");
			Debug.Break();
			
			return;
		}
		
		startingPosition = new Vector3( transform.position.x, transform.position.y, transform.position.z );
		waypointIndex = 0;
		
		// Track number of waypoints we're working with.
		NUM_WAYPOINTS = transform.childCount;
		
		// Create waypoints array based on the number of children.	
		waypoints = new Vector3[NUM_WAYPOINTS];			
		
		int i = 0;
		// Go through all the children and note their positions
		foreach (Transform child in transform)
		{
			waypoints[i] = child.position;
			print( waypoints[i] );
			i++;			
		}		
		
		// Start moving toward the first waypoint.
		iTween.MoveTo(gameObject, iTween.Hash("position", waypoints[waypointIndex],		                                      
		                                      "speed", speed,
		                                      "oncomplete", "WaypointReached"));
		      */                                
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	
	private float speed2 = 0.1f;
	public GameObject targetA;
	public GameObject targetB;
	
	void FixedUpdate () 
	{
		float weight = Mathf.Cos(Time.time * speed2 * 2 * Mathf.PI) * 0.5f + 0.5f;
		transform.position = targetA.transform.position * weight + targetB.transform.position * (1-weight);
	}
	
	
	/// <summary>
	/// The current waypoint has been reached.
	/// </summary>
	void WaypointReached()
	{
		// Increment waypointIndex
		waypointIndex++;
		
		// If this is the last waypoint, move to the starting position if returnToStart is true
		if( waypointIndex >= NUM_WAYPOINTS )
		{
			if( returnToStart )
			{
				// Set this to -1 since it'll be incremented to 0 when WaypointReached is called
				// after the starting point is reached.
				waypointIndex = -1;
				
				iTween.MoveTo(gameObject, iTween.Hash("position", startingPosition,
		                                      "speed", speed,
		                                      "oncomplete", "WaypointReached"));
			}
			
			// Don't do anything else because we're completely done.
			else
			{				
			}
		}
		
		// This case means that we've just reached the starting position after
		// making a complete circuit.
		else if( waypointIndex == 0 )
		{
			// Continue moving between the waypoints forever.
			if( loop )
			{
				iTween.MoveTo(gameObject, iTween.Hash("position", waypoints[waypointIndex],
		                                      "speed", speed,
		                                      "oncomplete", "WaypointReached"));
			}
			
			// Don't do anything else because we're completely done.
			else
			{
			}
		}
		
		// Move onto the next waypoint if possible.		
		else
		{
			print( waypointIndex );
			iTween.MoveTo(gameObject, iTween.Hash("position", waypoints[waypointIndex],
		                                      "speed", speed,
		                                      "oncomplete", "WaypointReached"));
		}
		
	}
}
