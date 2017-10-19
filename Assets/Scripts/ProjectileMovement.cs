using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour {

	PhysicsObject physicsObject;
	
	CollisionObject collisionObject;
	//GameObject collisionObject2;
	float projectileForce = 11;
	float projectileAngle = 40;

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
		float playerSpeed = this.playerTransform.GetComponent<PhysicsObject>().Velocity.magnitude;
		if(facingRight){
			direction = new Vector2(Mathf.Cos(Mathf.Deg2Rad * (playerTransform.rotation.eulerAngles.z + projectileAngle)), Mathf.Sin(Mathf.Deg2Rad * (playerTransform.rotation.eulerAngles.z + projectileAngle)));
		}
		else {
			direction = new Vector2(Mathf.Cos(Mathf.Deg2Rad * (playerTransform.rotation.eulerAngles.z + 180 - projectileAngle)), Mathf.Sin(Mathf.Deg2Rad * (playerTransform.rotation.eulerAngles.z + 180 - projectileAngle)));
		}

		physicsObject.addVelocityVector(direction * ((projectileForce + (playerSpeed/2)) * 0.6f));
	}

	void OnTriggerEnter2D(Collider2D field){
		//print("Collided with gravField");
		physicsObject.SetTargetPlanet(field.GetComponent<GravField>());
	}
	void onCollisionWithPlanet(){
		//print("collided with planet");
		Destroy(gameObject);
	}

	void onCollisionWithEnemy(){
		//print("collided with enemy");
		Destroy(gameObject);
	}
}
