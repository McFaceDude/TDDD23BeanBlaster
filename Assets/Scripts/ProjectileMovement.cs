using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour {

	PhysicsObject physicsObject;

	float projectileForce = 20;

	Transform playerTransform;

	// Use this for initialization
	void Awake () {
		physicsObject = GetComponentInChildren<PhysicsObject>();
		physicsObject.HitEvenet.AddListener(onCollisionWithPlanet);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		

		physicsObject.UpdateVelocity();
		physicsObject.UpdateRotation("projectile");
	}

	public void SetDirection(Transform playerTransform){
		this.playerTransform = playerTransform;
		Vector2 direction = new Vector2(Mathf.Cos(Mathf.Deg2Rad * playerTransform.rotation.eulerAngles.z), Mathf.Sin(Mathf.Deg2Rad * playerTransform.rotation.eulerAngles.z));
		Debug.DrawRay(this.transform.position, direction * projectileForce, Color.green, 4f);
		physicsObject.addVelocityVector(direction * projectileForce);
		
		//physicsObject.addVelocityVector(direction * projectileForce);
	}

	void OnTriggerEnter2D(Collider2D field){
		//print("Collided with gravField");
		physicsObject.SetTargetPlanet(field.GetComponent<GravField>());
	}
	void onCollisionWithPlanet(){
		Destroy(gameObject);
	}
}
