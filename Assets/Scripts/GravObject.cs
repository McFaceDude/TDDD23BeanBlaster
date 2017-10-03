using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Has the positions of the planet and sets all the vectors for the obejcts effected by gravity and the calls the addVelocity method 

public class GravObject : MonoBehaviour {

	GravField currentGravField = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//When a obejct enters a gravField, get the gravField
	void OnTriggerEnter2D(Collider2D gravField){
		print("Collided with gravField");
		GravField field = gravField.GetComponent<GravField>();
		GetComponentInParent<PlayerMovement>().SetGravitySource(field); 
	}

	void OnTriggerExit2D(Collider2D gravField){
		print("Leaving");
		
		GetComponentInParent<PlayerMovement>().SetGravitySource(null); 
	}
}
