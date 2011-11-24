using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour {
	
	public float acceleration;
	private bool lookingLeft = true;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("Horizontal") > 0) {
			animation.CrossFade("run_forward");
			transform.LookAt(transform.position + Vector3.right, Vector3.up);
			transform.Translate(Input.GetAxis("Horizontal"), 0, 0, Space.World);
			lookingLeft = false;
		} else if (Input.GetAxis("Horizontal") < 0) {
			animation.CrossFade("run_forward");
			transform.LookAt(transform.position + Vector3.left, Vector3.up);
			transform.Translate(Input.GetAxis("Horizontal"), 0, 0, Space.World);
			lookingLeft = true;
		} else {
			animation.CrossFade("idle");
			transform.LookAt(transform.position + ((lookingLeft) ? Vector3.left : Vector3.right) , Vector3.up);
		}
		
	}
}
