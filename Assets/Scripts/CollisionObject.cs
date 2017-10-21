using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class CollisionObject : MonoBehaviour {

	public UnityEvent CollisionEvent = new UnityEvent();

	//When a obejct enters a gravField, set the gravField
	void OnTriggerEnter2D(Collider2D collider2D){
		if (collider2D.name != "GravField"){
			CollisionEvent.Invoke();
		}
		
	}
	
}

