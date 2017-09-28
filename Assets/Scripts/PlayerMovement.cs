using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	public Vector2 PlanetDirection { get {return vectorToPlanet.normalized;}}
	public LayerMask RayMask;
	public Transform TargetPlanet;
	public bool didJump = false;
	public bool leftPressed = false;
	public bool rightPressed = false;
	float groundMoveVelocity = 1.0f;
	float airMoveVelocity = 1.0f;
	float jumpVelocity = 15.0f;	
	float playerRadius;
	float gravity;
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
		//Get the gravity from the planet
		gravity = PlanetBigPhysics.gravity;
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
		//Set the vector to the planet from player.position and the velocity of player
		vectorToPlanet = TargetPlanet.position - transform.position;
		velocity += PlanetDirection * gravity * Time.deltaTime;

		Vector2 xVector = Vector2.Dot(velocity, PlanetTangentRight)*PlanetTangentRight; 
		Vector2 yVector = velocity - xVector; 
		//Collison detection with the planet.
		float raycastDistance = playerRadius + yVector.magnitude * Time.deltaTime;
		RaycastHit2D hit = Physics2D.Raycast(transform.position, PlanetDirection, raycastDistance, RayMask);
		//Debug.DrawRay(transform.position, velocity.normalized, Color.yellow);
		//Debug.DrawRay(transform.position, PlanetDirection * raycastDistance, Color.blue);
		//Debug.DrawRay(transform.position, velocity.magnitude * PlanetTangentRight, Color.blue);

		
		Debug.DrawRay(transform.position, xVector , Color.yellow);
		Debug.DrawRay(transform.position, yVector , Color.red);
		Debug.DrawRay(transform.position, velocity , Color.blue);
		
		if(hit){
			//velocity.normalized * (hit.distance - playerRadius)
			velocity = (xVector  + (hit.distance - playerRadius) *PlanetDirection) ;
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
		
		//Debug vectors
		//Debug.DrawRay(transform.position, velocity, Color.green);
		//Debug.DrawRay(transform.position, groundMoveVelocity * PlanetTangentRight, Color.blue);
		//Debug.DrawRay(transform.position, PlanetDirection * gravity,  Color.red);

		//Add the velocity to the position and rotate the player in relation to the planet.
		//body.position += velocity * Time.deltaTime;
		body.velocity = velocity;
		body.rotation = Mathf.Rad2Deg * Mathf.Atan2(PlanetDirection.y, PlanetDirection.x) + 90;
	}
}
