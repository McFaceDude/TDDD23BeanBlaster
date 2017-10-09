using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementOnPlayer : MonoBehaviour {

	Transform planetTf;
	PlayerMovement playerMovement;
	Vector2 planetDirection;

	float colliderRadius;
	float planetScale;
	float planetRadius;
	float ScalingFactor = 1.1f;
	


	// Use this for initialization
	void Start () {
		playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
		planetTf =  playerMovement.TargetPlanet;
		colliderRadius = planetTf.GetComponent<CircleCollider2D>().radius;
		planetScale = planetTf.localScale.x;
		planetRadius = colliderRadius * planetScale;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		planetTf =  playerMovement.TargetPlanet;

		if(planetTf != null){
			transform.rotation = playerMovement.transform.rotation;
			transform.position =  new Vector3(playerMovement.transform.position.x, playerMovement.transform.position.y, -10);
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
			transform.position =  new Vector3(playerMovement.transform.position.x, playerMovement.transform.position.y, -10);
			
		}

		
		
	}
}
