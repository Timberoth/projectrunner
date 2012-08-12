using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
	
	
	// INPUT CODE
#if UNITY_EDITOR
	private bool mouseHeld = false;
#endif
		
	// Last Checkpoint Position
	public Vector3 checkPointPosition;
	
	// Prefab References used to spawn objects at run time
	
	
	// Keep a reference to the player object
	private GameObject playerObject = null;
	
	/// <summary>
	/// LevelManager Singleton Code 
	/// </summary>
	public static LevelManager instance;
	void Awake(){		
		if(instance == null){
			instance = this;
		}
		else{
			Debug.LogWarning("There should only be one of these");
		}
	}
	
	
	
	/// <summary>
	/// Constructor - Use this for initialization before anything is visible
	/// </summary>	
	void Start () {
		
		checkPointPosition = new Vector3();
		
		// Cache a reference to the player object.
		playerObject = GameObject.Find("Boxy");

		// Check it exists
		if( !playerObject )
		{
			print("[ERROR] Could not find the player object");
			Debug.Break();	
		}
				
		// Track the player's initial position for the checkpoint		
		checkPointPosition.x = playerObject.transform.position.x;	
		checkPointPosition.y = playerObject.transform.position.y;
		checkPointPosition.z = playerObject.transform.position.z;		
		
		
		// TODO DEBUG START
		
		// Initialize CollectibleManager
		// This should be done when the game first fires up
		CollectibleManager.Instance.Initialize();
		
		// TODO DEBUG END
		
		// Initialize all the GUI references		
	}
	
	/// <summary>
	/// Update is called once per frame 
	/// </summary>	
	void Update () {
		
		// Check for input
		CheckForInput();	
		
	}
	
	
	/// <summary>
	/// Input Functions 
	/// </summary>	 
	private void CheckForInput()
	{
#if UNITY_EDITOR				
		// Check if mouse is down for the first time.		
		bool mouseDown = Input.GetMouseButtonDown(0);
		
		// First mouse press
		if( mouseDown && !mouseHeld )
		{			
			// Position
			Camera mainCamera = UnityEngine.Camera.mainCamera;
			Vector3 mousePosition = Input.mousePosition;			
			mousePosition.z = -mainCamera.transform.position.z;
			Vector3 worldPosition = mainCamera.ScreenToWorldPoint( mousePosition );
												
			
			mouseHeld = true;			
			
			// Start particle effect
		}		
		
		// Check if the mouse has just been released.
		bool mouseUp = Input.GetMouseButtonUp(0);
		if( mouseUp && mouseHeld )
		{
			mouseHeld = false;					
			
			// End particle effect			
		}
				
#elif UNITY_ANDROID
		// DO ANDROID STUFF
		
#elif UNITY_IPHONE		
		// If there are no touches, do nothing
		if( Input.touches.Length == 0 )
		{			
			return;
		}
				
		// Check touches
		foreach(Touch touch in Input.touches) {
			
			// Calculate touches world space position.
			Camera mainCamera = UnityEngine.Camera.mainCamera;
			Vector3 fingerPosition = new Vector3();
			fingerPosition.x = touch.position.x;
			fingerPosition.y = touch.position.y;
			fingerPosition.z = -mainCamera.transform.position.z;
			Vector3 worldPosition = mainCamera.ScreenToWorldPoint( fingerPosition );
			
			float fingerDelta = touch.deltaPosition.magnitude;
					
			// On first touch
			if (touch.phase == TouchPhase.Began) 
			{
				
			}
			
			// Touch ended or canceld
			else if( touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled )
			{
				
			}
		}
#endif
	}	
	
	
	/// <summary>
	/// This function gets the "game" started and hands over control to the player.
	/// This allows us to get the level started whenever we want instead of starting
	/// the level as soon as the LevelManager object is instantiated and Start() is called.
	/// </summary>
	public void StartLevel()
	{
		print("Level Started");
		// Play particles
		
		// Play sounds
		
		
		// Get the player moving
	}
	
	
	/// <summary>
	/// This function is called when the finish line object is triggered and we
	/// need to start wrapping things up and transition to another scene.
	/// </summary>
	public void FinishLevel()
	{
		print("Level Finished");
		
		// Play particles
		
		// Play sounds
		
		// Show results text
		
		// Wait for input and then move to the next scene
		
		
		// TODO DEBUG ONLY - Reload the scene.  Should go to score screen.
		Application.LoadLevel( "Sandbox" );		
	}
	
	
	/// <summary>
	/// Restart the level - Reset the level objects, hopefully this
	/// won't screw up the timing of anything.
	/// </summary>
	public void RestartLevel()
	{
		// Go through all the objects in the scene and call their Reset function.
		// This could be optimized by tagging all level objects just to go through them
		// instead of everything.
		foreach( GameObject gameObj in FindObjectsOfType (typeof(GameObject)) )
		{
			gameObj.SendMessage ("ResetObject", SendMessageOptions.DontRequireReceiver);			
		}
		
		
		// Spawn the player at the last checkpoint.
		playerObject.transform.position = checkPointPosition;		
	}
	
		
	/// <summary>
	/// Pause the game.  This may be called when users switch to another app. 
	/// </summary>
	public void PauseLevel()
	{
		print("Level Paused");
		Time.timeScale = 0.0f;
	}
	
	
	/// <summary>
	/// Unpause the game. 
	/// </summary>
	public void UnpauseLevel()
	{
		print("Level Unpaused");
		Time.timeScale = 1.0f;
	}
}
