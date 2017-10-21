using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravObject : MonoBehaviour {

	PhysicsObject physicsObject;

	// Use this for initialization
	void Awake () {
		physicsObject =  GetComponentInParent<PhysicsObject>();
	}

	//When a obejct enters a gravField, set the gravField
	void OnTriggerEnter2D(Collider2D field){
		physicsObject.SetTargetPlanet(field.GetComponent<GravField>());
	}
	void OnTriggerExit2D(Collider2D field){
		physicsObject.SetTargetPlanet(null);	 
	}
}
