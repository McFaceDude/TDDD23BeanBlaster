using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Keeps track of the gravField

public class GravObject : MonoBehaviour {

	PhysicsObject physicsObject;

	// Use this for initialization
	void Awake () {
		//print("Grav Objects parent: " + GetComponentInParent<ProjectileMovement>());
		physicsObject =  GetComponentInParent<PhysicsObject>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	//When a obejct enters a gravField, set the gravField
	void OnTriggerEnter2D(Collider2D field){
		print("Collided with " + field.name);
		physicsObject.SetTargetPlanet(field.GetComponent<GravField>());
	}
	void OnTriggerExit2D(Collider2D field){
		//print("Leaving!");
		physicsObject.SetTargetPlanet(null);	 
	}
}
