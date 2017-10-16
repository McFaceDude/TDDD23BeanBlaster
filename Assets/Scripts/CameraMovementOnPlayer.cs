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
	public bool PlayerView = true;
	float zoomSpeed = 30; 
	Camera camera;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		planetTf =  player.GetComponent<PhysicsObject>().TargetPlanet;
		camera = transform.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		planetTf =  player.GetComponent<PhysicsObject>().TargetPlanet;
		if (PlayerView){
			transform.position =  new Vector3(player.transform.position.x, player.transform.position.y, -10);
		}
		
			
	}
	public void ZoomOutForBeanification(Vector3 planetPosition){
		PlayerView = false;
		print("field of view " + camera.orthographicSize);
		//camera.fieldOfView = 20;
		camera.transform.position = new Vector3(planetPosition.x, planetPosition.y, -10);
		camera.orthographicSize = 32;
	}
}
