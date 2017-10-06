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
	float groundMoveVelocity = 0.8f;
	float airMoveVelocity = 0.8f;
	float jumpVelocity = 10.0f;	
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
			//Set the vector to the planet from player.position and the velocity of player
			vectorToPlanet = TargetPlanet.position - transform.position;
			velocity += PlanetDirection * gravity * Time.deltaTime;
			Vector2 xVector = Vector2.Dot(velocity, PlanetTangentRight)*PlanetTangentRight; 
			Vector2 yVector = velocity - xVector; 

			//Collison detection with the planet.
			float raycastDistance = playerRadius + yVector.magnitude * Time.deltaTime;
			RaycastHit2D hit = Physics2D.Raycast(transform.position, PlanetDirection, raycastDistance, RayMask);

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
			float planetRadius = TargetPlanet.transform.localScale.x * TargetPlanet.GetComponent<CircleCollider2D>().radius;
			float t = Mathf.Clamp(1 - (PlanetDistane - planetRadius - playerRadius) * 0.01f, 0, 1);
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(PlanetDirection.y, PlanetDirection.x) + 90), t);		

		}
		//If not in gravField, we are in open space
		else{
			if(leftPressed){
				leftPressed = false;
				
				velocity += airMoveVelocity * PlanetTangentLeft;
				}
			if(rightPressed){
				rightPressed = false;

				velocity += airMoveVelocity * PlanetTangentRight;
			}		
		}
		Debug.DrawRay(transform.position, velocity, Color.red);
		body.velocity = velocity;
		
		}	
	public void AddVelocity(Vector2 velocity){
	}

	public void SetGravitySource(GravField gravField){

		if (gravField == null){
			inGravField = false;
			this.gravity = 0f;
			this.atmosphereFriction = 0f;
			this.planetFriction = 0f;
			TargetPlanet = null;
		}
		else{
			inGravField = true;
			this.gravity = gravField.gravity;
			this.atmosphereFriction = gravField.atmosphereFriction;
			this.planetFriction = gravField.planetFriction;
			TargetPlanet = gravField.transform.parent;
		}
	}
}
