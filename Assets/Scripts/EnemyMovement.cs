using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	PhysicsObject physicsObject;
	// Use this for initialization
	void Awake () {
		print("awake");
		physicsObject = GetComponent<PhysicsObject>();
		physicsObject.HitEvenet.AddListener(Jump);
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		physicsObject.UpdateVelocity();
		physicsObject.UpdateRotation("standard");
		
		
	}

	void Jump(){
		print("jump");
		physicsObject.addVelocityUp(300);
		physicsObject.addVelocityLeft(20);
	}
}
