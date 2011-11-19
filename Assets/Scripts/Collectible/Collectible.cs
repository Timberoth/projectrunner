using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour {
	
	// Set through editor
	public GameObject collectItemFX = null;

	// Use this for initialization
	void Start () {		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter( Collider other )
	{
		// Fire Particle FX
		if( collectItemFX )
		{
			print("Fire particles");
			GameObject.Instantiate( collectItemFX, this.transform.position, this.transform.rotation );			
		}
		
		// Play Sound FX
		
		
		// Destroy this collectible
		GameObject.Destroy( this.gameObject );
	}
}
