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
	public bool gameOver = false;

	private GUIStyle guiStyle = new GUIStyle();

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		planetTf =  player.GetComponent<PhysicsObject>().TargetPlanet;
		camera = transform.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		if (player != null){
			planetTf =  player.GetComponent<PhysicsObject>().TargetPlanet;
			if (PlayerView){
				transform.position =  new Vector3(player.transform.position.x, player.transform.position.y, -10);
				camera.orthographicSize = 6;
				transform.rotation = player.transform.rotation;
			}
			else if(ZoomedOutPlayerView){
				camera.transform.position =  new Vector3(player.transform.position.x, player.transform.position.y, -10);
				transform.rotation = player.transform.rotation;
				camera.orthographicSize = 32;
			}
		
			transform.rotation = player.transform.rotation;
		}
		
	}

	public void FollowPlayerZoomedOut(Vector3 playerPosition){
		this.playerPosition = new Vector3(playerPosition.x, playerPosition.y, -10);
		PlayerView = false;
		ZoomedOutPlayerView = true;
	}
	void OnGUI(){
		if (gameOver){
			guiStyle.fontSize = 80;
			guiStyle.normal.textColor = Color.white;
			GUI.Label(new Rect(20,Screen.height/4,Screen.width,Screen.height),"GAME OVER!", guiStyle);
		}
		
	}

	public void ZoomOutForBeanification(Vector3 planetPosition){
		PlayerView = false;
		ZoomedOutPlayerView = false;
		camera.transform.position = new Vector3(planetPosition.x, planetPosition.y, -10);
		camera.orthographicSize = 32;
		transform.rotation = player.transform.rotation;
	}
}
