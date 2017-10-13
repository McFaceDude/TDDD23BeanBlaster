using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour {

	PhysicsObject physicsObject;

	// Use this for initialization
	void Awake () {
		physicsObject = GetComponentInChildren<PhysicsObject>();
		physicsObject.addVelocityRight(400f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		physicsObject.UpdateVelocity();
		physicsObject.UpdateRotation("projectile");
	}

	public void SetDirection(Transform playerTransform){
		print("");
		print("player transform: " + playerTransform.rotation.eulerAngles.z);
		print("cos part: "+ Mathf.Cos( playerTransform.rotation.z));
		print("sin part: "+ Mathf.Sin(playerTransform.rotation.z));
		
		Vector2 direction = new Vector2(Mathf.Cos(Mathf.Deg2Rad * playerTransform.rotation.eulerAngles.z), Mathf.Sin(Mathf.Deg2Rad * playerTransform.rotation.eulerAngles.z));
		Debug.DrawRay(this.transform.position, direction * 5, Color.green, 4f);
		print("projectile direction " + direction);
		physicsObject.addVelocityVector(direction * 20f);
	}

}
