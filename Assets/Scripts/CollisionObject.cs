using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class CollisionObject : MonoBehaviour {

	public UnityEvent CollisionEvent = new UnityEvent();


	// Use this for initialization
	void Awake () {

	}
	
	// Update is called once per frame
	void Update () {
	}

	//When a obejct enters a gravField, set the gravField
	void OnTriggerEnter2D(Collider2D collider2D){
		//print("Collided with gravField");
		if (collider2D.name != "GravField"){
			//print("collided with something");
			CollisionEvent.Invoke();
		}
		
	}
	
}

