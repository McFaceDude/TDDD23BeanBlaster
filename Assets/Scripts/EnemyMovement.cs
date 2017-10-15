using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	PhysicsObject physicsObject;
	// Use this for initialization
	void Awake () {
		//print("awake");
		physicsObject = GetComponent<PhysicsObject>();
		physicsObject.HitEvenet.AddListener(Jump);
		GravObject gravObject = GetComponentInChildren<GravObject>();
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		physicsObject.UpdateVelocity();
		physicsObject.UpdateRotation("standard");
		
		
	}

	void Jump(){
		//print("physics obejct "+ physicsObject.FrictionMultiplier);
		physicsObject.addVelocityUp(300);
		physicsObject.addVelocityRight(50);
		//print("jump");
		//physicsObject.addVelocityUp(800);
		//physicsObject.addVelocityLeft(10);
		
	}

	
}
