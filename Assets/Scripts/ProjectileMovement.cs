﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour {

	PhysicsObject physicsObject;
	
	CollisionObject collisionObject;
	//GameObject collisionObject2;
	float projectileForce = 25;

	Transform playerTransform;
	Vector2 direction;

	// Use this for initialization
	void Awake () {
		physicsObject = GetComponent<PhysicsObject>();
		collisionObject = GetComponentInChildren<CollisionObject>();
		
		//Add listeners 
		physicsObject.HitEvenet.AddListener(onCollisionWithPlanet);
		collisionObject.CollisionEvent.AddListener(onCollisionWithEnemy);

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		physicsObject.UpdateVelocity();
		physicsObject.UpdateRotation("projectile");
	}

	public void SetDirection(Transform playerTransform, bool facingRight){
		this.playerTransform = playerTransform;
		if(facingRight){
			direction = new Vector2(Mathf.Cos(Mathf.Deg2Rad * playerTransform.rotation.eulerAngles.z), Mathf.Sin(Mathf.Deg2Rad * playerTransform.rotation.eulerAngles.z));
		}
		else {
			direction = new Vector2(Mathf.Cos(Mathf.Deg2Rad * (playerTransform.rotation.eulerAngles.z + 180)), Mathf.Sin(Mathf.Deg2Rad * (playerTransform.rotation.eulerAngles.z + 180)));
		}
		
		Debug.DrawRay(this.transform.position, direction * projectileForce, Color.green, 4f);
		physicsObject.addVelocityVector(direction * projectileForce);
		
		//physicsObject.addVelocityVector(direction * projectileForce);
	}

	void OnTriggerEnter2D(Collider2D field){
		//print("Collided with gravField");
		physicsObject.SetTargetPlanet(field.GetComponent<GravField>());
	}
	void onCollisionWithPlanet(){
		print("collided with planet");
		Destroy(gameObject);
	}

	void onCollisionWithEnemy(){
		print("collided with enemy");
		Destroy(gameObject);
	}
}
