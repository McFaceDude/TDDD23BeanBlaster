using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour {
	//Trampoline for jumping to the second planet
	//Adds velocity to player when player is on the trampoline
	public Transform gravfieldTf;

	void OnTriggerEnter2D(Collider2D collider2D){
		
		Vector3 trampolineVector =  -1 *(gravfieldTf.position - transform.position);
		collider2D.GetComponentInParent<PhysicsObject>().addVelocityVector(trampolineVector* 2.2f);
		GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
		camera.transform.GetComponent<CameraMovement>().FollowPlayerZoomedOut(collider2D.transform.position);
	}
}
