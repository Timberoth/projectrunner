using UnityEngine;
using System.Collections;

public class CharacterControllerMovementScript : MonoBehaviour {
	
	public float moveMultiplier = 1f;
	public float jumpSpeed = 10f;
	public float fallRate = 54f;
	
	private bool lookingLeft;
	private bool isJumping;
	private float lastInputValue;
	private float notGroundedTimer;
	
	// Use this for initialization
	void Start () {
		lookingLeft = true;
		isJumping = false;
		lastInputValue = 0;
		notGroundedTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
		// make character look in the right direction and not topple over
		transform.LookAt(transform.position + (lookingLeft ? Vector3.left : Vector3.right), Vector3.up);
		
		// get variables
		CharacterController controller = GetComponent<CharacterController>();
		Vector3 moveVector = Vector3.zero;
		
		// check input status and do actions
		if (Input.GetAxis("Horizontal") != 0) {
			bool movingRight = (Input.GetAxis("Horizontal") > 0);
			
			if ((movingRight) ? lookingLeft : !lookingLeft) {
				transform.FindChild("player_root").Rotate(new Vector3(0, 180, 0));
			}
			lookingLeft = (movingRight) ? false : true;
			
			if ((movingRight && lastInputValue <= Input.GetAxis("Horizontal")) ||
			    (!movingRight && lastInputValue >= Input.GetAxis("Horizontal"))) {
				animation.CrossFade("run_forward");
				moveVector.x += ((movingRight) ? 1 : -1) * moveMultiplier * Time.deltaTime;
				lastInputValue = Input.GetAxis("Horizontal");
			} else {
				animation.CrossFade("idle");
				lastInputValue = 0;
			}
			
		} else {
			animation.CrossFade("idle");
		}
		
		// reset acceleration if on ground
		if (controller.isGrounded) {
			notGroundedTimer = 0;
			isJumping = false;
		}
		
		// only jump from the ground
		if (controller.isGrounded && Input.GetAxis("Vertical") > 0){
			isJumping = true;
	    }
		
		// if jumping, add the jumpspeed to current movement
		if (isJumping) {
			moveVector.y += jumpSpeed * Time.deltaTime;
		}
		// increase velocity (accelerate downward, ie. gravity)
		notGroundedTimer += Time.deltaTime;
		moveVector.y -= fallRate * notGroundedTimer*notGroundedTimer;
		controller.Move(moveVector * Time.deltaTime);
	}
}
