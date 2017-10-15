using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
	CollisionObject collisionObject;
	PhysicsObject physicsObject;
	int collisonPushback = 10;
	int hp = 3;

	// Use this for initialization
	void Awake () {
		//print("awake");
		collisionObject = GetComponentInChildren<CollisionObject>();
		physicsObject = GetComponent<PhysicsObject>();
		physicsObject.HitEvenet.AddListener(Jump);
		GravObject gravObject = GetComponentInChildren<GravObject>();
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		physicsObject.UpdateVelocity();
		physicsObject.UpdateRotation("standard");
		
		
	}
	Vector2 vectorFromPosition(Transform fromTransform){
		return new Vector2(transform.position.x - fromTransform.position.x, transform.position.y - fromTransform.position.y);
	}

	void OnTriggerEnter2D(Collider2D collider2D){
		if (collider2D.name != "GravField"){
	
			collidedWithProjectileNew(collider2D);
		}
	}
	void collidedWithProjectileNew(Collider2D collider2D){
		if (collider2D.name != "Player"){
			hp -= 1;
			physicsObject.addVelocityVector(vectorFromPosition(collider2D.transform) * collisonPushback);
		}
		//print("enmy hit");
		if (hp == 0){
			Destroy(gameObject);
		}
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
