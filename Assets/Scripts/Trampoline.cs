using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour {

	// Use this for initialization

	public Transform gravfieldTf;


	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D collider2D){
		print("Collided with " + collider2D.name);
		Vector3 trampolineVector =  -1 *(gravfieldTf.position - transform.position);
		Debug.DrawRay(transform.position, -trampolineVector, Color.yellow, 4f);
		collider2D.GetComponentInParent<PhysicsObject>().addVelocityVector(trampolineVector* 2.2f);
		GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
		camera.transform.GetComponent<CameraMovementOnPlayer>().FollowPlayerZoomedOut(collider2D.transform.position);
		
	}
}
