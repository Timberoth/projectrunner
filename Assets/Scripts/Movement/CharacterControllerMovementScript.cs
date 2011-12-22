using UnityEngine;
using System.Collections;

public class CharacterControllerMovementScript : MonoBehaviour {
	
	public float moveMultiplier = 5f;
	public float jumpForce = 10f;
	public float fallRate = 54f;
	
	private bool lookingLeft;
	private bool isJumping;
	private float lastX;
	private float notGroundedTimer;
	
	
	// Moving platform support
	private Transform activePlatform;
	private Vector3 activeLocalPlatformPoint;
	private Vector3 activeGlobalPlatformPoint;
	private Vector3 lastPlatformVelocity;
	
	// Use this for initialization
	void Start () 
	{
		lookingLeft = true;
		isJumping = false;
		lastX = 0;
		notGroundedTimer = 0;
	}
	
	
	// Update is called once per frame
	void Update () 
	{
		
		// Moving platform support
		if ( activePlatform != null ) 
		{			
			Vector3 newGlobalPlatformPoint = activePlatform.TransformPoint(activeLocalPlatformPoint);
			// Calculate the distance traveled by the platform between frames
			Vector3 moveDistance = (newGlobalPlatformPoint - activeGlobalPlatformPoint);
			
			// Apply that movement to the player.
			transform.position = transform.position + moveDistance;
			
			// Calculate platform velocity.
			lastPlatformVelocity = (newGlobalPlatformPoint - activeGlobalPlatformPoint) / Time.deltaTime;
		} else {
			lastPlatformVelocity = Vector3.zero;	
		}
		
		// Null the platform since it'll be set next frame by the collision.
		activePlatform = null;
		
		
		// make character look in the right direction and not topple over
		transform.LookAt(transform.position + (lookingLeft ? Vector3.left : Vector3.right), Vector3.up);
		
		// get components
		CharacterController controller = GetComponent<CharacterController>();
		
		
		// calculate movement
		// initialize delta vector
		Vector3 movementVector = Vector3.zero;
		
		// calculate x velocity
		movementVector.x = Input.GetAxis("Horizontal") * Time.deltaTime;
		movementVector.x *= moveMultiplier;
		
		// calculate y velocity
		// reset acceleration if on ground
		if (controller.isGrounded) {
			notGroundedTimer = 0;
			isJumping = false;
		}
		// only jump from the ground
		if (controller.isGrounded && Input.GetButton("Jump")){
			isJumping = true;
			animation.CrossFade("Jump");			
	    }
		// if jumping, add the jumpspeed to current movement
		if (isJumping) {
			movementVector.y += jumpForce * Time.deltaTime;
		}
		// accelerate downward, ie. gravity
		notGroundedTimer += Time.deltaTime;
		movementVector.y -= fallRate * notGroundedTimer * Time.deltaTime;
		
		
		// animation
		// based on x, animate appropriately
		if (movementVector.x != 0) {
			bool movingRight = (movementVector.x > 0);
			
			// if looking opposite of where going, rotate
			if ((movingRight) ? lookingLeft : !lookingLeft) {
				transform.FindChild("player_root").Rotate(new Vector3(0, 180, 0));
			}
			lookingLeft = (movingRight) ? false : true;
			
			// if starting run, calculate normally, else stop quicker
			if ((movingRight && lastX <= movementVector.x) ||
			    (!movingRight && lastX >= movementVector.x)) {
				
				if( !isJumping )
					animation.CrossFade("Walk");				
				
				lastX = movementVector.x;				
			} else {				
				lastX = 0;
			}
		} else {
			if( !isJumping )
				animation.CrossFade("Idle");			
		}
		
		
		// Moving platform support
		// This is a hacky special case to make sure that platforms will
		// push the character when he's standing completely still.
		// By apply a little x movement, the Move function will resolve
		// the collisions.  Without it the platform will pass through.
		if( movementVector.x == 0.0f )
		{
			movementVector.x = 0.00001f;	
		}
		
		// Move the player
		controller.Move(movementVector);
		
		
		// Moving platforms support - not quite sure what this is good for
		if (activePlatform != null) {
			activeGlobalPlatformPoint = transform.position;
			activeLocalPlatformPoint = activePlatform.InverseTransformPoint (transform.position);
		}		
	}
	
	
	void OnControllerColliderHit( ControllerColliderHit hit )
	{
		// Only want to track "Moving" objects like MovingPlatforms
		if( !hit.gameObject.name.Contains("Moving") )
		{
			return;
		}
		
		if (hit.moveDirection.y > 0.01) 
			return;
		
		// Make sure we are really standing on a straight platform
		// Not on the underside of one and not falling down from it either!
		//if (hit.moveDirection.y < -0.9 && hit.normal.y > 0.9) 
		{
			activePlatform = hit.collider.transform;	
		}
	}	
}
