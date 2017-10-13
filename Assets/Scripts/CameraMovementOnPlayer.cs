using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementOnPlayer : MonoBehaviour {

	Transform planetTf;
	GameObject player;
	Vector2 planetDirection;

	float colliderRadius;
	float planetScale;
	float planetRadius;
	float ScalingFactor = 1.1f;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		planetTf =  player.GetComponent<PhysicsObject>().TargetPlanet;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		planetTf =  player.GetComponent<PhysicsObject>().TargetPlanet;

		if(planetTf != null){
			transform.rotation = player.transform.rotation;
			transform.position =  new Vector3(player.transform.position.x, player.transform.position.y, -10);
			/*
			colliderRadius = planetTf.GetComponent<CircleCollider2D>().radius;
			planetScale = planetTf.localScale.x;
			planetRadius = colliderRadius * planetScale;
			planetDirection = playerMovement.PlanetDirection;
			transform.rotation = playerMovement.transform.rotation;
			transform.position = planetTf.position - new Vector3(
				((planetRadius*planetDirection).x * ScalingFactor),
				((planetRadius*planetDirection).y * ScalingFactor), 
				10);
			 */
		}
		else{
			transform.position =  new Vector3(player.transform.position.x, player.transform.position.y, -10);
			
		}

		
		
	}
}
