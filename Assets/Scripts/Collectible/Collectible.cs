using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour {
	
	// Set through editor
	public GameObject collectItemFX = null;
	
	// Keep ref to audio source attached
	private AudioSource collectibleHit;

	// Use this for initialization
	void Start () {		
		
		InitializeAudio();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter( Collider other )
	{
		// Check for player tag
		if( other.gameObject.tag == "Player" )
		{		
			// Track collectible
			CollectibleManager.Instance.IncrementStarsCollected();		
			
			// Fire Particle FX
			if( collectItemFX )
			{			
				GameObject.Instantiate( collectItemFX, this.transform.position, this.transform.rotation );			
			}
			
			// Play Sound FX	
			AudioSource.PlayClipAtPoint( collectibleHit.clip, Vector3.zero );
			
			// Destroy this collectible
			GameObject.Destroy( this.gameObject );
		}
	}
	
	void InitializeAudio()
	{
		// Create references to the attached audio sources.
		foreach( AudioSource source in this.GetComponents<AudioSource>() )
		{
			if( source.clip.name == "CollectibleHit" )
			{
				collectibleHit = source;				
			}			
		}			
	}
}
