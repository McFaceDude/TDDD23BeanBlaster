using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	Vector2 jumpVelocity = new Vector2(0,3);	
	Vector2 velocity = Vector2.zero;

	public LayerMask RayMask;

	public Transform TargetPlanet;

	public bool didJump = false;
	private Rigidbody2D body;


	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			didJump = true;
		}
	}

	void FixedUpdate(){

		float gravity = 2.0f;
		
		Vector2 vectorToPlanet = TargetPlanet.position - transform.position;
		Vector2 directionToPlanet = vectorToPlanet.normalized;
		float distanceToPlanet = vectorToPlanet.magnitude;

		velocity += directionToPlanet * gravity * Time.deltaTime;
		Debug.DrawRay(transform.position, vectorToPlanet, Color.red);

		if(didJump == true){

			didJump = false;
			velocity = jumpVelocity;
		}	

		float raycastDistance = 0.5f + velocity.magnitude * Time.deltaTime;
		RaycastHit2D hit = Physics2D.Raycast(transform.position, velocity.normalized, raycastDistance, RayMask);
		Debug.DrawRay(transform.position, velocity.normalized  * (hit ? hit.distance : raycastDistance), Color.yellow);
	
		if(hit){
			velocity = velocity.normalized * (hit.distance - 0.5f) * Time.deltaTime;
		}

		body.position += velocity * Time.deltaTime;
		
	}

	void OnCollisionStay2D(Collision2D col){
		print("collision function");
		
//		velocity = Vector2.zero;
//		body.position += velocity * Time.deltaTime;

	}


}
