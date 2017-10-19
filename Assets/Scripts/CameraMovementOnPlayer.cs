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

	//for shaking
	Vector3 originPosition;
	Quaternion originRotation;
	float shake_decay;
	float shake_intensity;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		
		planetTf =  player.GetComponent<PhysicsObject>().TargetPlanet;
		camera = transform.GetComponent<Camera>();
	}
	
	void Update(){
		if(shake_intensity > 0){
			transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
			transform.rotation = new  Quaternion(
							originRotation.x + Random.Range(-shake_intensity,shake_intensity)* 0.2f,
							originRotation.y + Random.Range(-shake_intensity,shake_intensity)* 0.2f,
							originRotation.z + Random.Range(-shake_intensity,shake_intensity)* 0.2f,
							originRotation.w + Random.Range(-shake_intensity,shake_intensity)* 0.2f);
			shake_intensity -= shake_decay;
		}
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

	public void Shake(){
		originPosition = transform.position;
		originRotation = transform.rotation;
		shake_intensity = 0.2f;
		shake_decay = 0.09f;
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
			GUI.Label(new Rect(40,100,Screen.width,Screen.height),"GAME OVER!" , guiStyle);
			guiStyle.fontSize = 30;
			GUI.Label(new Rect(40,200,Screen.width,Screen.height),"Created by Samuel Lindgren samli627" , guiStyle);
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
