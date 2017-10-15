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
	void OnTriggerEnter2D(Collider2D field){
		//print("Collided with gravField");
		print("collided with something");
		CollisionEvent.Invoke();
	}
	
}

