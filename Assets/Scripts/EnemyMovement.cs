using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
	CollisionObject collisionObject;
	PhysicsObject physicsObject;
	int collisonPushback = 10;
	int hp = 3;

	bool followPLayer;
	Transform playerTransform;
	float moveSpeed = 0.01f;

	Vector3 playerPosition = Vector3.zero;

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
		
		if(followPLayer){
			moveToPlayer(playerTransform);
		}
		
	}
	Vector2 vectorFromPosition(Transform fromTransform){
		return new Vector2(transform.position.x - fromTransform.position.x, transform.position.y - fromTransform.position.y);
	}

	void moveToPlayer(Transform playerPosition){
		vectorFromPosition(playerPosition);
		physicsObject.addVelocityVector(vectorFromPosition(playerPosition) * - 1 * moveSpeed);
	}


	void OnTriggerEnter2D(Collider2D collider2D){
		if (collider2D.name != "GravField" && collider2D.name != "EnemyTrigger"){
	
			collidedWithProjectileNew(collider2D);
		}
		if (collider2D.name == "EnemyTrigger"){
			followPLayer = true;
			playerTransform = collider2D.transform;
		}
	}

	void OnTriggerExit2D(Collider2D collider2D){
		if (collider2D.name == "EnemyTrigger"){
			followPLayer = false;
			
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
		physicsObject.addVelocityUp(300);

	}

	
}
