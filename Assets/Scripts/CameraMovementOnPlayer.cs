using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementOnPlayer : MonoBehaviour {

	Transform planetTf;
	Transform playerTf;
	Vector2 planetDirection;
	
	GameObject playerGo;
	float colliderRadius;
	float planetScale;
	float planetRadius;


	// Use this for initialization
	void Start () {
		playerGo = GameObject.FindGameObjectWithTag("Player");
		if(playerGo == null){
			Debug.LogError("Could not find a object with tag player!");
			return;
		}

		GameObject planetGo = GameObject.FindGameObjectWithTag("Planet");
		if(planetGo == null){
			Debug.LogError("Could not find a object with tag planet!");
			return;
		}

		if(playerGo == null){
			Debug.LogError("Could not find a object with tag player!");
			return;
		}

		playerTf = playerGo.transform;
		planetTf = planetGo.transform;

		colliderRadius = planetTf.GetComponent<CircleCollider2D>().radius;
		planetScale = planetTf.localScale.x;
		
		planetRadius = colliderRadius * planetScale;
		
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		planetDirection = playerGo.GetComponent<PlayerMovement>().PlanetDirection;

		transform.rotation = playerTf.rotation;
		transform.position = planetTf.position - new Vector3(
			((planetRadius*planetDirection).x * 1.1f),
			((planetRadius*planetDirection).y * 1.1f), 
			10);
		
	}
}
