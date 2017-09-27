using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {


	Vector2 vectorToPlanet; 

	public Vector2 PlanetDirection { get {return vectorToPlanet.normalized;}}
	float PlanetDistane { get {return vectorToPlanet.magnitude;}}
	Vector2 PlanetTangentLeft { 
		get {
				Vector2 dir = PlanetDirection;
				return new Vector2(dir.y, -dir.x);
			}
		}
	Vector2 PlanetTangentRight{ get {return -PlanetTangentLeft;}}
	float moveVelocity = 10.0f;
	float jumpVelocity = 20.0f;	
	Vector2 velocity = Vector2.zero;

	public LayerMask RayMask;

	public Transform TargetPlanet;

	public bool didJump = false;
	public bool leftPressed = false;
	public bool rightPressed = false;
	private Rigidbody2D body;

	float playerRadius;


	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D>();
		float playerColliderRadius = transform.GetComponent<CircleCollider2D>().radius;
		float playerScale = transform.localScale.x;
		playerRadius = playerScale * playerColliderRadius;

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			didJump = true;
		}
		if(Input.GetKeyDown(KeyCode.LeftArrow)){
			leftPressed = true;
		}
		if(Input.GetKeyDown(KeyCode.RightArrow)){
			rightPressed = true;
		}
		
		
		
	}
	void FixedUpdate(){

		float gravity = 50.0f;

		vectorToPlanet = TargetPlanet.position - transform.position;
		vectorToPlanet = TargetPlanet.position - transform.position;
		velocity += PlanetDirection * gravity * Time.deltaTime;

		Debug.DrawRay(transform.position, PlanetDirection * gravity,  Color.red);
		Debug.DrawRay(transform.position, velocity + PlanetTangentLeft * moveVelocity,  Color.yellow);

		float raycastDistance = 0.5f + velocity.magnitude * Time.deltaTime;
		RaycastHit2D hit = Physics2D.Raycast(transform.position, PlanetDirection, raycastDistance, RayMask);
		if(hit){
			velocity = velocity.normalized * (hit.distance - 0.5f) * Time.deltaTime;
		}

		if(didJump){
			didJump = false;
			velocity += jumpVelocity * -PlanetDirection;
		}	
		if(leftPressed){

			leftPressed = false;
			velocity += moveVelocity * PlanetTangentLeft;
			print(PlanetTangentLeft);
			print("veclocity: " + velocity);
			
		}
		if(rightPressed){
			rightPressed = false;
			velocity += moveVelocity * PlanetTangentRight;
		}
		
		Debug.DrawRay(transform.position, PlanetDirection  * (hit ? hit.distance : raycastDistance), Color.yellow);
		Debug.DrawRay(transform.position, moveVelocity * PlanetTangentLeft, Color.green);
		Debug.DrawRay(transform.position, moveVelocity * PlanetTangentRight, Color.blue);
		
		body.position += velocity * Time.deltaTime;
		body.rotation = Mathf.Rad2Deg * Mathf.Atan2(PlanetDirection.y, PlanetDirection.x) + 90;
		
	}

}
