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
	
	// Use this for initialization
	void Start () {
		lookingLeft = true;
		isJumping = false;
		lastX = 0;
		notGroundedTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
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
				animation.CrossFade("run_forward");
				lastX = movementVector.x;
			} else {
				animation.CrossFade("idle");
				lastX = 0;
			}
		} else {
			animation.CrossFade("idle");
		}
		
		controller.Move(movementVector);
	}
}
