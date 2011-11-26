using UnityEngine;
using System.Collections;

public class CharacterControllerMovementScript : MonoBehaviour {
	
	public float moveMultiplier = 1f;
	public float jumpSpeed = 10f;
	public float fallRate = 54f;
	
	private bool lookingLeft;
	private bool hasJumped;
	private float notGroundedTimer;
	private Vector3 moveDirection;
	
	// Use this for initialization
	void Start () {
		lookingLeft = true;
		hasJumped = false;
		notGroundedTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(transform.position + (lookingLeft ? Vector3.left : Vector3.right), Vector3.up);
		CharacterController controller = GetComponent<CharacterController>();
		
		moveDirection = Vector3.zero;
		if (Input.GetAxis("Horizontal") > 0) {
			animation.CrossFade("run_forward");
			if (lookingLeft) {
				transform.FindChild("player_root").Rotate(new Vector3(0, 180, 0));
			}
			moveDirection.x += moveMultiplier;
			lookingLeft = false;
		} else if (Input.GetAxis("Horizontal") < 0) {
			animation.CrossFade("run_forward");
			if (!lookingLeft) {
				transform.FindChild("player_root").Rotate(new Vector3(0, 180, 0));
			}
			moveDirection.x += -moveMultiplier;
			lookingLeft = true;
		} else {
			animation.CrossFade("idle");
		}
		
		if (controller.isGrounded) {
			notGroundedTimer = 0;
			hasJumped = false;
		}
			
		if (controller.isGrounded && Input.GetAxis("Vertical") > 0){ // only jump from the ground
			hasJumped = true;
	    }
		
		if (hasJumped) {
			moveDirection.y += jumpSpeed * Time.deltaTime;
		}
		notGroundedTimer += Time.deltaTime;
		moveDirection.y -= fallRate * notGroundedTimer*notGroundedTimer;
		controller.Move(moveDirection * Time.deltaTime);
	}
}
