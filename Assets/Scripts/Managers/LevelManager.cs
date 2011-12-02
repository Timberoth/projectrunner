using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
	
	/*
	 * Enums
	 */
	
	
	
	/*
	 * Publics
	 */	
	
#if UNITY_EDITOR
	private bool mouseHeld = false;
#endif
	
	// Prefab References used to spawn objects at run time
	
	
	
	/*
	 * LevelManager Singleton Code
	 */	
	public static LevelManager instance;
	void Awake(){		
		if(instance == null){
			instance = this;
		}
		else{
			Debug.LogWarning("There should only be one of these");
		}
	}
	
	
	
	/*
	 * Unity Functions 
	 */
	
	// Use this for initialization
	void Start () {
		
		// TODO DEBUG START
		
		// Initialize CollectibleManager
		// This should be done when the game first fires up
		CollectibleManager.Instance.Initialize();
		
		// TODO DEBUG END
		
		
		
	}
	
	// Update is called once per frame
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
}
