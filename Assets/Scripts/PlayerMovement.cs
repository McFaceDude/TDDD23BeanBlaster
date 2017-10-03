using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Make the the pyhysics part of the player
//Make another script for the input and cotroll part




public class PlayerMovement : MonoBehaviour {

	bool inGravField;
	GravField gravField;
	public Vector2 PlanetDirection { get {return vectorToPlanet.normalized;}}
	public LayerMask RayMask;
	public Transform TargetPlanet;
	public bool didJump = false;
	public bool leftPressed = false;
	public bool rightPressed = false;
	float groundMoveVelocity = 1.0f;
	float airMoveVelocity = 1.0f;
	float jumpVelocity = 18.0f;	
	float playerRadius;
	float gravity;
	float planetFriction;
	float atmosphereFriction;
	float PlanetDistane { get {return vectorToPlanet.magnitude;}}
	Vector2 vectorToPlanet; 
	Vector2 PlanetTangentLeft { 
		get {
				Vector2 dir = PlanetDirection;
				return new Vector2(dir.y, -dir.x);
			}
		}
	Vector2 PlanetTangentRight{ get {return -PlanetTangentLeft;}}
	Vector2 velocity = Vector2.zero;
	private Rigidbody2D body;

	// Use this for initialization
	void Start () {
		//Starting inside gravField
		inGravField = true;
		//Get the gravity from the planet
		gravity = PlanetBigPhysics.gravity;
		planetFriction = PlanetBigPhysics.planetFriction;
		atmosphereFriction = PlanetBigPhysics.atmosphereFriction;

		//Set the body and size of the player
		body = GetComponent<Rigidbody2D>();
		float playerColliderRadius = transform.GetComponent<CircleCollider2D>().radius;
		float playerScale = transform.localScale.x;
		playerRadius = playerScale * playerColliderRadius;
	}
	// Update is called once per frame
	void Update () {
		//Check for user input
		if(Input.GetKeyDown(KeyCode.Space)){
			didJump = true;
		}
		if(Input.GetKey(KeyCode.LeftArrow)){
			leftPressed = true;
		}
		if(Input.GetKey(KeyCode.RightArrow)){
			rightPressed = true;
		}
	}
	void FixedUpdate(){
		if(inGravField){	
		}

		//Set the vector to the planet from player.position and the velocity of player
		vectorToPlanet = TargetPlanet.position - transform.position;
		velocity += PlanetDirection * gravity * Time.deltaTime;
		Vector2 xVector = Vector2.Dot(velocity, PlanetTangentRight)*PlanetTangentRight; 
		Vector2 yVector = velocity - xVector; 

		
		//Collison detection with the planet.
		float raycastDistance = playerRadius + yVector.magnitude * Time.deltaTime;
		RaycastHit2D hit = Physics2D.Raycast(transform.position, PlanetDirection, raycastDistance, RayMask);
		//Debug.DrawRay(transform.position, xVector , Color.yellow);
		//Debug.DrawRay(transform.position, yVector , Color.red);
		//Debug.DrawRay(transform.position, velocity , Color.blue);
		
		if(hit){
			//velocity.normalized * (hit.distance - playerRadius)
			velocity = xVector * planetFriction +  (hit.distance - playerRadius)  * PlanetDirection   ;
		}
		else{
			velocity = xVector * atmosphereFriction + yVector;
		}


		//Change the volecity if the user has pressed a key to move the player
		if(didJump){
			didJump = false;
			//Only jump when player is on the ground
			if (hit){
				velocity += jumpVelocity * -PlanetDirection;
			}
		}	
		if(leftPressed){
			leftPressed = false;
			if (hit){
				velocity += groundMoveVelocity * PlanetTangentLeft;
			}
			else{
				velocity += airMoveVelocity * PlanetTangentLeft;
			}
		
		}
		if(rightPressed){
			rightPressed = false;

			if (hit){
				velocity += groundMoveVelocity * PlanetTangentRight;
			}
			else{
				velocity += airMoveVelocity * PlanetTangentRight;
			}

		}		
		Debug.DrawRay(transform.position, velocity, Color.red);
		body.velocity = velocity;

		body.rotation = Mathf.Rad2Deg * Mathf.Atan2(PlanetDirection.y, PlanetDirection.x) + 90;
	}
	public void AddVelocity(Vector2 velocity){

	}

	public void SetGravitySource(GravField gravField){

		if (gravField == null){
			inGravField = false;
			this.gravity = 0f;
			this.atmosphereFriction = 0f;
			this.planetFriction = 0f;
			//TargetPlanet = null;
		}
		// Update values
		else{
			inGravField = true;
			this.gravity = gravField.gravity;
			this.atmosphereFriction = gravField.atmosphereFriction;
			this.planetFriction = gravField.planetFriction;
			TargetPlanet = gravField.transform;
		}
	
		


		
	}
}
