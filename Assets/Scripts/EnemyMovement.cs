using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
	CollisionObject collisionObject;
	PhysicsObject physicsObject;
	int collisonPushback = 10;
	public int hp = 2;

	public bool followPLayer = false;
	Transform playerTransform;
	public float moveSpeed = 0.01f;

	public float jumpSpeed = 300;

	Vector3 playerPosition = Vector3.zero;

	// Use this for initialization
	void Awake () {
		physicsObject = GetComponent<PhysicsObject>();
		physicsObject.HitEvenet.AddListener(Jump);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		physicsObject.UpdateVelocity();
		physicsObject.UpdateRotation("standard");
		
		if(followPLayer){
			if (playerTransform != null){
				moveToPlayer(playerTransform);
			}
			
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
		if (collider2D.transform.GetComponentsInParent<ProjectileMovement>().Length > 0){
			collidedWithProjectile(collider2D);
		}
		if (collider2D.transform.GetComponentsInParent<PlayerMovement>().Length > 0){
			followPLayer = true;
			playerTransform = collider2D.transform;
		}
	}

	void collidedWithProjectile(Collider2D collider2D){
		if (collider2D.name != "Player"){
			hp -= 1;
			physicsObject.addVelocityVector(vectorFromPosition(collider2D.transform) * collisonPushback);
			
		}
		if (hp == 0){
			physicsObject.TargetPlanet.GetComponentInChildren<GravField>().EnemyDied();
			
			Destroy(gameObject);
		}
	}

	void Jump(){
		physicsObject.addVelocityUp(jumpSpeed);
	}
}
