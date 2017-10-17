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
	public bool PlayerView = false;
	public bool ZoomedOutPlayerView = true;
	float zoomSpeed = 30; 
	Camera camera;
	Vector3 playerPosition;

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
			print("player view true in camera");
			transform.position =  new Vector3(player.transform.position.x, player.transform.position.y, -10);
			camera.orthographicSize = 6;
			transform.rotation = player.transform.rotation;
		}
		if(ZoomedOutPlayerView){
			print("zoomed out in camera");
			camera.transform.position =  new Vector3(player.transform.position.x, player.transform.position.y, -10);
			transform.rotation = player.transform.rotation;
			//camera.orthographicSize = 32;
		}
		transform.rotation = player.transform.rotation;
	}

	public void FollowPlayerZoomedOut(Vector3 playerPosition){
		this.playerPosition = new Vector3(playerPosition.x, playerPosition.y, -10);
		print("follow the player");
		PlayerView = false;
		ZoomedOutPlayerView = true;
	}

	public void ZoomOutForBeanification(Vector3 planetPosition){
		PlayerView = false;
		print("field of view " + camera.orthographicSize);
		//camera.fieldOfView = 20;
		camera.transform.position = new Vector3(planetPosition.x, planetPosition.y, -10);
		camera.orthographicSize = 32;
		transform.rotation = player.transform.rotation;
	}
}
