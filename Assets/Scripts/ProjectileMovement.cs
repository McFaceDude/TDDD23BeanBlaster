using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour {

	//Handles the pejectile movement and destoys the game object when it collides with something.
	PhysicsObject physicsObject;
	CollisionObject collisionObject;
	float projectileForce = 7;
	float projectileAngle = 70;

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
		Vector2 playerXVector = this.playerTransform.GetComponent<PhysicsObject>().xVector;
		if(facingRight){
			direction = new Vector2(Mathf.Cos(Mathf.Deg2Rad * (playerTransform.rotation.eulerAngles.z + projectileAngle)), Mathf.Sin(Mathf.Deg2Rad * (playerTransform.rotation.eulerAngles.z + projectileAngle)));
		}
		else {
			direction = new Vector2(Mathf.Cos(Mathf.Deg2Rad * (playerTransform.rotation.eulerAngles.z + 180 - projectileAngle)), Mathf.Sin(Mathf.Deg2Rad * (playerTransform.rotation.eulerAngles.z + 180 - projectileAngle)));
		}
		physicsObject.addVelocityVector(direction * ((projectileForce + playerXVector.magnitude)));
	}

	void OnTriggerEnter2D(Collider2D field){
		physicsObject.SetTargetPlanet(field.GetComponent<GravField>());
	}
	void onCollisionWithPlanet(){
		Destroy(gameObject);
	}

	void onCollisionWithEnemy(){
		Destroy(gameObject);
	}
}
