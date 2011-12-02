using UnityEngine;
using System.Collections;

public class RigidbodyMovementScript : MonoBehaviour {
	
	public float moveMultiplier = 1f;
	public float jumpSpeed = 10f;
	
	private float distGround;
	private bool lookingLeft;
	private bool isGrounded;
	
	// Use this for initialization
	void Start () {
		CapsuleCollider myCollider = ((CapsuleCollider)rigidbody.collider);
		distGround = myCollider.height/2 + myCollider.radius;
		lookingLeft = true;
		isGrounded = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.position = new Vector3(transform.position.x, transform.position.y, 0);
		transform.LookAt(transform.position + (lookingLeft ? Vector3.left : Vector3.right), Vector3.up);
		
		RaycastHit hit;
	    isGrounded = Physics.Raycast(transform.position, -Vector3.up, out hit, distGround + 0.1f);
	    if (isGrounded && Input.GetAxis("Vertical") > 0){ // only jump from the ground
			Vector3 jumpVector = new Vector3(0, jumpSpeed, 0);
			rigidbody.velocity = jumpVector;
	    }
		
		if (Input.GetAxis("Horizontal") > 0) {
			animation.CrossFade("run_forward");
			if (lookingLeft) {
				transform.FindChild("player_root").Rotate(new Vector3(0, 180, 0));
			}
			rigidbody.AddRelativeForce(Vector3.right * 100, ForceMode.Acceleration);
			lookingLeft = false;
		} else if (Input.GetAxis("Horizontal") < 0) {
			animation.CrossFade("run_forward");
			if (!lookingLeft) {
				transform.FindChild("player_root").Rotate(new Vector3(0, 180, 0));
			}
			rigidbody.AddRelativeForce(Vector3.right * 100, ForceMode.Acceleration);
			lookingLeft = true;
		} else {
			animation.CrossFade("idle");
		}
	}
}
