using UnityEngine;
using System.Collections;

public class RigidbodyTranslateMovementScript : MonoBehaviour {
	
	public float moveMultiplier = 1f;
	public float jumpSpeed = 10f;
	
	private bool lookingLeft;
	private bool isGrounded;
	
	// Use this for initialization
	void Start () {
		lookingLeft = true;
		isGrounded = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.position = new Vector3(transform.position.x, transform.position.y, 0);
		
	    isGrounded = Physics.Raycast(transform.position, -Vector3.up, 0.1f);
	    if (isGrounded && Input.GetAxis("Vertical") > 0){ // only jump from the ground
			Vector3 speedVector = new Vector3(0, jumpSpeed, 0);
	        rigidbody.velocity = speedVector;
	    }
		
		if (Input.GetAxis("Horizontal") > 0) {
			animation.CrossFade("run_forward");
			transform.LookAt(transform.position + Vector3.right, Vector3.up);
			transform.Translate((Input.GetAxis("Horizontal") * moveMultiplier), 0, 0, Space.World);
			lookingLeft = false;
		} else if (Input.GetAxis("Horizontal") < 0) {
			animation.CrossFade("run_forward");
			transform.LookAt(transform.position + Vector3.left, Vector3.up);
			transform.Translate((Input.GetAxis("Horizontal") * moveMultiplier), 0, 0, Space.World);
			lookingLeft = true;
		} else {
			animation.CrossFade("idle");
			transform.LookAt(transform.position + ((lookingLeft) ? Vector3.left : Vector3.right) , Vector3.up);
		}
	}
}
