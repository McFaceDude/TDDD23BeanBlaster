using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour {

	// Use this for initialization

	public Transform gravfieldTf;

	void OnTriggerEnter2D(Collider2D collider2D){
		
		Vector3 trampolineVector =  -1 *(gravfieldTf.position - transform.position);
		collider2D.GetComponentInParent<PhysicsObject>().addVelocityVector(trampolineVector* 2.2f);
		GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
		camera.transform.GetComponent<CameraMovementOnPlayer>().FollowPlayerZoomedOut(collider2D.transform.position);
	}
}
