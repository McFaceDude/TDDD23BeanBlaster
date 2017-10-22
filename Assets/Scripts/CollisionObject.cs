using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class CollisionObject : MonoBehaviour {
	//Collisonscript for the enemies and projectile
	public UnityEvent CollisionEvent = new UnityEvent();
	
	void OnTriggerEnter2D(Collider2D collider2D){
		if (collider2D.name != "GravField"){
			CollisionEvent.Invoke();
		}
	}
}

